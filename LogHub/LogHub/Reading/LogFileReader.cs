namespace LogHub.Reading;

public class LogFileReader
{
    public IEnumerable<string> ReadLines(string filePath)
    {
        return File.ReadLines(filePath);
    }
}