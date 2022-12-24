using System.Collections.Generic;
using WriteSharp.Checks;
using WriteSharp.Types;
using Xunit;

namespace WriteSharp.Test;

public class TooWordyTest
{
    private readonly TooWordy _tooWordy;

    private const string BadWordInSentence =
        "An abundance of highlights accede to long, complex sentences and common errors.";

    private const string BadPhrasesInSentence =
        "a number of sentences are so dense and complicated that your readers will be adversely impacted";

    private const string BadPhrasesInSentenceWithFormatting = "a   number \nof sentences are dense in a number of ways";
    private const string GoodSentence = "The good dog jumps over the bad cat.";

    private const string SentenceWithHyphen =
        "A transfer visa may be required at check-in between any connecting flight.";

    public TooWordyTest()
    {
        _tooWordy = new TooWordy();
    }

    // A sentence filled with overly complex words
    [Fact]
    public void WordsAreNoted()
    {
        List<CheckResult> results = _tooWordy.Check(BadWordInSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 3,
                Offset = 9,
                Reason = "\"abundance\" is too wordy"
            },
            new()
            {
                Index = 27,
                Offset = 9,
                Reason = "\"accede to\" is too wordy"
            }
        };

        Assert.Equal(expected, results);
    }

    // A sentence with phrases that could be stated more simply
    [Fact]
    public void PhrasesAreNoted()
    {
        List<CheckResult> results = _tooWordy.Check(BadPhrasesInSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 11,
                Reason = "\"a number of\" is too wordy"
            },
            new()
            {
                Index = 87,
                Offset = 8,
                Reason = "\"impacted\" is too wordy"
            }
        };

        Assert.Equal(expected, results);
    }

    // Hyphenated words should not be parsed as two words
    [Fact]
    public void HyphenWords()
    {
        List<CheckResult> results = _tooWordy.Check(SentenceWithHyphen);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }

    // Should not have a problem with a short sentence
    [Fact]
    public void ShortSentence()
    {
        List<CheckResult> results = _tooWordy.Check(GoodSentence);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }

    // Should not have a problem with white-space formatting
    [Fact]
    public void WhiteSpace()
    {
        List<CheckResult> results = _tooWordy.Check(BadPhrasesInSentenceWithFormatting);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 14,
                Reason = "\"a   number \nof\" is too wordy"
            },
            new()
            {
                Index = 38,
                Offset = 11,
                Reason = "\"a number of\" is too wordy"
            }
        };

        Assert.Equal(expected, results);
    }
}