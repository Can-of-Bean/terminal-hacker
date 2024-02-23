using System;

namespace Terminal
{
    public class TerminalInputEventArgs : EventArgs
    {
        public TerminalInputEventArgs(string input)
        {
            Input = input;
        }

        public string Input { get; }
    }
}