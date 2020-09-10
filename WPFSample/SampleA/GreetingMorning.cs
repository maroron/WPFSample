using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    class GreetingMorning : GreetingBase, IGreeting
    {
        public string GetGreeting()
        {
            return "Morning Good";
        }

        public override string GetMessage()
        {
            return "Good Morning";
        }
    }
}
