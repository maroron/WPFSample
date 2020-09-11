using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA.TemplatePattern
{
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
    }
}
