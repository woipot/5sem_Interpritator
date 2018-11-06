using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Interpritator.Source.Convertors
{
    public static class ArrToOneStringConverter    {
        public static string Convert(IEnumerable<string> value)
        {
            var commandsList = new List<string>();
            if (value != null)
                commandsList.AddRange((ObservableCollection<string>) value);

            var result = string.Join("\r\n", commandsList);
            return result;
        }

        public static IEnumerable<string> ConvertBack(string value, bool needRemove = true)
        {
            var commands = FromsStringtoCommands(value, needRemove);
            return new ObservableCollection<string>(commands);
        }

        private static IEnumerable<string> FromsStringtoCommands(string text, bool needRemove = true)
        {
            var commands = new List<string>();
            var parsedString = needRemove
                ? text.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                : text.Split(new[] {"\r\n"}, StringSplitOptions.None);


            commands.AddRange(parsedString);

            return commands;
        }
    }
}
