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

        var checksOption = new Option<string>(
            name: "--checks",
            description: "Specify the checks you want to run, Only the tests you specified will run."
        );

        var noChecksOption = new Option<string>(
            name: "--no-checks",
            description:
            "Specify the checks you don't want to run, All the other checks will run. --no-checks will overwrite --checks"
        );

        var rootCommand = new RootCommand("WriteSharp is a linter for english prose and a spell checker");

        rootCommand.AddOption(whiteListOption);
        rootCommand.AddOption(checksOption);
        rootCommand.AddOption(noChecksOption);
        rootCommand.AddArgument(textArgument);

        rootCommand.SetHandler((whiteList, checkToDo, checksToSkip, text) =>
        {
            HandleWhiteList(whiteList);
            HandleCheck(checkToDo, false);
            HandleCheck(checksToSkip, true);
            Check(text);
        }, whiteListOption, checksOption, noChecksOption, textArgument);

        return await rootCommand.InvokeAsync(args);
    }

    private static void HandleWhiteList(string words)
    {
        List<string> list = string.IsNullOrWhiteSpace(words)
            ? new List<string>()
            : words.Split(",").ToList();

        Options.WhiteList = list;
    }

    private static void HandleCheck(string checks, bool checkValue)
    {
        if (string.IsNullOrWhiteSpace(checks))
        {
            return;
        }

        TurnChecks(checkValue);

        var toDo = checks.Split(",");
        foreach (var check in toDo)
        {
            string checkToLower = check.ToLower();
            if (checkToLower == "adverb")
                Options.AdverbWhere = !checkValue;
            else if (checkToLower == "duplicates")
                Options.Duplicates = !checkValue;
            else if (checkToLower == "eprime")
                Options.EPrime = !checkValue;
            else if (checkToLower == "cliche")
                Options.NoCliches = !checkValue;
            else if (checkToLower == "passive")
                Options.PassiveVoice = !checkValue;
            else if (checkToLower == "so")
                Options.StartWithSo = !checkValue;
            else if (checkToLower == "there-is")
                Options.ThereIs = !checkValue;
            else if (checkToLower == "wordy")
                Options.TooWordy = !checkValue;
            else if (checkToLower == "weasel") Options.WeaselWords = !checkValue;
        }
    }

    private static void TurnChecks(bool value)
    {
        Options.AdverbWhere = value;
        Options.Duplicates = value;
        Options.ThereIs = value;
        Options.EPrime = value;
        Options.NoCliches = value;
        Options.PassiveVoice = value;
        Options.TooWordy = value;
        Options.WeaselWords = value;
        Options.StartWithSo = value;
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