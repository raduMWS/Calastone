using CalastoneTakeHome.Filters;
using CalastoneTakeHome.Words;

namespace CalastoneTakeHome.Tests;

public sealed class TextFilterPipelineEndToEndTests
{
    private static TextFilterPipeline BuildBriefPipeline()
    {
        var filters = new List<IWordFilter>
        {
            new MinimumLengthFilter(3),
            new ContainsLetterFilter('t'),
            new VowelInMiddleFilter(new[] { 'a', 'e', 'i', 'o', 'u' })
        };

        return new TextFilterPipeline(new LetterWordSplitter(), new CompositeWordFilter(filters));
    }

    [Fact]
    public void Filter_OpeningSentenceOfTheInput_KeepsOnlyWordsPassingAllThreeFilters()
    {
        var pipeline = BuildBriefPipeline();
        var text = "Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do:";

        var result = pipeline.Filter(text);

        Assert.Equal(new[] { "beginning", "and" }, result);
    }

    [Fact]
    public void Filter_PunctuationBetweenWords_SplitsAndDropsCleanly()
    {
        var pipeline = BuildBriefPipeline();

        var result = pipeline.Filter("her.There was nothing so very remarkable in that;");

        Assert.Equal(new[] { "remarkable" }, result);
    }

    [Theory]
    [InlineData("clean")]
    [InlineData("what")]
    [InlineData("currently")]
    public void Filter_WordsTheBriefGivesAsFiltered_AreDropped(string word)
    {
        var pipeline = BuildBriefPipeline();

        var result = pipeline.Filter(word);

        Assert.Empty(result);
    }
}
