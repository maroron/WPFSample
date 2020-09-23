using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.Sample2.KeyEvent
{
    class RoiKeyEvent : KeyEventBase
    {
        public RoiKeyEvent(Window w) : base(w)
        {
        }

        protected override bool IsValidKey(Key key)
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
            Console.WriteLine("Process!!");
        }
    }
}
