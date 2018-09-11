using System;

namespace Interpritator.Source.Interpritator
{
    public class CompilerException : Exception
    {
        private string _wrongCommand;

        private int _commandNumber;

        public CompilerException(string wrongCommand, string message) : base(message)
        {
            _wrongCommand = wrongCommand;
            _commandNumber = -1;
        }

        public string WrongCommand { get => _wrongCommand; set => _wrongCommand = value; }
        public int CommandNumber { get => _commandNumber; set => _commandNumber = value; }
    }
}