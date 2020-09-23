using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFSample.Sample2.KeyEvent
{
    class RoiKeyEvent : KeyEventBase
    {
        private Canvas canvas;
        private CanvasTest window;

        public RoiKeyEvent(Window w) : base(w)
        {
            window = w as CanvasTest;
            canvas = window.canvas;
        }

        protected override bool IsValidKey(Key key)
        {
            bool isValid = false;
            switch (key)
            {
                case Key.A:
                case Key.W:
                case Key.D:
                case Key.S:
                    isValid = true;
                    break;
                default:
                    isValid = false;
                    break;
            }
            return isValid;
        }

        protected override void Process(object sender, KeyEventArgs e)
        {
            if (window.hasSelectedRoi)
            {
                var rois = window.GetRois();
                var roi = rois[window.selectedRoiIndex];
                var step = ComputePoint(e.Key);
                rois[window.selectedRoiIndex] = window.CreateRoi(new Point(roi.Center.X + step.X, roi.Center.Y + step.Y));

                window.DrawRois();
            }
            else
            {

            }
        }

        private Point ComputePoint(Key key)
        {
            var point = new Point();
            switch (key)
            {
                case Key.A:
                    point = new Point(-10, 0);
                    break;
                case Key.W:
                    point = new Point(0, -10);
                    break;
                case Key.D:
                    point = new Point(10, 0);
                    break;
                case Key.S:
                    point = new Point(0, 10);
                    break;
                default:
                    break;
            }
            return point;
        }
    }
}
