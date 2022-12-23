using System.Text.RegularExpressions;
using WriteSharp.Interfaces;

namespace WriteSharp.Checks;

public class AdverbWhere: IChecker
{
    private readonly Regex _regex;

    public AdverbWhere()
    {
        string[] adverbs =
        {
            "absolutely",
            "accidentally",
            "additionally",
            "allegedly",
            "alternatively",
            "angrily",
            "anxiously",
            "approximately",
            "awkwardly",
            "badly",
            "barely",
            "beautifully",
            "blindly",
            "boldly",
            "bravely",
            "brightly",
            "briskly",
            "bristly",
            "bubbly",
            "busily",
            "calmly",
            "carefully",
            "carelessly",
            "cautiously",
            "cheerfully",
            "clearly",
            "closely",
            "coldly",
            "completely",
            "consequently",
            "correctly",
            "courageously",
            "crinkly",
            "cruelly",
            "crumbly",
            "cuddly",
            "currently",
            "daringly",
            "deadly",
            "definitely",
            "deliberately",
            "doubtfully",
            "dumbly",
            "eagerly",
            "early",
            "easily",
            "elegantly",
            "enormously",
            "enthusiastically",
            "equally",
            "especially",
            "eventually",
            "exactly",
            "exceedingly",
            "exclusively",
            "extremely",
            "fairly",
            "faithfully",
            "fatally",
            "fiercely",
            "finally",
            "fondly",
            "foolishly",
            "fortunately",
            "frankly",
            "frantically",
            "generously",
            "gently",
            "giggly",
            "gladly",
            "gracefully",
            "greedily",
            "happily",
            "hardly",
            "hastily",
            "healthily",
            "heartily",
            "helpfully",
            "honestly",
            "hourly",
            "hungrily",
            "hurriedly",
            "immediately",
            "impatiently",
            "inadequately",
            "ingeniously",
            "innocently",
            "inquisitively",
            "interestingly",
            "irritably",
            "jiggly",
            "joyously",
            "justly",
            "kindly",
            "largely",
            "lately",
            "lazily",
            "likely",
            "literally",
            "lonely",
            "loosely",
            "loudly",
            "loudly",
            "luckily",
            "madly",
            "many",
            "mentally",
            "mildly",
            "mortally",
            "mostly",
            "mysteriously",
            "neatly",
            "nervously",
            "noisily",
            "normally",
            "obediently",
            "occasionally",
            "only",
            "openly",
            "painfully",
            "particularly",
            "patiently",
            "perfectly",
            "politely",
            "poorly",
            "powerfully",
            "presumably",
            "previously",
            "promptly",
            "punctually",
            "quarterly",
            "quickly",
            "quietly",
            "rapidly",
            "rarely",
            "really",
            "recently",
            "recklessly",
            "regularly",
            "relatively",
            "reluctantly",
            "remarkably",
            "repeatedly",
            "rightfully",
            "roughly",
            "rudely",
            "sadly",
            "safely",
            "selfishly",
            "sensibly",
            "seriously",
            "sharply",
            "shortly",
            "shyly",
            "significantly",
            "silently",
            "simply",
            "sleepily",
            "slowly",
            "smartly",
            "smelly",
            "smoothly",
            "softly",
            "solemnly",
            "sparkly",
            "speedily",
            "stealthily",
            "sternly",
            "stupidly",
            "substantially",
            "successfully",
            "suddenly",
            "surprisingly",
            "suspiciously",
            "swiftly",
            "tenderly",
            "tensely",
            "thoughtfully",
            "tightly",
            "timely",
            "truthfully",
            "unexpectedly",
            "unfortunately",
            "usually",
            "very",
            "victoriously",
            "violently",
            "vivaciously",
            "warmly",
            "waverly",
            "weakly",
            "wearily",
            "wildly",
            "wisely",
            "worldly",
            "wrinkly"
        };

        string[] weakens =
        {
            "just",
            "maybe",
            "stuff",
            "things"
        };
        
        string adverbString = String.Join("|", adverbs);
        string weakensString = String.Join("|", weakens);
        _regex = new Regex($"\\b(({adverbString})|({weakensString}))\\b", RegexOptions.IgnoreCase);
    }

    public List<CheckResult> Check(string text)
    {
        List<CheckResult> results = new List<CheckResult>();

        foreach (Match match in _regex.Matches(text))
        {
            CheckResult result = new CheckResult()
            {
                Index = match.Index,
                Offset = match.Length,
                Reason = $"\"{match.Value}\" can weaken meaning"
            };
            results.Add(result);
        }

        return results;
    }
}