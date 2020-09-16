using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class CanvasTest : Window, INotifyPropertyChanged
    {
        public enum ButtonState
        {
            None,
            Add,
            Select,
            Delete,
        }

        private ButtonState buttonState = ButtonState.None;
        public ButtonState CurrentButtonState
        {
            get { return buttonState; }
            set
            {
                if (value != buttonState)
                {
                    buttonState = value;
                    NotifyChanged();
                }
            }
        }

        public CanvasTest()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool isSelectedMode = false;
            Point clickedPoint = e.GetPosition(this);

            switch (CurrentButtonState)
            {
                case ButtonState.None:
                    break;
                case ButtonState.Add:
                    SolidColorBrush stroke = isSelectedMode ? new SolidColorBrush(Color.FromRgb(255, 255, 0)) : new SolidColorBrush(Color.FromRgb(0, 170, 255));
                    Rect rect = new Rect(clickedPoint.X - 20, clickedPoint.Y - 20, 40, 40);
                    Path path = new Path
                    {
                        Data = new RectangleGeometry(rect),
                        Stroke = stroke,
                        StrokeThickness = 6,
                    };
                    this.canvas.Children.Add(path);

                    break;
                case ButtonState.Select:
                    break;
                case ButtonState.Delete:
                    break;
                default:
                    break;
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentButtonState != ButtonState.Add)
            {
                CurrentButtonState = ButtonState.Add;
            }
            else
            {
                CurrentButtonState = ButtonState.None;
            }
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentButtonState != ButtonState.Select)
            {
                CurrentButtonState = ButtonState.Select;
            }
            else
            {
                CurrentButtonState = ButtonState.None;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentButtonState != ButtonState.Delete)
            {
                CurrentButtonState = ButtonState.Delete;
            }
            else
            {
                CurrentButtonState = ButtonState.None;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
