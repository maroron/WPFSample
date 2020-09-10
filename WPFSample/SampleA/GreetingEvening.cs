using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    class GreetingEvening : GreetingBase, IGreeting
    {
        public string GetGreeting()
        {
            return "Evening Good";
        }

        public override string GetMessage()
        {
            return "Good Evening";
        }
    }
}
