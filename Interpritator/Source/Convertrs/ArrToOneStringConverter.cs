using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace Interpritator.Source.Convertrs
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

        public static IEnumerable<string> ConvertBack(string value)
        {
            var commands = FromsStringtoCommands(System.Convert.ToString(value));
            return new ObservableCollection<string>(commands);
        }

        private static IEnumerable<string> FromsStringtoCommands(string text)
        {
            var commands = new List<string>();
            var parsedString = text.Split(new[]{'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            commands.AddRange(parsedString);

            return commands;
        }
    }
}
