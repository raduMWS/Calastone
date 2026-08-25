using CalastoneTakeHome.Filters;

namespace CalastoneTakeHome.Tests.Filters;

public sealed class ContainsLetterFilterTests
{
    [Theory]
    [InlineData("tired")]
    [InlineData("Tired")]  // case insensitivity
    [InlineData("")]
    public void ShouldExclude_WordContainsLetter_ReturnsTrue(string word)
    {
        var filter = new ContainsLetterFilter('t');

        Assert.True(filter.ShouldExclude(word));
    }

    [Fact]
    public void ShouldExclude_WordWithoutLetter_ReturnsFalse()
    {
        var filter = new ContainsLetterFilter('t');

        Assert.False(filter.ShouldExclude("bank"));
    }

    [Fact]
    public void Constructor_WhitespaceLetter_Throws()
    {
        Assert.Throws<ArgumentException>(() => new ContainsLetterFilter(' '));
    }
}
