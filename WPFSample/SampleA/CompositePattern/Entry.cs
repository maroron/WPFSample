using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA.CompositePattern
{
    abstract class Entry
    {
        protected string name;
        protected int size;

        public abstract string GetName();

        public abstract int GetSize();

        public void PrintList()
        {
            PrintList("");
        }

        public abstract void PrintList(string prefix);

        public virtual Entry Add(Entry entry)
        {
            throw new Exception();
        }

        public override string ToString()
        {
            return GetName() + " (" + GetSize() + ")";
        }
    }
}
