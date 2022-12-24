using System.Text.RegularExpressions;
using WriteSharp.Interfaces;
using WriteSharp.Types;

namespace WriteSharp.Checks;

internal class EPrime : IChecker
{
    private readonly Regex _regex;

    public EPrime()
    {
        string[] toBe =
        {
            "am",
            "are",
            "aren't",
            "be",
            "been",
            "being",
            "he's",
            "here's",
            "here's",
            "how's",
            "i'm",
            "is",
            "isn't",
            "it's",
            "she's",
            "that's",
            "there's",
            "they're",
            "was",
            "wasn't",
            "we're",
            "were",
            "weren't",
            "what's",
            "where's",
            "who's",
            "you're"
        };

        string toBeString = string.Join("|", toBe);
        _regex = new Regex($"\\b({toBeString})\\b", RegexOptions.IgnoreCase);
    }

    public List<CheckResult> Check(string text)
    {
        List<CheckResult> results = new List<CheckResult>();

        foreach (Match match in _regex.Matches(text))
        {
            CheckResult result = new CheckResult()
            {
                Index = match.Index,
                Offset = match.Length,
                Reason = $"\"{match.Value}\" is a form of \"to be\""
            };
            results.Add(result);
        }

        return results;
    }
}