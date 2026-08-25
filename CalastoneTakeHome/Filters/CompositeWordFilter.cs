namespace CalastoneTakeHome.Filters;

public class CompositeWordFilter : IWordFilter
{
    private readonly List<IWordFilter> _filters;

    public CompositeWordFilter(IEnumerable<IWordFilter> filters)
    {
        if (filters == null)
        {
            throw new ArgumentException("Filters cannot be null", nameof(filters));
        }

        _filters = filters.ToList();

        if (_filters.Count == 0)
        {
            throw new ArgumentException("Filters cannot be empty", nameof(filters));
        }
    }

    public bool ShouldExclude(string word)
    {
        return _filters.Any(filter => filter.ShouldExclude(word));
    }
}
