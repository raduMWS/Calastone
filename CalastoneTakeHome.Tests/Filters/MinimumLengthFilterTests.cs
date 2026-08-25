using CalastoneTakeHome.Filters;

namespace CalastoneTakeHome.Tests.Filters;

public sealed class MinimumLengthFilterTests
{
    [Theory]
    [InlineData("hi")]
    [InlineData("")]
    public void ShouldExclude_WordShorterThanMinimum_ReturnsTrue(string word)
    {
        var filter = new MinimumLengthFilter(3);

        Assert.True(filter.ShouldExclude(word));
    }

    [Theory]
    [InlineData("she")]
    [InlineData("sitting")]
    public void ShouldExclude_WordAtOrAboveMinimum_ReturnsFalse(string word)
    {
        var filter = new MinimumLengthFilter(3);

        Assert.False(filter.ShouldExclude(word));
    }

    [Fact]
    public void Constructor_NegativeMinimum_Throws()
    {
        Assert.Throws<ArgumentException>(() => new MinimumLengthFilter(-1));
    }
}
