using System;

namespace Exceptions
{
    public class FileSystemException : Exception
    {

        public FileSystemException(string message) : base(message)
        {
        }

    }
}