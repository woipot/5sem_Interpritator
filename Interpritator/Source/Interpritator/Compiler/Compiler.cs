﻿using System;
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
        #region File Operations

        public static void SaveToBinFile(string patch, RichTextBox dataInput)
        {
            var textRange = new TextRange(
                dataInput.Document.ContentStart,
                dataInput.Document.ContentEnd
            );

            var separators = new[] { ';' };
            var commandsArr = textRange.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);


            var file = File.Open(patch, FileMode.Create);
            var binaryWriter = new BinaryWriter(file);

            var counter = 1;
            foreach (var command in commandsArr)
            {
                if (command == commandsArr.Last())
                    break;

                try
                {
                    var bitCommand = CommandToBit(command);

                    var inInt = bitCommand.ToInt();
                    binaryWriter.Write(inInt);

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

        public static void DecodeBinFile(string patch, RichTextBox dataOutput)
        {
            var binFile = File.Open(patch, FileMode.Open);

            using (var br = new BinaryReader(binFile))
            {
                while (br.PeekChar() > -1)
                {
                    var byteCommand = br.ReadInt32();
                    var command = new NumberCommand(BitArrayExtension.IntToBitArr(byteCommand));

                    dataOutput.AppendText(command.ToString() + ";\n");
                }
            }

            binFile.Dispose();
        }

        #endregion


        #region ToBit Functions

        private static BitArray CommandToBit(string command)
        {
            var resultCommand = new NumberCommand();

            var separators = new[] { ' ' };
            var splitedCommand = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (splitedCommand.Length > 4)
                throw new CompilerException(command, "Command has more than 4 parts");

            for (var i = 0; i < splitedCommand.Length - 1; i++)
            {
                var operand = splitedCommand[i];

                try
                {
                    var bitPart = OperandToBit(operand);
                    var operandNumber = OperandCount - i;
                    resultCommand.SetOperand((uint)operandNumber, bitPart);
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

            var result = BitArrayExtension.IntToBitArr(intpart, OperandSize);

            return result;
        }

        private static BitArray OperatorToBit(string strOperator)
        {
            var index = 0;

            index = OperationsName.IndexOf(strOperator);
            if (index < 0)
                throw new CompilerException(strOperator, "Incorrect operator");

            var bitOperator = BitArrayExtension.IntToBitArr(index, OperatorSize);

            return bitOperator;
        }

        #endregion
        
    }
}
