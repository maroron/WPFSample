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

        public abstract void KeyDown(object sender, KeyEventArgs e);
    }
}
