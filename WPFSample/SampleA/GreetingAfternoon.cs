using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.SampleA
{
    class GreetingAfternoon : GreetingBase, IGreeting
    {
        public string GetGreeting()
        {
            return "Afternoon Good";
        }

        public override string GetMessage()
        {
            return "Good Afternoon";
        }
    }
}
