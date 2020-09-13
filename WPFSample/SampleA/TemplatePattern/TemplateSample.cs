using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFSample.SampleA.TemplatePattern
{
    // Template Method Pattern
    abstract class TemplateSample
    {
        private Window window;

        protected TemplateSample(Window w)
        {
            this.window = w;
            Initialize();
            Execute();
            Terminate();
        }

        public virtual void Run()
        {
            window.KeyDown -= KeyDown;
            window.KeyDown += KeyDown;
        }

        protected abstract void Initialize();
        protected abstract void Execute();
        protected abstract void Terminate();

        protected abstract void KeyDown(object sender, KeyEventArgs e);
    }
}
