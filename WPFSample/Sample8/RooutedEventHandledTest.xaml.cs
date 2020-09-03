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

namespace WPFSample.Sample8
{
    /// <summary>
    /// RooutedEventHandledTest.xaml の相互作用ロジック
    /// </summary>
    public partial class RooutedEventHandledTest : Window
    {
        public RooutedEventHandledTest()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Button_PreviewMouseRightButtonDown");
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Button_MouseRightButtonDown");
        }

        private void StackPanel_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StackPanel_PreviewMouseRightButtonDown");
        }

        private void StackPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StackPanel_MouseRightButtonDown");
        }

        private void DockPanel_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("DockPanel_PreviewMouseRightButtonDown");
        }

        private void DockPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("DockPanel_MouseRightButtonDown");
        }
    }
}
