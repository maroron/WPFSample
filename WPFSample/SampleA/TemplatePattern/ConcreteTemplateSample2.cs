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

        protected override bool ValidKey(Key key)
        {
            bool isValid = false;
            switch (key)
            {
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
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
            Console.WriteLine("sample2 : " + key.ToString());
        }
    }
}
