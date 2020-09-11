using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA.TemplatePattern
{
    class ConcreteTemplateSample : TemplateSample
    {
        protected override void Execute()
        {
            Console.WriteLine("Execute");
        }

        protected override void Initialize()
        {
            Console.WriteLine("Initialize");
        }

        protected override void Terminate()
        {
            Console.WriteLine("Terminate");
        }
    }
}
