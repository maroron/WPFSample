using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    class OOPTestViewModel
    {
        public string TextProperty { get; set; }

        public OOPTestViewModel()
        {
            var greeting = InitializeGreeting();
            TextProperty = greeting;
        }

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

            return sb.ToString();
        }
    }
}
