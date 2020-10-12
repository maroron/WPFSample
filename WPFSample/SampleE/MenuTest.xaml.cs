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

namespace WPFSample.SampleE
{
    /// <summary>
    /// MenuTest.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuTest : Window
    {
        public MenuTest()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.contextmenu.IsOpen)
            {
                this.contextmenu.IsOpen = true;
            }
        }
    }
}
