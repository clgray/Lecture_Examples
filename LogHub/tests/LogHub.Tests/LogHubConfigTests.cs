using LogHub.Config;
using Xunit;

namespace LogHub.Tests;

public class LogHubConfigTests
{
    [Fact]
    public void Parse_EmptyConfig_GivesDefaults()
    {
        var config = LogHubConfig.Parse(Array.Empty<string>());

        Assert.Equal("console", config.Sink);
        Assert.Equal("Info", config.MinSeverity);
        Assert.Equal("plain", config.Format);
        Assert.Equal('|', config.Delimiter);
    }

    [Fact]
    public void Parse_IgnoresCommentsAndBlankLines_AndTrimsValues()
    {
        var config = LogHubConfig.Parse(new[]
        {
            "# комментарий",
            "",
            "  sink =  file ",
            "minSeverity=Warning"
        });

        Assert.Equal("file", config.Sink);
        Assert.Equal("Warning", config.MinSeverity);
    }

    [Fact]
    public void Parse_Delimiter_IsReadFromConfig()
    {
        var config = LogHubConfig.Parse(new[] { "delimiter = ;" });

        Assert.Equal(';', config.Delimiter);
    }
}
