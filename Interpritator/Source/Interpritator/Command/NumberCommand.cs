using System;
using System.Collections;
using Interpritator.Source.Extension;
using Interpritator.Source.Interpritator.Command.Operations;

namespace Interpritator.Source.Interpritator.Command
{
    public class NumberCommand
    {
        private BitArray _command;

        public const int OperandCount = 3;
        public const int OperandSize = 9;
        public const int CommandSize = 32;
        public const int OperatorSize = 5;

        
        #region Constructors

        public NumberCommand()
        {
            _command = new BitArray(CommandSize, false);
        }

        public NumberCommand(BitArray bitArr)
        {
            _command = (BitArray)bitArr.Clone();
        }

        public NumberCommand(byte[] byteArr)
        {
            _command = new BitArray(byteArr);
        }
        #endregion


        #region Public Methods

        public BitArray GetOperand(uint operandNumber)
        {

            var firstIndex = (OperandCount - operandNumber) * OperandSize;
            var secondIndex = firstIndex + OperandSize;

            var result = _command.GetRange(firstIndex, secondIndex);
            return result;
        }

        public void SetOperand(uint operandNumber, BitArray inputArr)
        {
            var firstIndex = (OperandCount - operandNumber) * OperandSize;

            _command = _command.SetRange(firstIndex, inputArr);

        }

        public BitArray GetOperator()
        {
            var firstIndex = OperandCount * OperandSize;
            var secondIndex = CommandSize;

            var result = _command.GetRange((uint)firstIndex, (uint)secondIndex);
            return result;
        }

        public void SetOperator(BitArray operatBitArr)
        {
            var firstIndex = 3 * OperandSize;

            _command = _command.SetRange((uint)firstIndex, operatBitArr);
        }

        public BitArray GetBitArr()
        {
            return (BitArray)_command.Clone();
        }

        #endregion


        #region Overrides

        public override string ToString()
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);
            var thirdOperand = GetOperand(3);
            var operation = GetOperator();


            var firstInStr = System.Convert.ToString(firstOperand.ToInt());
            var secondInStr = System.Convert.ToString(secondOperand.ToInt());
            var thirdInStr = System.Convert.ToString(thirdOperand.ToInt());

            var operationNumber = operation.ToInt();
            var operationName = OperationsInfo.OperationsName[operationNumber];


            var result = $"{thirdInStr} {secondInStr} {firstInStr} {operationName}";

