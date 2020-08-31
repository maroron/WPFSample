using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Sample7
{
    class FruitsManager
    {
        public ObservableCollection<Fruit> Fruits { get; set; }

        public FruitsManager()
        {
            Fruits = new ObservableCollection<Fruit>()
            {
                new Fruit(){ Name = "Banana", Count = 2 },
                new Fruit(){ Name = "Apple", Count = 1 },
                new Fruit(){ Name = "Mango", Count = 5 }
            };
        }
    }
}
