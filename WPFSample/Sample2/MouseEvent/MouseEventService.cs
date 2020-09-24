using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSample.Sample2.MouseEvent
{
    class MouseEventService
    {
        private static Window w;

        private static Dictionary<string, MouseEventBase> mouseEventRegisters;

        public static void Entry(Window w)
        {
            MouseEventService.w = w;
            mouseEventRegisters = new Dictionary<string, MouseEventBase>()
            {
                { "mouse1", new RoiMouseEvent(w) },
            };
            SetPreviewMouseMove("mouse1");
            SetPreviewMouseDown("mouse1");
        }

        private static void SetPreviewMouseDown(string key)
        {
            foreach (var mouseEvent in mouseEventRegisters.Values)
            {
                w.PreviewMouseDown -= mouseEvent.PreviewMouseDown;
            } 

            var keyevent = mouseEventRegisters[key];
            w.PreviewMouseDown += keyevent.PreviewMouseDown;
        }

        private static void SetPreviewMouseMove(string key)
        {
            foreach (var mouseEvent in mouseEventRegisters.Values)
            {
                w.PreviewMouseMove -= mouseEvent.PreviewMouseMove;
            } 

            var keyevent = mouseEventRegisters[key];
            w.PreviewMouseMove += keyevent.PreviewMouseMove;
        }
    }
}
