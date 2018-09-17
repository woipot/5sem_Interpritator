using System.Collections.Generic;

namespace Interpritator.Source.Interpritator
{
    public static class OperationsInfo
    {
        public static List<string> OperationsName = new List<string>
        {
            "Shb", "!", "||", "&&", "Xor",

            "->", "<-", "~", @"\/", @"/\",

            "+", "-", "*", "/", "mod",

            "<->", ".!..", "~>", "~~>", "max^p",
            
            "<<", ">>", "..<<", ">>..", 

            "..->.."
        };
    }
}