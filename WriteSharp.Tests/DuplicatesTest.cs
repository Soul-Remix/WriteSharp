using System.Collections.Generic;
using WriteSharp.Checks;
using Xunit;

namespace WriteSharp.Test;

public class DuplicatesTest
{
    private readonly Duplicates _duplicates;
    private const string BadSentenceThis = "This this is a nice day.";
    private const string BadSentenceA = "I saw a a dog.";
    private const string GoodSentence = "What about this? This tastes good.";
    private const string MultipleBadSentences = BadSentenceThis + GoodSentence + BadSentenceA;

    public DuplicatesTest()
    {
        _duplicates = new Duplicates();
    }

    [Fact]
    public void Duplicates_BadSentenceThis()
    {
        List<CheckResult> results = _duplicates.Check(BadSentenceThis);
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
    public void Duplicates_BadSentenceA()
    {
        List<CheckResult> results = _duplicates.Check(BadSentenceA);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 6,
                Offset = 3,
                Reason = "\"a a\" is a duplicate"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void Duplicates_MultipleBadSentences()
    {
        List<CheckResult> results = _duplicates.Check(MultipleBadSentences);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 9,
                Reason = "\"This this\" is a duplicate"
            },
            new()
            {
                Index = 64,
                Offset = 3,
                Reason = "\"a a\" is a duplicate"
            }
        };

        Assert.Equal(expected, results);
    }

    [Fact]
    public void Duplicates_GoodSentence()
    {
        List<CheckResult> results = _duplicates.Check(GoodSentence);

        Assert.Empty(results);
    }
}