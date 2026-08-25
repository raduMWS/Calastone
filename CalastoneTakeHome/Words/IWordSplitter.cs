namespace CalastoneTakeHome.Words;

public interface IWordSplitter
{
    IEnumerable<string> Split(string text);
}
