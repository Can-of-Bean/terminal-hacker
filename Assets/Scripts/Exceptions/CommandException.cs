using System;

namespace Exceptions
{
    public class CommandException : Exception
    {

        public CommandException(string message) : base(message)
        {
        }

    }
}