namespace WriteSharp.Checks;

public record CheckResult
{
    public String Reason { get; init; } = "";
    public int Index { get; set; }
    public int Offset { get; set; }

    
}