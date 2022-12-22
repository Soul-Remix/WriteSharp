using System.Collections.Generic;
using WriteSharp.Checks;
using Xunit;

namespace WriteSharp.Test;

public class AdverbWhereTest
{
    private readonly AdverbWhere _adverbWhere;

    private const string BadWordInSentence = "Allegedly, this sentence is terrible.";
    private const string GoodSentence = "The good dog jumps over the bad cat.";
    private const string VagueSentence = "We are writing about things and stuff.";

    public AdverbWhereTest()
    {
        _adverbWhere = new AdverbWhere();
    }

    [Fact]
    public void NotEscapeNotice()
    {
        List<CheckResult> results = _adverbWhere.Check(BadWordInSentence);
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
    public void ShortSentence()
    {
        List<CheckResult> results = _adverbWhere.Check(GoodSentence);
        List<CheckResult> expected = new List<CheckResult>();

        Assert.Equal(expected, results);
    }

    [Fact]
    public void VagueConstruct()
    {
        List<CheckResult> results = _adverbWhere.Check(VagueSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 21,
                Offset = 6,
                Reason = "\"things\" can weaken meaning"
            },
            new()
            {
                Index = 32,
                Offset = 5,
                Reason = "\"stuff\" can weaken meaning"
            }
        };

        Assert.Equal(expected, results);
    }
}