using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSample.Sample2.Data
{
    public class Roi
    {
        public Rect Rect { get; set; }

        public Point Center { get; set; }

        public Roi(double x, double y, double width, double height)
        {
            Rect = new Rect(x, y, width, height);
            Center = new Point(x + 0.5 * width, y + 0.5 * height);
        }
    }
}
