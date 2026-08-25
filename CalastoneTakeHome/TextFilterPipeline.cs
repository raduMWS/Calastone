using CalastoneTakeHome.Filters;
using CalastoneTakeHome.Words;

namespace CalastoneTakeHome;

public class TextFilterPipeline
{
    private readonly IWordSplitter _splitter;
    private readonly IWordFilter _filter;

    public TextFilterPipeline(IWordSplitter splitter, IWordFilter filter)
    {
        if (splitter == null)
        {
            throw new ArgumentNullException(nameof(splitter));
        }

        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        _splitter = splitter;
        _filter = filter;
    }

    public IEnumerable<string> Filter(string text)
    {
        return _splitter.Split(text).Where(word => !_filter.ShouldExclude(word));
    }
}
