using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Sample7
{
    class FruitsManager : INotifyPropertyChanged
    {
        // refernce
        // https://qiita.com/soi/items/d0c83a0cc3a4b23237ef

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

        private string textSourceProperty = "Hello world!";
        public string TextSourceProperty
        {
            get { return textSourceProperty; }
            set
            {
                textSourceProperty = value;
                NotifyChanged();
            }
        }

        private Point point = new Point() { X = 100, Y = 200 };
        public Point Point
        {
            get { return point; }
            set
            {
                if (point != value)
                {
                    point = value;
                    NotifyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
