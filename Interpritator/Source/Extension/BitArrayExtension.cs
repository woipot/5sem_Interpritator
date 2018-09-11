using System.Collections;
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

            var secondIndex = firstIndex + inputArray.Count;

            var counter = 0;
            for (var i = (int)firstIndex; i < secondIndex; i++)
            {
                result[i] = inputArray[counter];
                counter++;
            }

            return result;
        }

    }
}
