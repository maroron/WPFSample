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

namespace WPFSample.Sample4
{
    /// <summary>
    /// DialogTest.xaml の相互作用ロジック
    /// </summary>
    public partial class DialogTest : Window
    {
        public DialogTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Show() と Visibility.Visible はモードレスウィンドウを表示する。効果は全く同じ。
            // データバインディングで行う場合はプロパティが必要で、
            // プログラムで表示する場合はメソッドが必要となる。
            // ShowDialog() はモーダルウィンドウを表示する。


            Button button = sender as Button;
            if (button.Name == "show_button")
            {
                Window w = new Window();
                w.Title = "Show";
                w.Show();
            }
            else if (button.Name == "visibility_button")
            {
                Window w = new Window();
                w.Title = "Visibility = Visible";
                w.Visibility = Visibility.Visible;
            }
            else // showDialog_button
            {
                Window w = new Window();
                w.Title = "ShowDialog";
                w.Owner = this;
                w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                w.ShowInTaskbar = true;
                w.ShowDialog();
            }
        }
    }
}
