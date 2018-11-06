using System;
using System.Collections.Generic;
using System.IO;
using Interpritator.Properties;

namespace Interpritator.Source.UserInterfaceUtilities
{
    public class MainMenuFunc
    {
        public static void SaveFile(string patch,[NotNull] string input)
        {
            var file = File.Open(patch, FileMode.Create);
            var sw = new StreamWriter(file);

            sw.Write(input);

            sw.Dispose();
            file.Dispose();
        }

        public static string OpenFile(string patch)
        {
            var isExist = File.Exists(patch);
            if(!isExist)
                throw new Exception("File not found");

            string result;

            using (var sr = new StreamReader(patch))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
    }
}
