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
    class ConcreteTemplateSample2 : TemplateSample
    {
        protected override void Execute()
        {
            Console.WriteLine("sample2 Execute");
        }

        protected override void Initialize()
        {
            Console.WriteLine("sample2 Initialize");
        }

        protected override void Terminate()
        {
            Console.WriteLine("sample2 Terminate");
        }

        public override void KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("sample2 : " + e.Key.ToString());
        }
    }
}
