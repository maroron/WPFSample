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
            var greetings = new List<GreetingBase>()
            {
                new GreetingMorning(),
                new GreetingAfternoon(),
                new GreetingEvening(),
            };

            foreach (var obj in greetings)
            {
                string msg = obj.GetMessage();
                Console.WriteLine(msg);
            }
        }
    }
}
