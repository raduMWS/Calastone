using CalastoneTakeHome;
using CalastoneTakeHome.Filters;
using CalastoneTakeHome.Input;
using CalastoneTakeHome.Words;

// add/remove filters here
var filters = new List<IWordFilter>
{
    new MinimumLengthFilter(3),
    new ContainsLetterFilter('t'),
    new VowelInMiddleFilter(new[] { 'a', 'e', 'i', 'o', 'u' })
};

var inputPath = args.Length > 0 ? args[0] : "input.txt";

if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPath}");
    return 1;
}

var pipeline = new TextFilterPipeline(new LetterWordSplitter(), new CompositeWordFilter(filters));
var text = new FileTextSource(inputPath).ReadText();

Console.WriteLine(string.Join(' ', pipeline.Filter(text)));

return 0;
