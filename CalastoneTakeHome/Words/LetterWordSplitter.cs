using System.Text;

namespace CalastoneTakeHome.Words;

//split text into words by letters ignoring punctuation
public class LetterWordSplitter : IWordSplitter
{
    public IEnumerable<string> Split(string text)
    {
        var words = new List<string>();
        var currentWord = new StringBuilder();

        foreach (var c in text)
        {
            if (char.IsLetter(c))
            {
                currentWord.Append(c);
            }
            else
            {
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
        }

        return words;
    }
}
