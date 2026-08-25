namespace CalastoneTakeHome.Filters;

public class CompositeWordFilter : IWordFilter
{
    private readonly List<IWordFilter> _filters;

    public CompositeWordFilter(IEnumerable<IWordFilter> filters)
    {
        if (filters == null || !filters.Any())
        {
            throw new ArgumentException("No filters present", nameof(filters));
        }

        _filters = filters.ToList();
    }

    public bool ShouldExclude(string word)
    {
        return _filters.Any(filter => filter.ShouldExclude(word));
    }
}
