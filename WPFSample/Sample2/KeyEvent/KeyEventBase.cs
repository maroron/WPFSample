using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.Sample2.KeyEvent
{
    abstract class KeyEventBase
    {
        protected Window w;

        public KeyEventBase(Window w)
        {
            this.w = w;
        }

        public void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;

            StartRegistEvent();
            if (IsValidKey(key))
            {
                Process(key);
            }
            EndRegistEvent();
        }

        protected abstract void StartRegistEvent();

        protected virtual bool IsValidKey(Key key) { return true; }

        protected abstract void Process(Key key);

        protected abstract void EndRegistEvent();
    }
}
