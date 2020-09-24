using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.Sample2.MouseEvent
{
    class RoiMouseEvent : MouseEventBase
    {
        public RoiMouseEvent(Window w) : base(w)
        {
        }

        protected override void ProcessMouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("RoiMouseDown");
        }

        protected override void ProcessMouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("RoiMouse");
        }
    }
}
