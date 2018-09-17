using System;
using System.IO;
using System.Text;
using Interpritator.Annotations;

namespace Interpritator.Source.UserInterfaceUtilities
{
    public class MainMenuFunc
    {
        public static void SaveFile(string patch,[NotNull] string inputText)
        {
            var file = File.Open(patch, FileMode.Create);
            var sw = new StreamWriter(file);

            sw.Write(inputText);

            sw.Dispose();
            file.Dispose();
        }

        public static string OpenFile(string patch)
        {
            var isExist = File.Exists(patch);
            if(!isExist)
                throw new Exception("File not found");

            var result = new StringBuilder();

            using (var sr = new StreamReader(patch))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result.Append(line);
                }
            }

            return result.ToString();
        }
    }
}
