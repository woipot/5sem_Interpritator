using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Windows.Media.Imaging;

namespace Interpritator.Source.Interpritator
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
            _command = bitArr.Clone() as BitArray;
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
            var secondIndex = CommandSize - 1;

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


        #region Operations

        public string GetOperandsList() //0 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);
            var thirdOperand = GetOperand(3);
            var operation = GetOperator();

            var bs = secondOperand.ToInt();

            if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

            var firstInNewBase = Convert.ToString(firstOperand.ToInt() , bs).ToUpper();
            var secondInNewBase = Convert.ToString(secondOperand.ToInt(), bs).ToUpper();
            var thirdInNewBase = Convert.ToString(thirdOperand.ToInt(), bs).ToUpper();
            var operationInNewBase = Convert.ToString(operation.ToInt(), bs).ToUpper();

            var result = $"{thirdOperand} {secondOperand} {firstOperand} {operationInNewBase}";

            return result;
        }

        public BitArray NotFirstToFirst() //1 
        {
            var firstOperand = GetOperand(1);
            firstOperand.Not();
            SetOperand(3, firstOperand);

            return firstOperand;
        }

        public BitArray Or() //2 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.Or(secondOperand);
            SetOperand(3, result);

            return result;
        }

        public BitArray And() //3 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.And(secondOperand);
            SetOperand(3, result);

            return result;
        }

        public BitArray Xor() //4 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.Xor(secondOperand);
            SetOperand(3, result);
            return result;
        }

        public BitArray Impication() //5
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var result = firstOperand.Not().Or(secondOperand);
            SetOperand(3, result);

            return result;
        }

        public void CoImpication() //6 
        {
            var implicationResult = Impication();

            var result = implicationResult.Not();
            SetOperand(3, result);
        }

        public BitArray Equivalence() //7 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var tmpFirst = (BitArray)firstOperand.Clone();


            firstOperand.And(secondOperand);
            tmpFirst.Not().And(secondOperand);

            var result = firstOperand.Or(tmpFirst);

            SetOperand(3, result);

            return result;
        }

        public BitArray Pierce() //8 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            firstOperand.Or(secondOperand);
            var result = firstOperand.Not();

            SetOperand(3, result);

            return result;
        }

        public BitArray Scheffer() //9 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            firstOperand.And(secondOperand);
            var result = firstOperand.Not();

            SetOperand(3, result);

            return  result;
        }

        public BitArray Addition() //10 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() + secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt);
            SetOperand(3, result);

            return result;
        }

        public BitArray Subtraction() //11 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() - secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt);
            SetOperand(3, result);

            return result;
        }

        public BitArray Multiplication() //12 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() * secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt);
            SetOperand(3, result);

            return result;
        }

        public BitArray StrongDivision() //13 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() / secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt);
            SetOperand(3, result);

            return result;
        }

        public BitArray Mod() //14 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var resultInt = firstOperand.ToInt() % secondOperand.ToInt();
            var result = BitArrayExtension.IntToBitArr(resultInt);
            SetOperand(3, result);

            return result;
        }

        public void Swap() //15 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            SetOperand(1, secondOperand);
            SetOperand(2,firstOperand);
        }

        public BitArray Insert() //16 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);
            var thirdOperand = GetOperand(3);

            var position = secondOperand.ToInt();

            var inRange = position < 9 && position >= 0;
            if (!inRange)
                position %= OperandSize;

            firstOperand[position] = thirdOperand[position];

            return firstOperand;
        }

        public string GetNumInSecOperandBase() //17 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var num = firstOperand.ToInt();
            var bs = secondOperand.ToInt();

            if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

            var numInNewBase = Convert.ToString(num, bs).ToUpper();
            return numInNewBase;
        }

        public BitArray ReadInBase(string operand) //18
        {
            var secondOperand = GetOperand(2);
            var bs  = secondOperand.ToInt();

            if (bs > 36 || bs < 0) throw new Exception("Incorrect base");

            var num = Convert.ToInt32(operand, bs);
            var result = BitArrayExtension.IntToBitArr(num);
            SetOperand(3, result);

            return result;
        }

        public BitArray FindMaxDivider() //19
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

            return result;
        }

        public BitArray ShiftL() //20 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.ShiftL(shiftLength);
            SetOperand(3, result);

            return result;
        }

        public BitArray ShiftR() //21 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.ShiftR(shiftLength);
            SetOperand(3, result);

            return result;
        }

        public BitArray CycleShiftL() //22
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.CycleShiftL(shiftLength);
            SetOperand(3, result);

            return result;
        }

        public BitArray CycleShiftR() //23 
        {
            var firstOperand = GetOperand(1);
            var secondOperand = GetOperand(2);

            var shiftLength = secondOperand.ToInt();

            var result = firstOperand.CycleShiftR(shiftLength);
            SetOperand(3, result);

            return result;
        }

        public BitArray MoveSecToFirst() //24
        {
            var secondOperand = GetOperand(2);

            SetOperand(1, secondOperand);

            return secondOperand;
        }


        #endregion
    }
}
