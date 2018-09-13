using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Interpritator.Source.Interpritator
{
    public class NumberCommandInterpritator
    {
        private RichTextBox _dataInput;

        public NumberCommandInterpritator(RichTextBox richText)
        {
            _dataInput = richText;
        }

        public void StartProgramm(string binFilePatch, RichTextBox outPutText)
        {
            var binFile = File.Open(binFilePatch, FileMode.Open);

            using (var br = new BinaryReader(binFile))
            {
                while (br.PeekChar() > -1)
                {
                    var byteCommand = br.ReadBytes(4);
                    var command = new NumberCommand(byteCommand);

                    

                }
            }

            binFile.Dispose();

        }

     
    }
}
