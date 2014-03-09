using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Corpus
{
    class Logger
    {
        private TextWriter mWriter = new StreamWriter("log.txt");

        public void Write(string str)
        {
            Console.Write(str);
            mWriter.Write(str);
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
            mWriter.WriteLine(str);
        }

        public void Close()
        {
            mWriter.Flush();
            mWriter.Close();
        }
    }
}
