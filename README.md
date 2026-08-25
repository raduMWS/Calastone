# Calastone Take Home

Console app that reads some text, splits it into words, and prints back the ones
that make it past three filters.

## Running it

```bash
cd CalastoneTakeHome
dotnet run                  # uses input.txt
dotnet run -- some/file.txt # or point it at another file
```

Needs .NET 10.

## Running the tests

```bash
dotnet test
```

## The filters

A word gets dropped if it:

- has a vowel in the middle — the centre letter, or the centre two if the length
  is even (`clean` → `e`, `what` → `ha`, `currently` → `e` all go; `the` → `h`
  and `rather` → `th` stay)
- is shorter than 3 letters
- contains a `t`

Any one of them is enough to drop the word.

## How it's put together

`Program.cs` builds the list of filters and runs the pipeline. Each filter is a
class implementing `IWordFilter.ShouldExclude`, and `CompositeWordFilter` ORs
them together, so adding or dropping a rule is a one-line change at the top of
`Program.cs`. The thresholds are constructor arguments rather than constants,
which is why there's a `MinimumLengthFilter(3)` instead of a `LengthOfThree`
filter.

Reading the text and splitting it into words both sit behind interfaces
(`ITextSource`, `IWordSplitter`). Those seemed the most likely things to want to
swap later — stdin instead of a file, or smarter word splitting — and the
pipeline doesn't need to know either way.

I went with `ShouldExclude` rather than `ShouldInclude` because the brief is
written as "filter out X", and it's easier to check the code against the spec
when they read the same direction.

`LetterWordSplitter` treats anything that isn't a letter as a separator, which
handles punctuation and runs of spaces without special cases. It does mean
`don't` comes out as `don` and `t`, which felt like a fair trade for this input.

## The tests

There's a test class per filter, plus the splitter and the file source. The
examples the brief gives (`clean`, `what`, `currently`, `the`, `rather`) are used
directly as cases for the vowel filter, since they're the part most likely to be
read wrong.

`TextFilterPipelineTests` stubs the splitter and the filter out, so it only
checks the pipeline's own job — that it keeps what isn't excluded.
`TextFilterPipelineEndToEndTests` then wires up the real splitter and the real
three filters the way `Program.cs` does, and runs sentences from the input
through it, so the combination the brief asks for is covered rather than just
the pieces.

## If I'd kept going

If the input could ever be large, the splitter should stream instead of building
the whole list up front. The filter set is also still hardcoded in `Program.cs` —
if it needed to change without a rebuild, it'd move to config.
