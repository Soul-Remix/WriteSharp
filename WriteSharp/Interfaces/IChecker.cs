using WriteSharp.Checks;

namespace WriteSharp.Interfaces;

public interface IChecker
{
    public List<CheckResult> Check(string text);
}