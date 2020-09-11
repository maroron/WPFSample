using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessor
{
    public abstract class TextProcessor
    {
        public static void Run<T>(string fileName) where T: TextProcessor, new()
        {
            var self = new T();
            self.Process(fileName);
        }

        private void Process(string fileName)
        {
            Initialize(fileName);
            using (var sr = new StreamReader(fileName))
            {
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Execute(line);
                }
            }
            Terminate();
        }

        protected virtual void Initialize(string fileName) { }
        protected virtual void Execute(string line) { }
        protected virtual void Terminate() { }
    }
}
