using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Interpritator.Source.Interpritator
{
    public class NumberCommandInterpritator
    {
        public static string StartProgram(string binFilePatch)
        {
            var binFile = File.Open(binFilePatch, FileMode.Open);


            var result = new StringBuilder();
            using (var br = new BinaryReader(binFile))
            {
                while (br.PeekChar() > -1)
                {
                    var byteCommand = br.ReadBytes(4);
                    
                    var command = new NumberCommand(byteCommand);

                }
            }

            binFile.Dispose();

            return result.ToString();
        }

        public static string GetCommandResult(NumberCommand command)
        {
            var operation = command.GetOperator();
            var operationNumber = operation.ToInt();
            var operationName = (Operations)operationNumber;

            var result = new StringBuilder();

            switch (operationName)
            {
                case Operations.Show:
                    break;
                case Operations.NotFirst:
                    break;
                case Operations.Disjunction:
                    break;
                case Operations.Сonjuction:
                    break;
                case Operations.Xor:
                    break;
                case Operations.Implication:
                    break;
                case Operations.Koimplication:
                    break;
                case Operations.Equivalence:
                    break;
                case Operations.ArrowPierce:
                    break;
                case Operations.Addition:
                    break;
                case Operations.Subtraction:
                    break;
                case Operations.Multiplication:
                    break;
                case Operations.Division:
                    break;
                case Operations.Mod:
                    break;
                case Operations.Swap:
                    break;
                case Operations.Insert:
                    break;
                case Operations.Convert:
                    break;
                case Operations.ConvertEx:
                    break;
                case Operations.MaxTwoDegree:
                    break;
                case Operations.LeftShift:
                    break;
                case Operations.RightShift:
                    break;
                case Operations.CycleLeftShifE:
                    break;
                case Operations.CecleLeftShift:
                    break;
                case Operations.Copy:
                    break;
            }

            return result.ToString();
        }


        //#region Operations

        //public string GetOperandsList(NumberCommand command) //0 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);
        //    var thirdOperand = command.GetOperand(3);
        //    var operation = command.GetOperator();

        //    var bs = secondOperand.ToInt();

        //    if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

        //    var firstInNewBase = Convert.ToString(firstOperand.ToInt(), bs).ToUpper();
        //    var secondInNewBase = Convert.ToString(secondOperand.ToInt(), bs).ToUpper();
        //    var thirdInNewBase = Convert.ToString(thirdOperand.ToInt(), bs).ToUpper();
        //    var operationInNewBase = Convert.ToString(operation.ToInt(), bs).ToUpper();

        //    var result = $"{thirdInNewBase} {secondInNewBase} {firstInNewBase} {operationInNewBase}";

        //    return result;
        //}

        //public BitArray NotFirstToFirst(NumberCommand command) //1 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    firstOperand.Not();
        //    command.SetOperand(3, firstOperand);

        //    return firstOperand;
        //}

        //public BitArray Or(NumberCommand command) //2 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var result = firstOperand.Or(secondOperand);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray And(NumberCommand command) //3 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var result = firstOperand.And(secondOperand);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Xor(NumberCommand command) //4 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var result = firstOperand.Xor(secondOperand);
        //    command.SetOperand(3, result);
        //    return result;
        //}

        //public BitArray Impication(NumberCommand command) //5
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var result = firstOperand.Not().Or(secondOperand);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public void CoImpication(NumberCommand command) //6 
        //{
        //    var implicationResult = Impication(command);

        //    var result = implicationResult.Not();
        //    command.SetOperand(3, result);
        //}

        //public BitArray Equivalence(NumberCommand command) //7 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var tmpFirst = (BitArray)firstOperand.Clone();


        //    firstOperand.And(secondOperand);
        //    tmpFirst.Not().And(secondOperand);

        //    var result = firstOperand.Or(tmpFirst);

        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Pierce(NumberCommand command) //8 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    firstOperand.Or(secondOperand);
        //    var result = firstOperand.Not();

        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Scheffer(NumberCommand command) //9 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    firstOperand.And(secondOperand);
        //    var result = firstOperand.Not();

        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Addition(NumberCommand command) //10 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var resultInt = firstOperand.ToInt() + secondOperand.ToInt();
        //    var result = BitArrayExtension.IntToBitArr(resultInt);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Subtraction(NumberCommand command) //11 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var resultInt = firstOperand.ToInt() - secondOperand.ToInt();
        //    var result = BitArrayExtension.IntToBitArr(resultInt);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Multiplication(NumberCommand command) //12 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var resultInt = firstOperand.ToInt() * secondOperand.ToInt();
        //    var result = BitArrayExtension.IntToBitArr(resultInt);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray StrongDivision(NumberCommand command) //13 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var resultInt = firstOperand.ToInt() / secondOperand.ToInt();
        //    var result = BitArrayExtension.IntToBitArr(resultInt);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray Mod(NumberCommand command) //14 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var resultInt = firstOperand.ToInt() % secondOperand.ToInt();
        //    var result = BitArrayExtension.IntToBitArr(resultInt);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public void Swap(NumberCommand command) //15 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    command.SetOperand(1, secondOperand);
        //    command.SetOperand(2, firstOperand);
        //}

        //public BitArray Insert(NumberCommand command) //16 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);
        //    var thirdOperand = command.GetOperand(3);

        //    var position = secondOperand.ToInt();

        //    var inRange = position < 9 && position >= 0;
        //    if (!inRange)
        //        position %= NumberCommand.OperandSize;

        //    firstOperand[position] = thirdOperand[position];

        //    return firstOperand;
        //}

        //public string GetNumInSecOperandBase(NumberCommand command)//17 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var num = firstOperand.ToInt();
        //    var bs = secondOperand.ToInt();

        //    if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

        //    var numInNewBase = Convert.ToString(num, bs).ToUpper();
        //    return numInNewBase;
        //}

        //public BitArray ReadInBase(NumberCommand command, string operand) //18
        //{
        //    var secondOperand = command.GetOperand(2);
        //    var bs = secondOperand.ToInt();

        //    if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

        //    var num = Convert.ToInt32(operand, bs);
        //    var result = BitArrayExtension.IntToBitArr(num);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray FindMaxDivider(NumberCommand command) //19
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var num = firstOperand.ToInt();
        //    var divider = 0;
        //    for (var i = 0; i < 10; i++)
        //    {
        //        if (num % (int)Math.Pow(2, i) == 0)
        //            divider = i;
        //    }

        //    var result = BitArrayExtension.IntToBitArr(divider, NumberCommand.OperandSize);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray ShiftL(NumberCommand command) //20 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var shiftLength = secondOperand.ToInt();

        //    var result = firstOperand.ShiftL(shiftLength);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray ShiftR(NumberCommand command) //21 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var shiftLength = secondOperand.ToInt();

        //    var result = firstOperand.ShiftR(shiftLength);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray CycleShiftL(NumberCommand command) //22
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var shiftLength = secondOperand.ToInt();

        //    var result = firstOperand.CycleShiftL(shiftLength);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray CycleShiftR(NumberCommand command) //23 
        //{
        //    var firstOperand = command.GetOperand(1);
        //    var secondOperand = command.GetOperand(2);

        //    var shiftLength = secondOperand.ToInt();

        //    var result = firstOperand.CycleShiftR(shiftLength);
        //    command.SetOperand(3, result);

        //    return result;
        //}

        //public BitArray MoveSecToFirst(NumberCommand command) //24
        //{
        //    var secondOperand = command.GetOperand(2);

        //    command.SetOperand(1, secondOperand);

        //    return secondOperand;
        //}


        //#endregion

    }
}
