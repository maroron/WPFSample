using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample
{
    class TabControlViewModel
    {
        public IEnumerable<Person> People { get; set; }

        public TabControlViewModel()
        {
            People = Enumerable.Range(1, 10).
                Select(i => new Person { Name = "さとう" + i, Age = 20 + i });
        }
    }
}
