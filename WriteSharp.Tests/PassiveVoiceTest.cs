using System.Collections.Generic;
using WriteSharp.Checks;
using WriteSharp.Types;
using Xunit;

namespace WriteSharp.Test;

public class PassiveVoiceTest
{
    private readonly PassiveVoice _passiveVoice;

    private const string PassiveVoiceSentence = "He was judged";
    private const string PassiveVoiceOverLineBreakSentence = "He was\njudged";
    private const string IrregularVerb = "She was given an apple.";
    private const string IsIndeedAsPassive = "This sentence is indeed active.";
    private const string Reason = "may be a passive voice";

    public PassiveVoiceTest()
    {
        _passiveVoice = new PassiveVoice();
    }

    [Fact]
    public void PassiveVoice_DetectPassiveVoice()
    {
        List<CheckResult> results = _passiveVoice.Check(PassiveVoiceSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 3,
                Offset = 10,
                Reason = "\"was judged\" " + Reason
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void PassiveVoice_DetectPassiveVoiceLineBreak()
    {
        List<CheckResult> results = _passiveVoice.Check(PassiveVoiceOverLineBreakSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 3,
                Offset = 10,
                Reason = "\"was\njudged\" " + Reason
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void PassiveVoice_DetectPassiveVoiceIrregularVerb()
    {
        List<CheckResult> results = _passiveVoice.Check(IrregularVerb);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 4,
                Offset = 9,
                Reason = "\"was given\" " + Reason
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void PassiveVoice_IsIndeedNotPassive()
    {
        List<CheckResult> results = _passiveVoice.Check(IsIndeedAsPassive);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }
}