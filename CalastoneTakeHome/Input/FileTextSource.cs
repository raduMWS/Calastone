namespace CalastoneTakeHome.Input;

public class FileTextSource : ITextSource
{
    private readonly string _path;

    public FileTextSource(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be empty", nameof(path));
        }

        _path = path;
    }

    public string ReadText()
    {
        return File.ReadAllText(_path);
    }
}
