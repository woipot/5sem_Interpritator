using System.Collections;
using System.Runtime.ExceptionServices;

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

    }
}
