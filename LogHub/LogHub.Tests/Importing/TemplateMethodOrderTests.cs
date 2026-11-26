using LogHub.Importing;
using LogHub.Models;
using LogHub.Reading;

namespace LogHub.Tests.Importing;

public class TemplateMethodOrderTests
{
    /// <summary>Импортёр-мок: записывает, какие шаги и в каком порядке вызывались.</summary>
    private sealed class RecordingImporter : LogImporter
    {
        public List<string> Calls { get; } = new();
        public bool ShouldThrowInParse { get; set; }
        private readonly ILogSource _source;

        public RecordingImporter(ILogSource source) => _source = source;

        protected override void OpenSource(ILogSource source) => Calls.Add("open");
        protected override IEnumerable<string> ReadLines(ILogSource source)
        {
            Calls.Add("read");
            return source.ReadLines();
        }
        protected override bool Validate(string line)
        {
            Calls.Add("validate");
            return base.Validate(line);
        }
        protected override LogRecord ParseLine(string line)
        {
            Calls.Add("parse");
            if (ShouldThrowInParse) throw new FormatException("bad");
            return new LogRecord { Date = DateTime.UtcNow, Message = line, SourceFormat = "test" };
        }
        protected override void CloseSource(ILogSource source) => Calls.Add("close");
        protected override void ReleaseSource(ILogSource source) => Calls.Add("release");
    }

    [Fact]
    public void Import_CallsSteps_InCorrectOrder()
    {
        var source = new InMemoryLogSource(new[] { "line1" });
        var importer = new RecordingImporter(source);

        _ = importer.Import(source).ToList();

        Assert.Equal(new[] { "open", "read", "validate", "parse", "close", "release" }, importer.Calls);
    }

    [Fact]
    public void Import_OnParseError_StillCallsCloseAndRelease()
    {
        var source = new InMemoryLogSource(new[] { "line1" });
        var importer = new RecordingImporter(source) { ShouldThrowInParse = true };

        var records = importer.Import(source).ToList();

        Assert.Single(records);
        Assert.Equal("BAD_FORMAT", records[0].Level);
        Assert.Equal("close",   importer.Calls[^2]);   // try/finally сработал
        Assert.Equal("release", importer.Calls[^1]);
    }
}