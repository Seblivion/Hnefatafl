using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserDefinedExceptions
{
    [Serializable]
    public class BadStringException : Exception
    {
        public BadStringException()
        {

        }

        public BadStringException(string message)
        : base(message)
        {

        }

        public BadStringException(string message, Exception inner)
        : base(message, inner)
        {

        }
    }
}
