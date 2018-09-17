using System;
using System.Collections;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Interpritator.Source.Interpritator
{
    static class BitArrayExtension
    {
        #region Get Set Range

        public static BitArray GetRange(this BitArray bitArr, uint firstIndex, uint secondIndex)
        {
            var inRange = secondIndex <= bitArr.Count;
            if (!inRange)
                throw new Exception("Index out of range");
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

            var topLimit = firstIndex + inputArray.Length;
            var inRange = (topLimit) < bitArray.Length;

            if (!inRange)
            {
                topLimit = bitArray.Length;
            }

            var inputIndex = 0;
            for (var i = (int)firstIndex; i < topLimit; i++)
            {
                result[i] = inputArray[inputIndex];
                inputIndex++;
            }

            return result;
        }

        #endregion


        #region Convert

        public static bool[] ToBoolArray(this BitArray bitArray)
        {
            var boolArray = bitArray.Cast<bool>().ToArray();
            return boolArray;
        }

        public static BitArray IntToBitArr(int number, int size = -1)
        {
            var preparingInt = new[] { number };
            var reverceResult = new BitArray(preparingInt);

            if (size >= 0)
                reverceResult.Length = size;


            var result = reverceResult.Reverce();


            return result;
        }

        public static int ToInt(this BitArray operand)
        {
            var inStr = operand.ConvertToString();

            return Convert.ToInt32(inStr, 2);
        }

        public static BitArray Reverce(this BitArray bitArray)
        {
            var result = new BitArray(bitArray.Count);

            var resultIndex = 0;
            for (var i = bitArray.Count - 1; i >= 0; i--)
            {
                result[resultIndex] = bitArray[i];

                resultIndex++;
            }

            return result;
        }

        #endregion


        #region override

        
        public static string ConvertToString(this BitArray bitArray)
        {
            var inString = string.Join("", bitArray.Cast<bool>().Select(Convert.ToInt32));
            return inString;
        }

        #endregion


        #region Operations

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

        #endregion
    }
}
