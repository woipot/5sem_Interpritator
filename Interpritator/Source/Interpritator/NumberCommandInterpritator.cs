using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interpritator.Source.Interpritator.Command;

namespace Interpritator.Source.Interpritator
{
    public class NumberCommandInterpritator
    {
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
            var operationName = (Operations)operationNumber;

            string commandResult;

            switch (operationName)
            {
                case Operations.Show:
                    commandResult = command.GetOperandsList();
                    break;

                case Operations.NotFirst:
                    commandResult = command.NotFirst().ToString();
                    break;

                case Operations.Disjunction:
                    commandResult = command.Or().ToString();
                    break;

                case Operations.Сonjuction:
                    commandResult = command.And().ToString();
                    break;

                case Operations.Xor:
                    commandResult = command.Xor().ToString();
                    break;

                case Operations.Implication:
                    commandResult = command.Impication().ToString();
                    break;

                case Operations.Koimplication:
                    commandResult = command.CoImpication().ToString();
                    break;

                case Operations.Equivalence:
                    commandResult = command.Equivalence().ToString();
                    break;

                case Operations.ArrowPierce:
                    commandResult = command.Pierce().ToString();
                    break;

                case Operations.Scheffer:
                    commandResult = command.Scheffer().ToString();
                    break;

                case Operations.Addition:
                    commandResult = command.Addition().ToString();
                    break;

                case Operations.Subtraction:
                    commandResult = command.Subtraction().ToString();
                    break;

                case Operations.Multiplication:
                    commandResult = command.Multiplication().ToString();
                    break;

                case Operations.Division:
                    commandResult = command.StrongDivision().ToString();
                    break;

                case Operations.Mod:
                    commandResult = command.Mod().ToString();
                    break;

                case Operations.Swap:
                    commandResult = command.Swap().ToString();
                    break;

                case Operations.Insert:
                    commandResult = command.Insert().ToString();
                    break;

                case Operations.Convert:
                    commandResult = command.Convert();
                    break;

                case Operations.ConvertEx:
                    //TODO:: ввод с клавиатуры
                    commandResult = command.ReadInBase("2").ToString();
                    break;

                case Operations.MaxTwoDegree:
                    commandResult = command.FindMaxDivider().ToString();
                    break;

                case Operations.LeftShift:
                    commandResult = command.ShiftL().ToString();
                    break;

                case Operations.RightShift:
                    commandResult = command.ShiftR().ToString();
                    break;

                case Operations.CycleLeftShift:
                    commandResult = command.CycleShiftL().ToString();
                    break;

                case Operations.CecleLeftShift:
                    commandResult = command.CycleShiftR().ToString();
                    break;

                case Operations.Copy:
                    commandResult = command.Copy().ToString();
                    break;

                default:
                    throw new Exception("wrong command");
            }

            return commandResult;
        }

    }
}
