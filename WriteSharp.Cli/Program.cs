using System.CommandLine;

namespace WriteSharp.Cli;

public class Program
{
    private static readonly WriteSharpOptions Options = new WriteSharpOptions();

    static async Task<int> Main(string[] args)
    {
        var textArgument = new Argument<string>(
            name: "TEXT",
            description: "Text to check"
        );

        var whiteListOption = new Option<string>(
            name: "--white-list",
            description: "A whitelist of words to ignore in the checks"
        );

        var checksToDoOption = new Option<string>(
            name: "--checks",
            description: "Specify the checks you want to run"
        );

        var rootCommand = new RootCommand("WriteSharp is a linter for english prose and a spell checker");

        rootCommand.AddOption(whiteListOption);
        rootCommand.AddOption(checksToDoOption);
        rootCommand.AddArgument(textArgument);

        rootCommand.SetHandler((whiteList, checkToDo, text) =>
        {
            HandleWhiteList(whiteList);
            HandleCheckToDo(checkToDo);
            Check(text);
        }, whiteListOption, checksToDoOption, textArgument);

        return await rootCommand.InvokeAsync(args);
    }

    private static void HandleWhiteList(string words)
    {
        List<string> list = string.IsNullOrWhiteSpace(words)
            ? new List<string>()
            : words.Split(",").ToList();

        Options.WhiteList = list;
    }

    private static void HandleCheckToDo(string checks)
    {
        if (string.IsNullOrWhiteSpace(checks))
        {
            return;
        }

        TurnOffChecks();
        
        var toDo = checks.Split(",");
        foreach (var check in toDo)
        {
            string checkToLower = check.ToLower();
            if (checkToLower == "adverb")
                Options.AdverbWhere = true;
            else if (checkToLower == "duplicates")
                Options.Duplicates = true;
            else if (checkToLower == "eprime")
                Options.EPrime = true;
            else if (checkToLower == "cliche")
                Options.NoCliches = true;
            else if (checkToLower == "passive")
                Options.PassiveVoice = true;
            else if (checkToLower == "so")
                Options.StartWithSo = true;
            else if (checkToLower == "there-is")
                Options.ThereIs = true;
            else if (checkToLower == "wordy")
                Options.TooWordy = true;
            else if (checkToLower == "weasel") Options.WeaselWords = true;
        }
    }

    private static void TurnOffChecks()
    {
        Options.AdverbWhere = false;
        Options.Duplicates = false;
        Options.ThereIs = false;
        Options.EPrime = false;
        Options.NoCliches = false;
        Options.PassiveVoice = false;
        Options.TooWordy = false;
        Options.WeaselWords = false;
        Options.StartWithSo = false;
    }

    private static void Check(string text)
    {
        WriteSharp writeSharp = new WriteSharp()
        {
            Options = Options
        };

        foreach (var c in writeSharp.Check(text))
        {
            Console.WriteLine(c);
        }
    }
}