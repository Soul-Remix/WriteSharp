using System.Collections.Generic;
using WriteSharp.Checks;
using WriteSharp.Types;
using Xunit;

namespace WriteSharp.Test;

public class StartWithSoTest
{
    private readonly StartWithSo _startWithSo;
    private const string SentenceStartWithSo = "So I saw the cute puppy and I picked it up. ";
    private const string GoodSentence = "The puppy was cute, so I picked it up.";
    private const string GoodSentenceStartsSo = "So??";
    private const string SoAfterSemicolonSentence = "This is a test; so it should pass or fail.";

    public StartWithSoTest()
    {
        _startWithSo = new StartWithSo();
    }

    [Fact]
    public void StartWithSo_SentenceStartStartWithSo()
    {
        List<CheckResult> results = _startWithSo.Check(SentenceStartWithSo);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 2,
                Reason = "\"So\" adds no meaning"
            }
        };
        
        Assert.Equal(expected, results);
    }

    [Fact]
    public void StartWithSo_GoodSentence()
    {
        List<CheckResult> results = _startWithSo.Check(GoodSentence);

        Assert.Empty(results);
    }
    
    [Fact]
    public void StartWithSo_GoodSentenceStartsSo()
    {
        List<CheckResult> results = _startWithSo.Check(GoodSentenceStartsSo);

        Assert.Empty(results);
    }
    
    [Fact]
    public void StartWithSo_SoAfterSemicolon()
    {
        List<CheckResult> results = _startWithSo.Check(SoAfterSemicolonSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 15,
                Offset = 2,
                Reason = "\"so\" adds no meaning"
            }
        };
        
        Assert.Equal(expected, results);
    }
}