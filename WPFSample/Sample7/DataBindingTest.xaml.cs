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
        private FruitsManager fm = new FruitsManager();

        public DataBindingTest()
        {
            InitializeComponent();
            DataContext = fm;
            //DataContext = new Point() { X = 100, Y = 200 };
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            fm.Fruits.Add(new Fruit() { Name = "Grape", Count = 10 });
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int index = fm.Fruits.Count - 1;
            if (0 <= index)
            {
                fm.Fruits.RemoveAt(index);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ソースプロパティを更新するだけでは値は通知されない
            fm.TextSourceProperty = this.text1.Text;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
