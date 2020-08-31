using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFSample.Sample7
{
    /// <summary>
    /// DataBindingTest.xaml の相互作用ロジック
    /// </summary>
    public partial class DataBindingTest : Window
    {
        public DataBindingTest()
        {
            InitializeComponent();
            //DataContext = new FruitsManager();
            DataContext = new Point() { X = 100, Y = 200 };
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
