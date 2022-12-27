using WriteSharp.Checks;
using WriteSharp.Enums;
using WriteSharp.Interfaces;
using WriteSharp.Types;

namespace WriteSharp;

public class WriteSharp : IWriteSharp
{
    private readonly Dictionary<CheckTypeEnum, IChecker> _checks = new()
    {
        { CheckTypeEnum.AdverbWhere, new AdverbWhere() },
        { CheckTypeEnum.Duplicates, new Duplicates() },
        { CheckTypeEnum.EPrime, new EPrime() },
        { CheckTypeEnum.NoCliches, new NoCliches() },
        { CheckTypeEnum.PassiveVoice, new PassiveVoice() },
        { CheckTypeEnum.StartWithSo, new StartWithSo() },
        { CheckTypeEnum.ThereIs, new ThereIs() },
        { CheckTypeEnum.TooWordy, new TooWordy() },
        { CheckTypeEnum.WeaselWords, new WeaselWords() },
    };

    public WriteSharpOptions Options { get; set; } = new();

    public List<CheckResult> Check(string text, WriteSharpOptions? options = null)
    {
        options ??= Options;

        IEnumerable<CheckResult> results = new List<CheckResult>();
        var checks = options.GetChecks();

        if (string.IsNullOrWhiteSpace(text))
        {
            return results.ToList();
        }

        foreach (CheckTypeEnum key in _checks.Keys)
        {
            if (checks[key])
            {
                List<CheckResult> checkerResult = _checks[key].Check(text);
                results = results.Concat(checkerResult);
            }
        }

        return Combine(Filter(results,options.WhiteList)).OrderBy(x => x.Index).ToList();
    }

    private IEnumerable<CheckResult> Filter(IEnumerable<CheckResult> list,List<string> whitelist)
    {
        if (whitelist.Count == 0)
        {
            return list;
        }

        return list.Where(x => !whitelist.Contains(x.Reason.Substring(1, x.Offset)));
    }

    private IEnumerable<CheckResult> Combine(IEnumerable<CheckResult> list)
    {
        Dictionary<String, CheckResult> results = new Dictionary<string, CheckResult>();

        foreach (var item in list)
        {
            string key = $"{item.Index}:{item.Offset}";
            if (results.TryGetValue(key, out CheckResult? value))
            {
                value.Reason += $" and {item.Reason.Substring(item.Offset + 3)}";
            }
            else
            {
                results.Add(key, item);
            }
        }

        return results.Values;
    }
}