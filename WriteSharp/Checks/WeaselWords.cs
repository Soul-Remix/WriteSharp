using System.Text.RegularExpressions;
using WriteSharp.Interfaces;
using WriteSharp.Types;

namespace WriteSharp.Checks;

internal class WeaselWords : IChecker
{
    private readonly Regex _regex;

    public WeaselWords()
    {
        string[] weasels =
        {
            "clearly",
            "completely",
            "exceedingly",
            "excellent",
            "extremely",
            "fairly",
            "few",
            "huge",
            "interestingly",
            "is a number",
            "largely",
            "many",
            "mostly",
            "obviously",
            "quite",
            "relatively",
            "remarkably",
            "several",
            "significantly",
            "substantially",
            "surprisingly",
            "tiny",
            "usually",
            "various",
            "vast",
            "very",
            "With all due respect",
            "That being said",
            "Leading",
            "Cutting-edge",
            "Could be",
            "I would say that",
            "Research shows",
            "Experts say",
            "Probably",
            "Possibly"
        };

        string weaselString = string.Join("|", weasels);
        _regex = new Regex($"\\b({weaselString})\\b", RegexOptions.IgnoreCase);
    }

    public List<CheckResult> Check(string text)
    {
        List<CheckResult> results = new List<CheckResult>();

        foreach (Match match in _regex.Matches(text))
        {
            string matchValueLower = match.Value.ToLower();

            if ((matchValueLower.Equals("many") || matchValueLower.Equals("few")) &&
                text.Substring(match.Index - 4, 3).ToLower().Equals("too"))
            {
                continue;
            }

            CheckResult result = new CheckResult()
            {
                Index = match.Index,
                Offset = match.Length,
                Reason = $"\"{match.Value}\" is a weasel word"
            };
            results.Add(result);
        }

        return results;
    }
}