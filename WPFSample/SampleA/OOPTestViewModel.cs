using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    class OOPTestViewModel : INotifyPropertyChanged
    {
        private string textProperty;
        public string TextProperty 
        {
            get { return textProperty; }
            set 
            {
                if (value != textProperty)
                {
                    textProperty = value;
                    NotifyChanged();
                }
            }
        }

        public OOPTestViewModel()
        {
            var greeting = InitializeGreeting();
            TextProperty = greeting;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string InitializeGreeting()
        {
            var greetings = new List<GreetingBase>()
            {
                new GreetingMorning(),
                new GreetingAfternoon(),
                new GreetingEvening(),
            };

            StringBuilder sb = new StringBuilder();
            foreach (var obj in greetings)
            {
                string msg = obj.GetMessage();
                sb.Append(msg)
                  .Append(Environment.NewLine);
            }

            sb.Append(Environment.NewLine);

            foreach (IGreeting obj in greetings)
            {
                string msg = obj.GetGreeting();
                sb.Append(msg)
                  .Append(Environment.NewLine);
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private void NotifyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
