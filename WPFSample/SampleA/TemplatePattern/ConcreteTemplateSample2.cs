using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.SampleA.TemplatePattern
{
    class ConcreteTemplateSample2 : TemplateSample
    {
        public ConcreteTemplateSample2(Window w) : base(w)
        {
        }

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

        protected override void KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("sample2 : " + e.Key.ToString());
        }
    }
}
