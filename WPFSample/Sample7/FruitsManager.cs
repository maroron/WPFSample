using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Sample7
{
    class FruitsManager : INotifyPropertyChanged
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

        private string textSourceProperty = "Hello world!";
        public string TextSourceProperty
        {
            get { return textSourceProperty; }
            set
            {
                textSourceProperty = value;
                NotifyChanged("TextSourceProperty");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
