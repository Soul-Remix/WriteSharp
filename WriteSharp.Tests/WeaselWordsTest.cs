using System.Collections.Generic;
using WriteSharp.Checks;
using Xunit;

namespace WriteSharp.Test;

public class WeaselWordsTest
{
    private readonly WeaselWords _weaselWords;

    private const string WeaselWordsInSentence = "Remarkably few developers write well.";
    private const string WeaselSubWords = "Everything is ok.";
    private const string TooManySentence = "I have too many things.";
    private const string TooFewSentence = "I have too few things.";

    public WeaselWordsTest()
    {
        _weaselWords = new WeaselWords();
    }

    [Fact]
    public void WeaselWords_CheckWeaselWords()
    {
        List<CheckResult> results = _weaselWords.Check(WeaselWordsInSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 10,
                Reason = "\"Remarkably\" is a weasel word"
            },
            new()
            {
                Index = 11,
                Offset = 3,
                Reason = "\"few\" is a weasel word"
            }
        };

        Assert.Equal(expected, results);
    }
    
    [Fact]
    public void WeaselWords_CheckWeaselSubWords()
    {
        List<CheckResult> results = _weaselWords.Check(WeaselSubWords);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }
    
    [Fact]
    public void WeaselWords_CheckTooMany()
    {
        List<CheckResult> results = _weaselWords.Check(TooManySentence);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }
    
    [Fact]
    public void WeaselWords_CheckTooFew()
    {
        List<CheckResult> results = _weaselWords.Check(TooFewSentence);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }
}