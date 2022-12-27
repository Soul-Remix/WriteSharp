using WriteSharp.Types;

namespace WriteSharp.Interfaces;

public interface IWriteSharp
{
    public List<CheckResult> Check(string text, WriteSharpOptions? options = null);
}