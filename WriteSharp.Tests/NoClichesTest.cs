using System.Collections.Generic;
using WriteSharp.Checks;
using WriteSharp.Types;
using Xunit;

namespace WriteSharp.Test;

public class NoClichesTest
{
    private readonly NoCliches _noCliches;

    private const string ClichesInSentence = "Writing specs puts me at loose ends.";
    private const string ClichesInSentenceWithFormatting = "Writing specs puts me at   loose\n ends.";
    private const string GoodSentence = "The good dog jumps over the bad cat.";

    public NoClichesTest()
    {
        _noCliches = new NoCliches();
    }

    [Fact]
    public void NoCliches_SentenceFilledWithCliche_NotEscapeNotice()
    {
        List<CheckResult> results = _noCliches.Check(ClichesInSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 22,
                Offset = 13,
                Reason = "\"at loose ends\" is a cliche"
            }
        };
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void NoCliches_SentenceFilledWithCliche_NoProblemWhiteFormatting()
    {
        List<CheckResult> results = _noCliches.Check(ClichesInSentenceWithFormatting);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 22,
                Offset = 16,
                Reason = "\"at   loose\n ends\" is a cliche"
            }
        };
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void NoCliches_GoodSentence_NoProblems()
    {
        List<CheckResult> results = _noCliches.Check(GoodSentence);
        List<CheckResult> expected = new List<CheckResult>();
       
        Assert.Equal(expected,results);
    }
}