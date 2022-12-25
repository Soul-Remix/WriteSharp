namespace WriteSharp.Types;

public class CheckResult
{
    public String Reason { get; set; } = "";
    public int Index { get; set; }
    public int Offset { get; set; }

    public override string ToString()
    {
        return $"Reason = {Reason}, Index = {Index}, Offset = {Offset}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        return this.ToString().Equals(obj.ToString());
    }

    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }
}