using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Interpritator.Source.UserInterfaceUtilities
{
    public class MainMenuFunc
    {
        public static void SaveFile(string patch, RichTextBox dataInput)
        {
            var textRange = new TextRange(
                dataInput.Document.ContentStart,
                dataInput.Document.ContentEnd
            );


            var file = File.Open(patch, FileMode.Create);
            var sw = new StreamWriter(file);

            sw.Write(textRange.Text);

            sw.Dispose();
            file.Dispose();
        }

        public static void OpenFile(string patch, RichTextBox dataOutput)
        {
            var isExist = File.Exists(patch);
            if(!isExist)
                throw new Exception("File not found");

            using (var sr = new StreamReader(patch))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    dataOutput.AppendText(line);
                }
            }

        }
    }
}
