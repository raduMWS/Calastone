using CalastoneTakeHome.Filters;
using CalastoneTakeHome.Words;

namespace CalastoneTakeHome.Tests;

public sealed class TextFilterPipelineTests
{
    private sealed class StubSplitter : IWordSplitter
    {
        private readonly IEnumerable<string> _words;

        public StubSplitter(IEnumerable<string> words)
        {
            _words = words;
        }

        public IEnumerable<string> Split(string text)
        {
            return _words;
        }
    }

    private sealed class ExcludeByPrefixFilter : IWordFilter
    {
        private readonly string _prefix;

        public ExcludeByPrefixFilter(string prefix)
        {
            _prefix = prefix;
        }

        public bool ShouldExclude(string word)
        {
            return word.StartsWith(_prefix);
        }
    }

    [Theory]
    [InlineData(new[] { "apple", "banana", "avocado", "cherry" }, "a", new[] { "banana", "cherry" })]
    [InlineData(new[] { "one", "two", "three" }, "z", new[] { "one", "two", "three" })]
    public void Filter_KeepsOnlyWordsNotExcludedByFilter(string[] words, string excludePrefix, string[] expected)
    {
        var pipeline = new TextFilterPipeline(new StubSplitter(words), new ExcludeByPrefixFilter(excludePrefix));

        var result = pipeline.Filter("irrelevant, splitter is stubbed");

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Constructor_NullSplitter_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new TextFilterPipeline(null!, new ExcludeByPrefixFilter("a")));
    }

    [Fact]
    public void Constructor_NullFilter_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new TextFilterPipeline(new StubSplitter(Array.Empty<string>()), null!));
    }
}
