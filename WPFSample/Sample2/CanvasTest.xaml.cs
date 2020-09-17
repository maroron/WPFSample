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

        // The part of the rectangle the mouse is over.
        private enum HitType
        {
            None, Body, TopLeft, TopRight, BottomRight, BottomLeft, Top, Bottom, Left, Right
        };

        /// <summary>
        /// BrightnessRoiリスト
        /// </summary>
        private List<Rect> roiRectList = new List<Rect>();

        /// <summary>
        /// コンボボックスから選択されたROIアウトラインを強調表示する
        /// </summary>
        private int selectedRoiIndex;


        public CanvasTest()
        {
            InitializeComponent();
            this.DataContext = this;
            DisplayService.Entry(this.canvas);
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool isSelectedMode = false;
            Point clickedPoint = e.GetPosition(this.canvas);

            switch (CurrentButtonState)
            {
                case ButtonState.None:
                    break;
                case ButtonState.Add:
                    SolidColorBrush stroke = isSelectedMode ? new SolidColorBrush(Color.FromRgb(255, 255, 0)) : new SolidColorBrush(Color.FromRgb(0, 170, 255));
                    Rect rect = new Rect(clickedPoint.X - 50 , clickedPoint.Y - 50, 100, 100);
                    Path path = new Path
                    {
                        Data = new RectangleGeometry(rect),
                        Stroke = stroke,
                        StrokeThickness = 2,
                    };
                    DisplayService.Add(path);
                    this.roiRectList.Add(rect);

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

        /// <summary>
        /// Resize range select rectangle
        /// </summary>
        /// <param name="point">マウスポインターの座標</param>
        private HitType SetHitType(Point point)
        {
            point = new Point(Math.Round(point.X, 1), Math.Round(point.Y, 1));
            //anchorPoint = point;

            Rect moveRect = this.roiRectList.ElementAt(selectedRoiIndex);
            // Return a HitType value to indicate what is at the point.
            double left = Math.Round(moveRect.X, 1);
            double top = Math.Round(moveRect.Y, 1);
            double right = left + moveRect.Width;
            double bottom = top + moveRect.Height;
            double widthMiddle = Math.Round(left + (moveRect.Width / 2), 1);
            double heightMiddle = Math.Round(top + (moveRect.Height / 2), 1);

            const int GAP = 10;
            if (Math.Abs(point.X - left) < GAP)
            {
                // Left edge.
                if (Math.Abs(point.Y - top) < GAP)
                {
                    return HitType.TopLeft;
                }
                else if (Math.Abs(bottom - point.Y) < GAP)
                {
                    return HitType.BottomLeft;
                }
                // Left middle point
                else if (Math.Abs(point.Y - heightMiddle) < GAP)
                {
                    return HitType.Left;
                }
                else
                {
                    // Not implemented
                }
            }
            else if (Math.Abs(point.X - right) < GAP)
            {
                // Right edge.
                if (Math.Abs(point.Y - top) < GAP)
                {
                    return HitType.TopRight;
                }
                else if (Math.Abs(bottom - point.Y) < GAP)
                {
                    return HitType.BottomRight;
                }
                // Right middle point
                else if (Math.Abs(point.Y - heightMiddle) < GAP)
                {
                    return HitType.Right;
                }
                else
                {
                    // Not implemented
                }
            }
            else if (Math.Abs(point.X - widthMiddle) < GAP)
            {
                // Top Bottom middle points
                if (Math.Abs(point.Y - top) < GAP)
                {
                    return HitType.Top;
                }
                else if (Math.Abs(point.Y - bottom) < GAP)
                {
                    return HitType.Bottom;
                }
                else
                {
                    // Not implemented
                }
            }
            else if (point.X > left && point.X < right &&
                point.Y > top && point.Y < bottom)
            {
                return HitType.Body;
            }
            else
            {
                // Not implemented
            }
            return HitType.None;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
