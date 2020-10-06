using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleC
{
    class Person
    {
        public string Name { get; set; }
        public List<Person> Children { get; set; }
    }
}
