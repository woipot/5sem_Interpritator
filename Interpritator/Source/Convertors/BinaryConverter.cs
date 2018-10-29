using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Interpritator.Source.Extension;
using Interpritator.Source.Interpritator.Command.Operations;

namespace Interpritator.Source.Convertors
{
    class BinaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text)) return value;

            return MultiConvert(text, true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text)) return value;

            return MultiConvert(text, false);
        }

        private object MultiConvert(string text, bool toBinConvert)
        {
            var sbResult = new StringBuilder();

            var stringCommands = ArrToOneStringConverter.ConvertBack(text, false);

            var counter = 0;
            foreach (var stringCommand in stringCommands)
            {
                var resultList = ToExecutedList(stringCommand.Trim(), toBinConvert);

                foreach (var pair in resultList)
                {
                    sbResult.Append(pair.Value + " ");
                }

                if (counter < stringCommands.Count()-1)
                {
                    sbResult.Append("\r\n");
                }
                counter++;
            }

            if (text[text.Length - 1] == '\n')
                sbResult.Append("\r\n");


            return sbResult.ToString();
        }

        private IEnumerable<KeyValuePair<string, string>> ToExecutedList(string command, bool toBin)
        {
            var result = new List<KeyValuePair<string, string>>(); 

            var splitedCommand = command.Split(' ');

            for (var i = 0; i < splitedCommand.Length; i++)
            {
                var part = splitedCommand[i];

                string procecedPart;

                if (i == splitedCommand.Length - 1)
                {
                    procecedPart = toBin ? PartToBin(part) : BinToStrOperator(part);
                }
                else
                {
                    procecedPart = toBin ? PartToBin(part) : BinToStrOperand(part);
                }
                    

                var resultPair = new KeyValuePair<string, string>(part, procecedPart);
                result.Add(resultPair);
            }


            return result;
        }

        private string PartToBin(string strPart)
        {
            var index = OperationsInfo.OperationsName.IndexOf(strPart);

            if (index >= 0)
                return System.Convert.ToString(index, 2);

            var isNumber = int.TryParse(strPart, out var intResult);

            return isNumber ? System.Convert.ToString(intResult, 2) : strPart;
        }

        private string BinToStrOperator(string binPart)
        {
            try
            {
                var index = System.Convert.ToInt32(binPart, 2);
                return index < OperationsInfo.OperationsName.Count && index > 0
                    ? OperationsInfo.OperationsName[index]
                    : index.ToString();
            }
            catch(Exception)
            {
                return binPart;
            }
        }

        private string BinToStrOperand(string binPart)
        {
            try
            {
                var index = System.Convert.ToInt32(binPart, 2);
                return index.ToString();
            }
            catch (Exception)
            {
                return binPart;
            }
        }

    }
}