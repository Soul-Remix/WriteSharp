using System.Collections.Generic;
using WriteSharp.Checks;
using Xunit;

namespace WriteSharp.Test;

public class ThereIsTests
{
    private readonly ThereIs _thereIs;
    private const string SentenceStartWithThereIs = "There is a beautiful fish in the glass case.";
    private const string SentenceStartWithThereAre = "There are many leaves on the ground.";
    private const string BadSentenceThereIsMiddle = "Don’t lose heart; There is a better plan for you.";
    private const string GoodSentenceThereIs = "Using there is in the middle of a sentence.";
    private const string GoodSentenceThereAre = "Using there are in the middle of a sentence.";

    public ThereIsTests()
    {
        _thereIs = new ThereIs();
    }

    [Fact]
    public void ThereIs_SentenceStartWithThereIs()
    {
        List<CheckResult> results = _thereIs.Check(SentenceStartWithThereIs);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 8,
                Reason = "\"There is\" is unnecessary verbiage"
            }
        };
        
        Assert.Equal(expected, results);
    }
    
    [Fact]
    public void ThereIs_SentenceStartWithThereAre()
    {
        List<CheckResult> results = _thereIs.Check(SentenceStartWithThereAre);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"There are\" is unnecessary verbiage"
            }
        };
        
        Assert.Equal(expected, results);
    }
    
    [Fact]
    public void ThereIs_BadSentenceThereIsMiddle()
    {
        List<CheckResult> results = _thereIs.Check(BadSentenceThereIsMiddle);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 17,
                Offset = 9,
                Reason = "\" There is\" is unnecessary verbiage"
            }
        };
        
        Assert.Equal(expected, results);
    }
    
    [Fact]
    public void ThereIs_GoodSentenceThereIs()
    {
        List<CheckResult> results = _thereIs.Check(GoodSentenceThereIs);

        Assert.Empty(results);
    }
    
    [Fact]
    public void ThereIs_GoodSentenceThereAre()
    {
        List<CheckResult> results = _thereIs.Check(GoodSentenceThereAre);

        Assert.Empty(results);
    }
}