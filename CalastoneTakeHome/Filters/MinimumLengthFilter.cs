namespace CalastoneTakeHome.Filters;

// Filter2 - filter out words that have length less than 3
public class MinimumLengthFilter : IWordFilter
{
    private readonly int _minimumLength;

    public MinimumLengthFilter(int minimumLength)
    {
        if (minimumLength < 0)
        {
            throw new ArgumentException("Minimum cannot be negative", nameof(minimumLength));
        }

        _minimumLength = minimumLength;
    }

    // true = exclude word
    public bool ShouldExclude(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return true;
        }

        return word.Length < _minimumLength;
    }
}
