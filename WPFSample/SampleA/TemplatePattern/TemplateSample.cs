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
        protected TemplateSample()
        {
            Initialize();
            Execute();
            Terminate();
        }

        protected abstract void Initialize();
        protected abstract void Execute();
        protected abstract void Terminate();

        public void KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if (ValidKey(key))
            {
                Process(key);
            }
        }

        protected abstract bool ValidKey(Key key);

        protected abstract void Process(Key key);
    }
}
