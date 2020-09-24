using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.Sample2.MouseEvent
{
    abstract class MouseEventBase
    {
        protected Window w;

        public MouseEventBase(Window w)
        {
            this.w = w;
        }

        public void PreviewMouseDown(object sender, MouseEventArgs e)
        {
            StartRegistMouseDown();
            if (IsValidMouseDown(sender, e))
            {
                ProcessMouseDown(sender, e);
            }
            EndRegistMouseDown();
        }

        protected virtual void StartRegistMouseDown() { }

        protected virtual bool IsValidMouseDown(object sender, MouseEventArgs e) { return true; }

        protected abstract void ProcessMouseDown(object sender, MouseEventArgs e);

        protected virtual void EndRegistMouseDown() { }

        public void PreviewMouseMove(object sender, MouseEventArgs e)
        {
            StartRegistMouseMove();
            if (IsValidMouseMove(sender, e))
            {
                ProcessMouseMove(sender, e);
            }
            EndRegistMouseMove();
        }

        protected virtual void StartRegistMouseMove() { }

        protected virtual bool IsValidMouseMove(object sender, MouseEventArgs e) { return true; }

        protected abstract void ProcessMouseMove(object sender, MouseEventArgs e);

        protected virtual void EndRegistMouseMove() { }
    }
}
