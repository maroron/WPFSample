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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFSample.Sample1;
using WPFSample.Sample2;
using WPFSample.Sample3;

namespace WPFSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Sample1_Click(object sender, RoutedEventArgs e)
        {
            var sample = new ProgressTest();
            sample.Show();
        }

        private void Sample2_Click(object sender, RoutedEventArgs e)
        {
            var sample = new CanvasTest();
            sample.Show();
        }

        private void Sample3_Click(object sender, RoutedEventArgs e)
        {
            var sample = new HowToUseList();
            sample.Show();
        }

        private void Sample4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample5_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Sample6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample9_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Sample10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample11_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample12_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample13_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample14_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample15_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sample16_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
