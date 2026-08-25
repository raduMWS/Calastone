using CalastoneTakeHome.Filters;

namespace CalastoneTakeHome.Tests.Filters;

public sealed class VowelInMiddleFilterTests
{
    private static readonly char[] Vowels = new[] { 'a', 'e', 'i', 'o', 'u' };

    [Theory]
    [InlineData("clean")]
    [InlineData("what")]
    [InlineData("currently")]
    [InlineData("a")]
    [InlineData("CLEAN")]
    [InlineData("")]
    public void ShouldExclude_WordWithVowelInMiddle_ReturnsTrue(string word)
    {
        var filter = new VowelInMiddleFilter(Vowels);

        Assert.True(filter.ShouldExclude(word));
    }

    [Theory]
    [InlineData("the")]
    [InlineData("rather")]
    [InlineData("b")]
    public void ShouldExclude_WordWithoutVowelInMiddle_ReturnsFalse(string word)
    {
        var filter = new VowelInMiddleFilter(Vowels);

        Assert.False(filter.ShouldExclude(word));
    }

    [Fact]
    public void Constructor_NullVowels_Throws()
    {
        Assert.Throws<ArgumentException>(() => new VowelInMiddleFilter(null!));
    }
}
