using System;

namespace Generator.Application.Exceptions
{
    public class GeneratorException : Exception
    {
        public GeneratorException()
        {
        }

        public GeneratorException(string message)
            : base(message)
        {
        }

        public GeneratorException(Exception inner)
            : this(inner.ToString())
        {
        }
    }
}
