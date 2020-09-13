using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.SampleA.TemplatePattern
{
    class ConcreteTemplateSample : TemplateSample
    {
        public ConcreteTemplateSample(Window w) : base(w)
        {
        }

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

        protected override void KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("sample1" + e.Key.ToString());
        }
    }
}
