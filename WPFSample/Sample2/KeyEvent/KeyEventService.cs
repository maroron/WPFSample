using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFSample.Sample2.KeyEvent
{
    class KeyEventService
    {
        private static Window w;

        private static Dictionary<string, KeyEventBase> keyEventRegisters;

        public static void Entry(Window w)
        {
            KeyEventService.w = w;
            keyEventRegisters = new Dictionary<string, KeyEventBase>()
            {
                { "key1", new RoiKeyEvent(w) },
            };
            SetKeyEvent("key1");
        }

        public static void SetKeyEvent(string key)
        {
            foreach (var keyEvnet in keyEventRegisters.Values)
            {
                w.PreviewKeyDown -= keyEvnet.PreviewKeyDown;
            } 

            var keyevent = keyEventRegisters[key];
            w.PreviewKeyDown += keyevent.PreviewKeyDown;
        }
    }
}
