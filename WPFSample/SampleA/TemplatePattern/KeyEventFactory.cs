using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSample.SampleA.TemplatePattern
{
    class KeyEventFactory
    {
        private static Window window;

        private static Dictionary<string, TemplateSample> keyEventRegisters = new Dictionary<string, TemplateSample>()
        {
            { "key1", new ConcreteTemplateSample() },
            { "key2", new ConcreteTemplateSample2() },
        };

        public static void Entry(Window w)
        {
            window = w;
        }

        public static TemplateSample SetKeyEvent(string key)
        {
            foreach (var keyEvnet in keyEventRegisters.Values)
            {
                window.KeyDown -= keyEvnet.KeyDown;
            } 

            var sample = keyEventRegisters[key];
            window.KeyDown += sample.KeyDown;

            return sample;
        }
    }
}
