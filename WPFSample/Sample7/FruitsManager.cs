using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Sample7
{
    class FruitsManager
    {
        public IList<Fruit> Fruits { get; set; }

        public FruitsManager()
        {
            Fruits = new List<Fruit>()
            {
                new Fruit(){ Name = "Banana", Count = 2 },
                new Fruit(){ Name = "Apple", Count = 1 },
                new Fruit(){ Name = "Mango", Count = 5 }
            };
        }
    }
}
