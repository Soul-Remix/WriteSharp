namespace WriteSharp.API.DTO;

public class CheckOptionsDto
{
    public bool AdverbWhere { get; set; } = true;

    public bool Duplicates { get; set; } = true;

    public bool EPrime { get; set; } = true;

    public bool NoCliches { get; set; } = true;

    public bool PassiveVoice { get; set; } = true;

    public bool StartWithSo { get; set; } = true;

    public bool ThereIs { get; set; } = true;

    public bool TooWordy { get; set; } = true;

    public bool WeaselWords { get; set; } = true;
}