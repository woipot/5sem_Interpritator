using System;
using System.Collections.Generic;
using System.IO;
using Interpritator.Properties;

namespace Interpritator.Source.UserInterfaceUtilities
{
    public class MainMenuFunc
    {
        public static void SaveFile(string patch,[NotNull] IEnumerable<string> inputArr)
        {
            var file = File.Open(patch, FileMode.Create);
            var sw = new StreamWriter(file);

            foreach (var command in inputArr)
            {
                sw.Write(command);
            }

            sw.Dispose();
            file.Dispose();
        }

        public static IEnumerable<string> OpenFile(string patch)
        {
            var isExist = File.Exists(patch);
            if(!isExist)
                throw new Exception("File not found");

            var result = new List<string>();

            using (var sr = new StreamReader(patch))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
