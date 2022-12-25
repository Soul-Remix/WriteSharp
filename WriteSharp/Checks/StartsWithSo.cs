using System.Text.RegularExpressions;
using WriteSharp.Interfaces;
using WriteSharp.Types;

namespace WriteSharp.Checks;

internal class StartWithSo : IChecker
{
    private readonly Regex _newSentenceRegex;
    private readonly Regex _startWithSoRegex;

    public StartWithSo()
    {
        _newSentenceRegex = new Regex("([^\n\\.;!?]+)([\\.;!?]+)", RegexOptions.IgnoreCase);
        _startWithSoRegex = new Regex("^(\\s)*so\\b[\\s\\S]", RegexOptions.IgnoreCase);
    }

    public List<CheckResult> Check(string text)
    {
        List<CheckResult> results = new List<CheckResult>();

        foreach (Match sentence in _newSentenceRegex.Matches(text))
        {
            Match startWithSo = _startWithSoRegex.Match(sentence.Value);
            if (startWithSo.Success)
            {
                if (startWithSo.Value.ToLower() == "so?")
                {
                    continue;
                }

                CheckResult result = new CheckResult()
                {
                    Index = startWithSo.Index + sentence.Index,
                    Offset = 2,
                    Reason = $"\"{startWithSo.Value.Trim()}\" adds no meaning"
                };
                results.Add(result);
            }
        }

        return results;
    }
}