using K1_S6.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class FileReader : IEnumerable<string>
    {

        private List<string> _dataList = new List<string>();

        private bool _FileReadIn = false;

        public bool FileReadIn { get { return _FileReadIn; } }


        private string _FilePath = "";

        public string FilePath { get { return _FilePath; } }

        public FileReader(string filePath)
        {
            _FilePath = filePath;
        }

        public void ReadFile()
        {
            if (File.Exists(FilePath))
            {
                // Source => https://docs.microsoft.com/de-de/dotnet/standard/io/how-to-read-text-from-a-file
                try
                {
                    using (StreamReader sr = new StreamReader(FilePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            _dataList.Add(line);
                        }

                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
                finally
                {
                    _FileReadIn = true;
                }
            }
            else
            {
                var e = new NoFileException(FilePath);
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

    }
}
