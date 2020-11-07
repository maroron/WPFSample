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

namespace WPFSample
{
    /// <summary>
    /// PopupTest.xaml の相互作用ロジック
    /// </summary>
    public partial class PopupTest : Window
    {
        public PopupTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var popups = new[]
            {
                this.popup1,
                this.popup2,
                this.popup3,
                this.popup4
            };
            foreach (var popup in popups)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }
    }
}
