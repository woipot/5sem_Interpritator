using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using static Interpritator.Source.Interpritator.NumberCommand;
using static Interpritator.Source.Interpritator.OperationsInfo;

namespace Interpritator.Source.Interpritator
{
    public static class Compiler
    {
        public static void SaveToBinFile(string patch, RichTextBox dataInput)
        {
            var textRange = new TextRange(
                dataInput.Document.ContentStart,
                dataInput.Document.ContentEnd
            );

            var separators = new []{';'};
            var commandsArr = textRange.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var file = File.Open(patch, FileMode.Create);
            var binaryWriter = new BinaryWriter(file);

            var counter = 1;
            foreach (var command in commandsArr)
            {
                try
                {
                    var bitCommand = CommandToBit(command);
                    foreach (bool bit in bitCommand)
                    {
                        binaryWriter.Write(bit);
                    }
                }
                catch (CompilerException ce)
                {
                    ce.CommandNumber = counter;
                    throw;
                }
                counter++;
            }

            binaryWriter.Dispose();
            file.Dispose();
        }

        private static BitArray CommandToBit(string command)
        {
            var resultCommand = new NumberCommand();

            var separators = new[] {' '};
            var splitedCommand = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if(splitedCommand.Length > 4)
                throw new CompilerException(command, "Command has more than 4 parts");

            for (var i = 0; i < splitedCommand.Length - 1; i++)
            {
                var operand = splitedCommand[i];

                try
                {
                    var bitPart = OperandToBit(operand);
                    resultCommand.SetOperand((uint)i+1, bitPart);
                }
                catch (CompilerException ce)
                {
                    ce.WrongCommand += " in " + command;
                    throw;
                }
            }

            var operatorInStr = splitedCommand.Last();

            var bitOperator = OperatorToBit(operatorInStr);

            resultCommand.SetOperator(bitOperator);

            return resultCommand.GetBitArr();
        }

        private static BitArray OperandToBit(string part)
        {

            var isCorrect = int.TryParse(part, out var intpart);

            if (!isCorrect) throw new CompilerException(part, "Operand is not number");
            if (intpart < 0 || intpart >= 512) throw new CompilerException(part, "Operand value out of range");

           
            var result = IntToBitArr(intpart);

            return result;
        }

        private static BitArray OperatorToBit(string strOperator)
        {
            var index = 0;
            try
            {
                index = OperationsName.BinarySearch(strOperator);
            }
            catch (Exception)
            {
                throw new CompilerException(strOperator, "Incorrect operator");
            }

            var bitOperator = IntToBitArr(index, OperatorSize);

            return bitOperator;
        }

        private static BitArray IntToBitArr(int number, int size = 9)
        {
            var result = new BitArray(size, false);

            var inString = Convert.ToString(number, 2);

            var counter = size - 1; 
            for (var i = inString.Length - 1; i >= 0; i--)
            {
                var boolResult = inString[i] == '1';

                result[counter] = boolResult; 

                counter--;
                if (counter < 0)
                    break;
            }

            return result;
        }
        
    }
}
