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

namespace WPFSample.Sample2
{
    /// <summary>
    /// CanvasTest.xaml の相互作用ロジック
    /// </summary>
    public partial class CanvasTest : Window
    {
        private enum ButtonState
        {
            None,
            Add,
            Select,
            Delete,
        }

        private ButtonState currentButtonState = ButtonState.None;

        public CanvasTest()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(this);

            switch (currentButtonState)
            {
                case ButtonState.None:
                    break;
                case ButtonState.Add:
                    break;
                case ButtonState.Select:
                    break;
                case ButtonState.Delete:
                    break;
                default:
                    break;
            }

        }
    }
}
