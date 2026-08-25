namespace CalastoneTakeHome.Filters;

public interface IWordFilter
{
    bool ShouldExclude(string word);
}
