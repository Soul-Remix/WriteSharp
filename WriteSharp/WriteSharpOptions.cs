using WriteSharp.Enums;

namespace WriteSharp;

public class WriteSharpOptions
{
    private readonly Dictionary<CheckTypeEnum, bool> _checks;

    public bool AdverbWhere
    {
        get => _checks[CheckTypeEnum.AdverbWhere];
        set => _checks[CheckTypeEnum.AdverbWhere] = value;
    }

    public bool Duplicates
    {
        get => _checks[CheckTypeEnum.Duplicates];
        set => _checks[CheckTypeEnum.Duplicates] = value;
    }

    public bool EPrime
    {
        get => _checks[CheckTypeEnum.EPrime];
        set => _checks[CheckTypeEnum.EPrime] = value;
    }

    public bool NoCliches
    {
        get => _checks[CheckTypeEnum.NoCliches];
        set => _checks[CheckTypeEnum.NoCliches] = value;
    }

    public bool PassiveVoice
    {
        get => _checks[CheckTypeEnum.PassiveVoice];
        set => _checks[CheckTypeEnum.PassiveVoice] = value;
    }

    public bool StartWithSo
    {
        get => _checks[CheckTypeEnum.StartWithSo];
        set => _checks[CheckTypeEnum.StartWithSo] = value;
    }

    public bool ThereIs
    {
        get => _checks[CheckTypeEnum.ThereIs];
        set => _checks[CheckTypeEnum.ThereIs] = value;
    }

    public bool TooWordy
    {
        get => _checks[CheckTypeEnum.TooWordy];
        set => _checks[CheckTypeEnum.TooWordy] = value;
    }

    public bool WeaselWords
    {
        get => _checks[CheckTypeEnum.WeaselWords];
        set => _checks[CheckTypeEnum.WeaselWords] = value;
    }

    public List<String> WhiteList { get; set; } = new List<string>();

    public WriteSharpOptions()
    {
        _checks = new Dictionary<CheckTypeEnum, bool>()
        {
            { CheckTypeEnum.AdverbWhere, true },
            { CheckTypeEnum.Duplicates, true },
            { CheckTypeEnum.EPrime, false },
            { CheckTypeEnum.NoCliches, true },
            { CheckTypeEnum.PassiveVoice, true },
            { CheckTypeEnum.StartWithSo, true },
            { CheckTypeEnum.ThereIs, true },
            { CheckTypeEnum.TooWordy, true },
            { CheckTypeEnum.WeaselWords, true },
        };
    }

    public void AddToWhiteLIst(string word)
    {
        WhiteList.Add(word);
    }

    public void RemoveFromWhiteList(string word)
    {
        WhiteList.Remove(word);
    }

    internal Dictionary<CheckTypeEnum, bool> GetChecks()
    {
        return _checks;
    }
}