            return result;
        }

        public string ToBinStr()
        {
            return _command.ConvertToString();
        }

        #endregion


        #region Operations

        public string GetOperandsList() //0 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);
            var thirdOperand = GetOperand(3);
            var operation = GetOperator();

            var bs = secondOperand.ToInt();

            if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

            var firstInNewBase = System.Convert.ToString(firstOperand.ToInt(), bs).ToUpper();
            var secondInNewBase = System.Convert.ToString(secondOperand.ToInt(), bs).ToUpper();
            var thirdInNewBase = System.Convert.ToString(thirdOperand.ToInt(), bs).ToUpper();
            var operationInNewBase = System.Convert.ToString(operation.ToInt(), bs).ToUpper();

            var result = $"{thirdInNewBase} {secondInNewBase} {firstInNewBase} {operationInNewBase}";

             return result ;
        }

        public NumberCommand NotFirst() //1 
        {
            var firstOperand = GetOperand(1);
            firstOperand.Not();
            SetOperand(3, firstOperand);

            return new NumberCommand(_command);
        }

        public NumberCommand Or() //2 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.Or(secondOperand);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand And() //3 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.And(secondOperand);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Xor() //4 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.Xor(secondOperand);
            SetOperand(3, result);
             return new NumberCommand(_command);
        }

        public NumberCommand Impication() //5
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.Not().Or(secondOperand);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand CoImpication() //6 
        {
            var implicationResult = Impication();

            var result = implicationResult.GetOperand(3);
            result.Not();
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Equivalence() //7 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var tmpFirst = (BitArray)firstOperand.Clone();


            firstOperand.And(secondOperand);
            tmpFirst.Not().And(secondOperand);

            var result = firstOperand.Or(tmpFirst);

            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Pierce() //8 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            firstOperand.Or(secondOperand);
            var result = firstOperand.Not();

            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Scheffer() //9 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            firstOperand.And(secondOperand);
            var result = firstOperand.Not();

            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Addition() //10 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() + secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt, OperandSize);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Subtraction() //11 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() - secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt, OperandSize);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Multiplication() //12 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() * secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt, OperandSize);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand StrongDivision() //13 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() / secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt, OperandSize);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Mod() //14 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() % secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt, OperandSize);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Swap() //15 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            SetOperand(1, secondOperand);
            SetOperand(2, firstOperand);
            return new NumberCommand(_command);

        }

        public NumberCommand Insert() //16 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);
            var thirdOperand = GetOperand(3);

            var position = secondOperand.ToInt();

            var inRange = position < 9 && position >= 0;
            if (!inRange)
                position %= OperandSize;

            firstOperand[position] = thirdOperand[position];

            return new NumberCommand(_command);
        }

        public string Convert() //17 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var num = firstOperand.ToInt();
            var bs = secondOperand.ToInt();

            if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

            var numInNewBase = System.Convert.ToString(num, bs).ToUpper();
            return numInNewBase;
        }

        public NumberCommand FindMaxDivider() //19
        {
            var firstOperand = GetOperand(1);
            var num = firstOperand.ToInt();
            var divider = 0;
            for (var i = 0; i < 10; i++)
            {
                if (num % (int)Math.Pow(2, i) == 0)
                    divider = i;
            }

            var result = BitArrayExtension.IntToBitArr(divider, OperandSize);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand ShiftL() //20 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.ShiftL(shiftLength);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand ShiftR() //21 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.ShiftR(shiftLength);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand CycleShiftL() //22
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.CycleShiftL(shiftLength);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand CycleShiftR() //23 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.CycleShiftR(shiftLength);
            SetOperand(3, result);

             return new NumberCommand(_command);
        }

        public NumberCommand Copy() //24
        {
            var secondOperand = GetOperand(2);

            SetOperand(1, secondOperand);

            return new NumberCommand(_command); ;
        }


        #endregion


        #region StaticOperations
        public static string GetOperandsList(NumberCommand command) //0 
        {
            return command.GetOperandsList();
        }

        public static NumberCommand NotFirst(NumberCommand command) //1 
        {
            return command.NotFirst();
        }

        public static NumberCommand Or(NumberCommand command) //2 
        {
            return command.Or();
        }

        public static NumberCommand And(NumberCommand command) //3 
        {
            return command.And();
        }

        public static NumberCommand Xor(NumberCommand command) //4 
        {
            return command.Xor();
        }

        public static NumberCommand Impication(NumberCommand command) //5
        {
            return command.Impication();
        }

        public static NumberCommand CoImpication(NumberCommand command) //6 
        {
            return command.CoImpication();
        }

        public static NumberCommand Equivalence(NumberCommand command) //7 
        {
            return command.Equivalence();
        }

        public static NumberCommand Pierce(NumberCommand command) //8 
        {
            return command.Pierce();
        }

        public static NumberCommand Scheffer(NumberCommand command) //9 
        {
            return command.Scheffer();
        }

        public static NumberCommand Addition(NumberCommand command) //10 
        {
            return command.Addition();
        }

        public static NumberCommand Subtraction(NumberCommand command) //11 
        {
            return command.Subtraction();
        }

        public static NumberCommand Multiplication(NumberCommand command) //12 
        {
            return command.Multiplication();
        }

        public static NumberCommand StrongDivision(NumberCommand command) //13 
        {
            return command.StrongDivision();
        }

        public static NumberCommand Mod(NumberCommand command) //14 
        {
            return command.Mod();
        }

        public static NumberCommand Swap(NumberCommand command) //15 
        {
            return command.Swap();

        }

        public static NumberCommand Insert(NumberCommand command) //16 
        {
            return command.Insert();
        }

        public static string Convert(NumberCommand command) //17 
        {
            return command.Convert();
        }

        public static NumberCommand FindMaxDivider(NumberCommand command) //19
        {
            return command.FindMaxDivider();
        }

        public static NumberCommand ShiftL(NumberCommand command) //20 
        {
            return command.ShiftL();
        }

        public static NumberCommand ShiftR(NumberCommand command) //21 
        {
            return command.ShiftR();
        }

        public static NumberCommand CycleShiftL(NumberCommand command) //22
        {
            return command.CycleShiftL();
        }

        public static NumberCommand CycleShiftR(NumberCommand command) //23 
        {
            return command.CycleShiftR();
        }

        public static NumberCommand Copy(NumberCommand command) //24
        {
            return command.Copy();
        }
        #endregion
    }
}
