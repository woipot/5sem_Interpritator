using System;
using System.Collections;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Interpritator.Source.Interpritator
{
    static class BitArrayExtension
    {
        public static BitArray GetRange(this BitArray bitArr, uint firstIndex, uint secondIndex)
        {
            var size = (int)(secondIndex - firstIndex);
            var result = new BitArray(size);

            var counter = 0;
            for (var i = (int)firstIndex; i < secondIndex; i++)
            {
                result[counter] = bitArr[i];
                counter++;
            }

            return result;
        }

        public static BitArray SetRange(this BitArray bitArray, uint firstIndex, BitArray inputArray)
        {
            var result = (BitArray)bitArray.Clone();

            var maxSecondIndex = firstIndex + inputArray.Count;
            var thisArrSize = bitArray.Count;

            var secondIndex = Math.Min(maxSecondIndex, thisArrSize);

            secondIndex -= 1;

            var counter = (int)firstIndex;
            for (var i = (int)secondIndex; i >= firstIndex; i--)
            {
                result[counter] = inputArray[i];
                counter++;
            }

            return result;
        }



        public static BitArray CycleShiftR(this BitArray operand, int count)
        {
            var source = operand.Cast<bool>().ToArray();
            for (int i = 0; i < count; i++)
            {
                var temp = source[source.Length - 1];
                Array.Copy(source, 0, source, 1, source.Length - 1);
                source[0] = temp;
            }
            return operand = new BitArray(source);
        }

        public static bool[] ToBoolArray(this BitArray bitArray)
        {
            var boolArray = bitArray.Cast<bool>().ToArray();
            return boolArray;
        }

        public static BitArray CycleShiftL(this BitArray operand, int count)
        {
            var source = operand.Cast<bool>().Reverse().ToArray();
            for (int i = 0; i < count; i++)
            {
                var temp = source[source.Length - 1];
                Array.Copy(source, 0, source, 1, source.Length - 1);
                source[0] = temp;
            }
            source = source.Reverse().ToArray();
            return operand = new BitArray(source);
        }

        public static BitArray IntToBitArr(int number, int size = -1)
        {
            var preparingInt = new[] {number};
            var result = new BitArray(preparingInt);

            if (size >= 0)
                result.Length = size;

            return result;
        }

        public static int ToInt(this BitArray operand)
        {
            var binaryOperand = string.Join("", operand.ToBoolArray());

            return Convert.ToInt32(binaryOperand, 2);
        }

        public static BitArray ShiftR(this BitArray operand, int number)
        {
            var tmpNum = operand.ToInt() >> number;
            operand = IntToBitArr(tmpNum);
            return operand;
        }

        public static BitArray ShiftL(this BitArray operand, int number)
        {
            var tmpNum = operand.ToInt() >> number;
            operand = IntToBitArr(tmpNum);
            return operand;
        }



    }
}
