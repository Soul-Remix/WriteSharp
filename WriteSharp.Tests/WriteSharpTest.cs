using System.Collections.Generic;
using WriteSharp.Types;
using Xunit;

namespace WriteSharp.Test;

public class WriteSharpTest
{
    private readonly WriteSharp _writeSharp;

    private const string WeaselWordSentence = "Remarkably few developers write well.";
    private const string PassiveVoiceSentence = "He was killed";
    private const string DuplicatesSentence = "This this is a nice day.";
    private const string DuplicatesLineBreak = "This this is a nice day.";
    private const string DuplicatesCaseInsensitive = "This this is a nice day.";
    private const string DuplicatesNonWord = "// //";
    private const string StartWithSoSentence = "So I saw the cute puppy and I picked it up.";
    private const string StartWithThereAre = "There are many leaves on the ground.";
    private const string CommonAdverbSentence = "Allegedly, this sentence is terrible.";
    private const string ComplexWordsSentence = "As a matter of fact, this sentence could be simpler.";
    private const string CommonClicheSentence = "Writing specs puts me at loose ends.";

    public WriteSharpTest()
    {
        _writeSharp = new WriteSharp();
    }

    [Fact]
    public void WriteSharp_DetectWeaselWords()
    {
        var results = _writeSharp.Check(WeaselWordSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 10,
                Reason = "\"Remarkably\" can weaken meaning and is a weasel word"
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
    public void WriteSharp_DetectPassiveVoice()
    {
        var results = _writeSharp.Check(PassiveVoiceSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 3,
                Offset = 10,
                Reason = "\"was killed\" may be a passive voice"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_PassiveVoiceDisabled_NotDetected()
    {
        var results = _writeSharp.Check(PassiveVoiceSentence, new WriteSharpOptions() { PassiveVoice = false });

        Assert.Empty(results);
    }

    [Fact]
    public void WriteSharp_DetectDuplicates()
    {
        var results = _writeSharp.Check(DuplicatesSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"This this\" is a duplicate"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_DuplicatesDisabled_NotDetected()
    {
        var results = _writeSharp.Check(DuplicatesSentence, new WriteSharpOptions() { Duplicates = false });

        Assert.Empty(results);
    }

    [Fact]
    public void WriteSharp_DetectDuplicatesLineBreak()
    {
        var results = _writeSharp.Check(DuplicatesLineBreak);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"This this\" is a duplicate"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_DetectDuplicatesCaseInsensitive()
    {
        var results = _writeSharp.Check(DuplicatesCaseInsensitive);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"This this\" is a duplicate"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_DuplicatesNonWord_NotDetected()
    {
        List<CheckResult> results = _writeSharp.Check(DuplicatesNonWord);

        Assert.Empty(results);
    }

    [Fact]
    public void WriteSharp_SentenceStartStartWithSo()
    {
        List<CheckResult> results = _writeSharp.Check(StartWithSoSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 2,
                Reason = "\"So\" adds no meaning"
            },
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_StartsWithSo_NotDetected()
    {
        var results = _writeSharp.Check(StartWithSoSentence,
            new WriteSharpOptions() { StartWithSo = false, WeaselWords = false });

        Assert.Empty(results);
    }

    [Fact]
    public void WriteSharp_SentenceStartWithThereAre()
    {
        List<CheckResult> results = _writeSharp.Check(StartWithThereAre);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"There are\" is unnecessary verbiage"
            },
            new()
            {
                Index = 10,
                Offset = 4,
                Reason = "\"many\" can weaken meaning and is a weasel word"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_DetectCommonAdverb()
    {
        List<CheckResult> results = _writeSharp.Check(CommonAdverbSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"Allegedly\" can weaken meaning"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_DetectComplexWords()
    {
        List<CheckResult> results = _writeSharp.Check(ComplexWordsSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 19,
                Reason = "\"As a matter of fact\" is too wordy"
            },
            new()
            {
                Index = 35,
                Offset = 8,
                Reason = "\"could be\" is a weasel word"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_DetectCommonCliche()
    {
        List<CheckResult> results = _writeSharp.Check(CommonClicheSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 22,
                Offset = 13,
                Reason = "\"at loose ends\" is a cliche"
            }
        };
        Assert.Equal(expected, results);
    }

    [Fact]
    public void WriteSharp_EmptyString_NoSuggestions()
    {
        var results = _writeSharp.Check("");

        Assert.Empty(results);
    }
}