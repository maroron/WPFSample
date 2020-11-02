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
                Select(i => new Person
                {
                    Name = "さとう" + i,
                    Age = 20 + i,
                    Fruits = new List<Fruit>
                    {
                        new Fruit{ Name = "Apple",  Count = i + 1 }, 
                        new Fruit{ Name = "Banana", Count = i + 1 },
                        new Fruit{ Name = "Peach",  Count = i + 1 },
                    }
                });
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Fruit> Fruits { get; set; }
    }

    class Fruit
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
