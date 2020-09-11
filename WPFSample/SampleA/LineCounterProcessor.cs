using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace WPFSample.SampleA
{
    class LineCounterProcessor : TextProcessor
    {
        private int count;

        protected override void Initialize(string fileName)
        {
            count = 0;
        }

        protected override void Execute(string line)
        {
            count++;
        }

        protected override void Terminate()
        {
            Console.WriteLine("{0}行", count);
        }
    }
}
