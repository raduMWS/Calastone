using CalastoneTakeHome.Words;

namespace CalastoneTakeHome.Tests.Words;

public sealed class LetterWordSplitterTests
{
    [Theory]
    [InlineData("hello world", new[] { "hello", "world" })]
    [InlineData("well, 'well' - that's odd!", new[] { "well", "well", "that", "s", "odd" })]
    [InlineData("one    two", new[] { "one", "two" })]
    [InlineData("", new string[0])]
    [InlineData("'hello'", new[] { "hello" })]
    public void Split_ReturnsExpectedWords(string text, string[] expected)
    {
        var splitter = new LetterWordSplitter();

        var result = splitter.Split(text);

        Assert.Equal(expected, result);
    }
}
