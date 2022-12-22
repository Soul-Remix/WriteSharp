using WriteSharp.Checks;

namespace WriteSharp.Interfaces;

internal interface IChecker
{
    public List<CheckResult> Check(string text);
}