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

        protected override bool ValidKey(Key key)
        {
            bool isValid = false;
            switch (key)
            {
                case Key.A:
                case Key.W:
                case Key.S:
                case Key.D:
                    isValid = true;
                    break;
                default:
                    isValid = false;
                    break;
            }
            return isValid;
        }

        protected override void Process(Key key)
        {
            Console.WriteLine("sample1 : " + key.ToString());
        }
    }
}
