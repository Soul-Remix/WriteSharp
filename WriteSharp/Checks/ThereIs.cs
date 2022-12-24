using System.Text.RegularExpressions;
using WriteSharp.Interfaces;
using WriteSharp.Types;

namespace WriteSharp.Checks;

internal class ThereIs : IChecker
{
    private readonly Regex _newSentenceRegex;
    private readonly Regex _startWithThereIsRegex;

    public ThereIs()
    {
        _newSentenceRegex = new Regex("([^\n\\.;!?]+)([\\.;!?]+)", RegexOptions.IgnoreCase);
        _startWithThereIsRegex = new Regex("^(\\s)*there\\b\\s(is|are)\\b", RegexOptions.IgnoreCase);
    }

    public List<CheckResult> Check(string text)
    {
        List<CheckResult> results = new List<CheckResult>();

        foreach (Match sentence in _newSentenceRegex.Matches(text))
        {
            Match startWithIs = _startWithThereIsRegex.Match(sentence.Value);
            if (startWithIs.Success)
            {
                CheckResult result = new CheckResult()
                {
                    Index = startWithIs.Index + sentence.Index,
                    Offset = startWithIs.Length,
                    Reason = $"\"{startWithIs.Value}\" is unnecessary verbiage"
                };
                results.Add(result);
            }
        }

        return results;
    }
}