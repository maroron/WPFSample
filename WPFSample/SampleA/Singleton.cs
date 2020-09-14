using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    class Singleton
    {
        static public Singleton GetInstance { get; private set; } = new Singleton();

        private Singleton() { }
    }
}