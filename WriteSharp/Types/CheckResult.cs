namespace WriteSharp.Types;

public record CheckResult
{
    public String Reason { get; set; } = "";
    public int Index { get; set; }
    public int Offset { get; set; }
}