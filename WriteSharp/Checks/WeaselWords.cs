using System.Text.RegularExpressions;
using WriteSharp.Interfaces;

namespace WriteSharp.Checks;

internal class WeaselWords : IChecker
{
    private readonly Regex _regex;

    public WeaselWords()
    {
        string[] weasels =
        {
            "most",
            "many",
            "various",
            "some",
            "a lot",
            "several",
            "few",
            "many",
            "often",
            "probably",
            "huge",
            "tiny",
            "very",
            "fairly",
            "extremely",
            "exceedingly",
            "quite",
            "remarkably",
            "surprisingly",
            "mostly",
            "largely",
            "is a number",
            "are a number",
            "excellent",
            "interestingly",
            "significantly",
            "substantially",
            "clearly",
            "vast",
            "relatively",
            "completely",
            "literally",
            "not rocket science",
            "outside the box",
            "expert",
            "experts",
            "helps",
            "reportedly",
            "arguably",
            "linked to",
            "supports",
            "useful",
            "better",
            "improved",
            "gains",
            "acts",
            "works",
            "effective",
            "efficient",
            "seems",
            "appears",
            "looks",
            "is like",
            "virtually",
            "lots",
            "almost",
            "could",
            "combats",
            "felt",
            "heard",
            "saw",
            "knew",
            "realized",
            "realised",
            "wanted",
            "thought",
            "noticed",
            "seemed",
            "decided",
            "looked",
            "understood",
            "considered",
            "believed",
            "appeared",
            "watched",
            "smelled",
            "touched",
            "wondered",
            "recognized",
            "recognised",
            "wished",
            "supposed",
            "about",
            "just",
            "really",
            "started",
            "began",
            "all",
            "again",
            "that",
            "so",
            "then",
            "rather",
            "only",
            "like",
            "close",
            "even",
            "somehow",
            "sort",
            "pretty",
            "back",
            "up",
            "down",
            "anyway",
            "real",
            "already",
            "own",
            "over",
            "ever",
            "be able to",
            "still",
            "bit",
            "far",
            "also",
            "enough",
            "might"
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