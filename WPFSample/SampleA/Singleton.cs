using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    sealed class Singleton
    {
        static public Singleton GetInstance { get; } = new Singleton();

        private Singleton() { }
    }
}