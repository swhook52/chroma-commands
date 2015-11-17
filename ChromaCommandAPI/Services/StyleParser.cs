using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using ChromaCommand.Dto;

namespace ChromaCommand.Api.Services
{
    public class StyleParser
    {
        public ChromaStyle Parse(string rawCommand)
        {
            var style = new ChromaStyle();
            var separator = new[] {" THEN "};
            var stringCommands = rawCommand.ToUpper().Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var commands = new List<StyleCommand>();
            foreach (var stringCommand in stringCommands)
            {
                var commandParts = stringCommand.Split(' ');
                commands.Add(CreateStyleFromParts(commandParts));
            }
            style.Commands = commands.ToArray();
            return style;
        }

        private StyleCommand CreateStyleFromParts(string[] commandParts)
        {
            var action = GetChromaAction(commandParts[0]);
            switch (action)
            {
                case ChromaAction.Pulse:
                    // Pulse #FF0000
                    return new StyleCommand
                    {
                        Action = ChromaAction.Pulse,
                        Color = GetColor(commandParts[1])
                    };
                case ChromaAction.Clear:
                    // Clear
                    return new StyleCommand
                    {
                        Action = ChromaAction.Clear
                    };
                case ChromaAction.Spell:
                    // Spell word
                    return new StyleCommand
                    {
                        Action = ChromaAction.Spell,
                        Parameter = commandParts[1].ToLower()
                    };
                case ChromaAction.Wait:
                    // Wait 1000
                    return new StyleCommand
                    {
                        Action = ChromaAction.Wait,
                        Parameter = commandParts[1]
                    };
                case ChromaAction.Static:
                    // Static #FF0000
                    return new StyleCommand
                    {
                        Action = ChromaAction.Static,
                        Color = GetColor(commandParts[1])
                    };
                case ChromaAction.Reactive:
                    // Reactive #FF0000
                    return new StyleCommand
                    {
                        Action = ChromaAction.Reactive,
                        Color = GetColor(commandParts[1])
                    };
                case ChromaAction.Wave:
                    // Static #FF0000
                    return new StyleCommand
                    {
                        Action = ChromaAction.Wave,
                        Parameter = commandParts[1]
                    };
                case ChromaAction.Cycle:
                    // Cycle
                    return new StyleCommand
                    {
                        Action = ChromaAction.Cycle
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Color GetColor(string hex)
        {
            var hexMatch = Regex.Match(hex, "#([0-9|A-F]{2})([0-9|A-F]{2})([0-9|A-F]{2})");
            return Color.FromArgb(
                1,
                Convert.ToInt32(hexMatch.Groups[1].Value, 16),
                Convert.ToInt32(hexMatch.Groups[2].Value, 16),
                Convert.ToInt32(hexMatch.Groups[3].Value, 16));
        }

        private ChromaAction GetChromaAction(string action)
        {
            switch (action.ToUpper())
            {
                case "PULSE":
                    return ChromaAction.Pulse;
                case "CLEAR":
                    return ChromaAction.Clear;
                case "SPELL":
                    return ChromaAction.Spell;
                case "WAIT":
                    return ChromaAction.Wait;
                case "STATIC":
                    return ChromaAction.Static;
                case "REACTIVE":
                    return ChromaAction.Reactive;
                case "WAVE":
                    return ChromaAction.Wave;
                case "CYCLE":
                    return ChromaAction.Cycle;
                default:
                    throw new Exception($"Invalid Chroma action: {action}");
            }
        }
    }
}