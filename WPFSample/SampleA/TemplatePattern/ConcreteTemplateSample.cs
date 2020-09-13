using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.SampleA.TemplatePattern
{
    // Template Method Pattern
    class ConcreteTemplateSample : TemplateSample
    {
        protected override void Execute()
        {
            Console.WriteLine("sample1 Execute");
        }

        protected override void Initialize()
        {
            Console.WriteLine("sample1 Initialize");
        }

        protected override void Terminate()
        {
            Console.WriteLine("sample1 Terminate");
        }

        public override void KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("sample1 : " + e.Key.ToString());
        }
    }
}
