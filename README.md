# WriteSharp

Naive linter for English prose and a spell checker for developers who can't write good and wanna learn to do other stuff good too.    

**Important:** Do not use this tool to be a jerk to other people about their writing.    

## API


```csharp
using WriteSharp;

var writeSharp = new WriteSharp();

var suggestion = writeSharp.Check("Remarkably few developers write well.");

// suggestions:
//
// [{
//   Reason = "Remarkably" can weaken meaning and is a weasel word,
//   Index = 0,
//   Offset = 10
//
// },{
//   Reason = "few" is a weasel word,
//   Index = 11,
//   Offset = 3
// }]
```

`WriteSharp.Checks` takes an optional second argument that allows you to disable certain checks.

You can disable checking for passive voice like this:

```csharp
using WriteSharp;

var writeSharp = new WriteSharp();

var suggestion = writeSharp.Check("So the cat was stolen.", new WriteSharpOptions() { PassiveVoice = false });
// suggestions: []
```

You can also pass WriteSharpOption in WriteSharp constructor, this will overwrite the default options.

```csharp
using WriteSharp;

var writeSharpOptions = new WriteSharpOptions()
        {
            WeaselWords = false,
            AdverbWhere = false
        };

var writeSharp = new WriteSharp(writeSharpOptions);

var suggestion = writeSharp.Check("Remarkably few developers write well.");

// suggestions: []
```

You can use the WriteSharpOptions `WhiteList` property to pass in a list of strings to whitelist from suggestions.
For example, normally `only` would be picked up as a bad word to use, but you might want to exempt `read-only` from that:

```csharp
using WriteSharp;

var writeSharp = new WriteSharp();

var suggestion = writeSharp.Check("Never write read-only sentences.");
// suggestions:
//
// [{
//   Reason = ""only" can weaken meaning",
//   Index = 16,
//   Offset = 4
//
// }]

var filtered = writeSharp.Check("Never write read-only sentences.",
            new WriteSharpOptions() { WhiteList = { "read-only" } });
// filtered: []
```

## CLI
You can use `WriteSharp` as a command-line tool

```shell
ws "Remarkably few developers write well."
Reason = "Remarkably" can weaken meaning and is a weasel word, Index = 0, Offset = 10
Reason = "few" is a weasel word, Index = 11, Offset = 3
```

You can run just specific checks like this:
```shell
ws "Remarkably few developers write well." --checks=weasel,so
```

Or exclude checks like this:
```shell
ws "Remarkably few developers write well." --no-checks=weasel,so
```

## Checks

You can disable or enable any combination of the following checks

### `passive`
Checks for passive voice.

### `duplicates`
Checks for duplicates – cases where a word is repeated.

### `so`
Checks for `so` at the beginning of the sentence.

### `thereIs`
Checks for `there is` or `there are` at the beginning of the sentence.

### `weasel`
Checks for "weasel words."

### `adverb`
Checks for adverbs that can weaken meaning: really, very, extremely, etc.

### `tooWordy`
Checks for wordy phrases and unnecessary words.

### `cliches`
Checks for common cliches.

### `eprime`
Checks for ["to-be"](https://en.wikipedia.org/wiki/E-Prime) verbs. _Disabled by default_

#### Prose

* [Elements of Style](http://www.bartleby.com/141/)
* [Flesch–Kincaid readability](http://en.wikipedia.org/wiki/Flesch%E2%80%93Kincaid_readability_test)
* [Fear and Loathing of the English passive](http://www.lel.ed.ac.uk/~gpullum/passive_loathing.pdf)
* [Words to Avoid in Educational Writing](http://css-tricks.com/words-avoid-educational-writing/)
