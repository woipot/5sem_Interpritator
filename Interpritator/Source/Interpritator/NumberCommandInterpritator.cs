using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interpritator.Source.Extension;
using Interpritator.Source.Interpritator.Command;

namespace Interpritator.Source.Interpritator
{
    public class NumberCommandInterpritator
    {
        private static List<Func<NumberCommand, object>> _commandList = new List<Func<NumberCommand, object>>
        {
            NumberCommand.GetOperandsList,
            NumberCommand.NotFirst,
            NumberCommand.Or,
            NumberCommand.And,
            NumberCommand.Xor,
            NumberCommand.Impication,
            NumberCommand.CoImpication,
            NumberCommand.Equivalence,
            NumberCommand.Pierce,
            NumberCommand.Scheffer,
            NumberCommand.Addition,
            NumberCommand.Subtraction,
            NumberCommand.Multiplication,
            NumberCommand.StrongDivision,
            NumberCommand.Mod,
            NumberCommand.Swap,
            NumberCommand.Insert,
            NumberCommand.Convert,
            ReadInBase,
            NumberCommand.FindMaxDivider,
            NumberCommand.ShiftL,
            NumberCommand.ShiftR,
            NumberCommand.CycleShiftL,
            NumberCommand.CycleShiftR,
            NumberCommand.Copy
        };

        public static KeyValuePair<string, string> StartProgram(string binFilePatch)
        {
            var binFile = File.Open(binFilePatch, FileMode.Open);


            var result = new StringBuilder();
            var errors = new StringBuilder();

            using (var br = new BinaryReader(binFile))
            {
                while (br.PeekChar() > -1)
                {
                    var intCommand = br.ReadInt32();
                    var bitCommand = BitArrayExtension.IntToBitArr(intCommand);

                    try
                    {
                       
                        var command = new NumberCommand(bitCommand);
                        var commandBefore = new NumberCommand(bitCommand);

                        var commandResult = GetCommandResult(command);

                        result.Append(commandBefore + " ---> " + commandResult+"\n");
                    }
                    catch (Exception e)
                    {
                        errors.Append(e + "\n");
                    }


                }
            }

            binFile.Dispose();

            return new KeyValuePair<string, string>(result.ToString(), errors.ToString());
        }

        public static string GetCommandResult(NumberCommand command)
        {
            var operation = command.GetOperator();
            var operationNumber = operation.ToInt();

            string result;

            var resultObj = _commandList[operationNumber].Invoke(command);
            var isNumberCommandResult = resultObj is NumberCommand;

            if (isNumberCommandResult)
            {
                var numCommandResult = (NumberCommand) resultObj;
                result = numCommandResult.ToString();
            }
            else
            {
                result = resultObj as string;
            }

            return result;
        }

        public static string GetBinCommandResult(NumberCommand command)
        {
            var operation = command.GetOperator();
            var operationNumber = operation.ToInt();

            string result;

            var resultObj = _commandList[operationNumber].Invoke(command);
            var isNumberCommandResult = resultObj is NumberCommand;

            if (isNumberCommandResult)
            {
                var numCommandResult = (NumberCommand)resultObj;
                result = numCommandResult.ToBinStr();
            }
            else
            {
                result = resultObj as string;
            }

            return result;
        }



        private static string ReadInBase(NumberCommand command)
        {
            throw new NotImplementedException();
        }

        public static KeyValuePair<string, string> RunCommand(BitArray bitCommand)
        {
            var result = new StringBuilder();
            var errors = new StringBuilder();

            try
            {
                var command = new NumberCommand(bitCommand);

                var commandResult = GetBinCommandResult(command);

                result.Append(bitCommand.ConvertToString() + " ---> " + commandResult + "\n");
            }
            catch (Exception e)
            {
                errors.Append(e + "\n");
            }

            return new KeyValuePair<string, string>(result.ToString(), errors.ToString());
        }
    }
}