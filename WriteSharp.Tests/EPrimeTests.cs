using System.Collections.Generic;
using WriteSharp.Checks;
using Xunit;

namespace WriteSharp.Test;

public class EPrimeTests
{
    private readonly EPrime _ePrime;

    private const string NonEWordSentence = "CSharp is awesome";
    private const string WasSentence = "So the cat was stolen";
    private const string ItIsSentence = "It's simple to use";
    private const string IamSentence = "I'm not sure where to deliver the pizza";
    private const string GoodSentence = "Welcome to the jungle";
    private const string MultipleEPrimeSentence = IamSentence + " because my map was stolen";
    private const string Reason = "is a form of \"to be\"";

    public EPrimeTests()
    {
        _ePrime = new EPrime();
    }

    [Fact]
    public void EPrime_NonEWord()
    {
        List<CheckResult> results = _ePrime.Check(NonEWordSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 7,
                Offset = 2,
                Reason = "\"is\" " + Reason
            }
        };
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void EPrime_DetectWas()
    {
        List<CheckResult> results = _ePrime.Check(WasSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 11,
                Offset = 3,
                Reason = "\"was\" " + Reason
            }
        };
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void EPrime_DetectItIs()
    {
        List<CheckResult> results = _ePrime.Check(ItIsSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 4,
                Reason = "\"It's\" " + Reason
            }
        };
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void EPrime_DetectIAm()
    {
        List<CheckResult> results = _ePrime.Check(IamSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 3,
                Reason = "\"I'm\" " + Reason
            }
        };
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void EPrime_GoodSentence()
    {
        List<CheckResult> results = _ePrime.Check(GoodSentence);
        List<CheckResult> expected = new List<CheckResult>();
        
        Assert.Equal(expected,results);
    }
    
    [Fact]
    public void EPrime_DetectMultiple()
    {
        List<CheckResult> results = _ePrime.Check(MultipleEPrimeSentence);
        List<CheckResult> expected = new List<CheckResult>()
        {
            new()
            {
                Index = 0,
                Offset = 3,
                Reason = "\"I'm\" " + Reason
            },
            new()
            {
                Index = 55,
                Offset = 3,
                Reason = "\"was\" " + Reason
            }
        };
        Assert.Equal(expected,results);
    }
}