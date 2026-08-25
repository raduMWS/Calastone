using CalastoneTakeHome.Input;

namespace CalastoneTakeHome.Tests.Input;

public class FileTextSourceTests
{
    [Fact]
    public void ReadText_ReturnsFileContents()
    {
        var path = Path.GetTempFileName();
        try
        {
            File.WriteAllText(path, "hello world");
            var source = new FileTextSource(path);

            var result = source.ReadText();

            Assert.Equal("hello world", result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void ReadText_MissingFile_Throws()
    {
        var source = new FileTextSource("does-not-exist.txt");

        Assert.Throws<FileNotFoundException>(() => source.ReadText());
    }

    [Fact]
    public void Constructor_EmptyPath_Throws()
    {
        Assert.Throws<ArgumentException>(() => new FileTextSource(""));
    }
}
