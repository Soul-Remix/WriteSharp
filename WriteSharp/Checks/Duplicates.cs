using System.Text.RegularExpressions;
using WriteSharp.Interfaces;
using WriteSharp.Types;

namespace WriteSharp.Checks;

public class Duplicates : IChecker
{
    private readonly Regex _regex;

    public Duplicates()
    {
        _regex = new Regex(@"\b(\w+?)\s\1\b", RegexOptions.IgnoreCase);
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
                Reason = $"\"{match.Value}\" is a duplicate"
            };
            results.Add(result);
        }

        return results;
    }
}