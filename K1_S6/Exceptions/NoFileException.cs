using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Exceptions
{
    class NoFileException : Exception
    {
        private string _FilePath = "";
        public string FilePath { get { return _FilePath; } }

        public NoFileException(string FilePath) : base(ModifyMessage(FilePath))
        {
            _FilePath = FilePath;
        }

        public static string ModifyMessage(string Message)
        {
            return "No file Found for : " + Message;
        }

    }
}
