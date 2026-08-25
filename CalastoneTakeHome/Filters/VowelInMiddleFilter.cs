namespace CalastoneTakeHome.Filters;

// Filter1 - filter out all the words that contain a vowel in the middle of the
// word - the centre 1 or 2 letters ("clean" middle is 'e', "what" middle is
// 'ha', "currently" middle is 'e' and should be filtered, "the" and "rather"
// should not be).
public class VowelInMiddleFilter : IWordFilter
{
    private readonly HashSet<char> _vowels;

    public VowelInMiddleFilter(IEnumerable<char> vowels)
    {
        if (vowels == null)
        {
            throw new ArgumentException("Vowels cannot be null", nameof(vowels));
        }

        // lower-cased so the middle-letter comparison below is case insensitive
        _vowels = vowels.Select(char.ToLowerInvariant).ToHashSet();
    }

    // true = exclude word
    public bool ShouldExclude(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return true;
        }

        var length = word.Length;
        var mid = length / 2;

        var midChars = length % 2 == 0 ? word.Substring(mid - 1, 2) : word.Substring(mid, 1);

        return midChars.Any(c => _vowels.Contains(char.ToLowerInvariant(c)));
    }
}
