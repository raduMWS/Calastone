using CalastoneTakeHome.Filters;

namespace CalastoneTakeHome.Tests.Filters;

public sealed class CompositeWordFilterTests
{
    private sealed class StubFilter : IWordFilter
    {
        private readonly bool _exclude;

        public StubFilter(bool exclude)
        {
            _exclude = exclude;
        }

        public bool ShouldExclude(string word)
        {
            return _exclude;
        }
    }

    [Theory]
    [InlineData(new[] { false, true })]
    [InlineData(new[] { true, true })]
    public void ShouldExclude_AnyFilterExcludes_ReturnsTrue(bool[] filterResults)
    {
        var composite = new CompositeWordFilter(filterResults.Select(r => (IWordFilter)new StubFilter(r)));

        Assert.True(composite.ShouldExclude("word"));
    }

    [Fact]
    public void ShouldExclude_NoFilterExcludes_ReturnsFalse()
    {
        var composite = new CompositeWordFilter(new[] { new StubFilter(false), new StubFilter(false) });

        Assert.False(composite.ShouldExclude("word"));
    }

    [Fact]
    public void Constructor_NullFilters_Throws()
    {
        Assert.Throws<ArgumentException>(() => new CompositeWordFilter(null!));
    }

    [Fact]
    public void Constructor_EmptyFilters_Throws()
    {
        Assert.Throws<ArgumentException>(() => new CompositeWordFilter(Array.Empty<IWordFilter>()));
    }
}
