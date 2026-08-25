namespace CalastoneTakeHome.Filters;

// Filter3 - filter out words that contain the letter 't'
public class ContainsLetterFilter : IWordFilter
{
    private readonly char _letter;

    public ContainsLetterFilter(char letter)
    {
        if (char.IsWhiteSpace(letter))
        {
            throw new ArgumentException("Letter cannot be whitespace", nameof(letter));
        }

        _letter = letter;
    }

    // true = exclude word
    public bool ShouldExclude(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return true;
        }

        return word.Contains(_letter, StringComparison.InvariantCultureIgnoreCase);
    }
}
