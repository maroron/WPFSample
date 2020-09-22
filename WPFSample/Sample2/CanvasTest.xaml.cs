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
using WPFSample.Sample2.Data;

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

        // The part of the rectangle under the mouse.
        private HitType MouseHitType = HitType.None;

        /// <summary>
        /// Roi選択および削除モードでmouseoverしたときにROI
        /// </summary>
        private Roi nearestRect = new Roi(0, 0, 0, 0);

        /// <summary>
        /// BrightnessRoiリスト
        /// </summary>
        private List<Roi> rois = new List<Roi>();

        /// <summary>
        /// コンボボックスから選択されたROIアウトラインを強調表示する
        /// </summary>
        private int selectedRoiIndex;

        /// <summary>
        /// ROIサイズ変更と移動ため
        /// </summary>
        private Roi selectedRoiRect = new Roi(0, 0, 0, 0);

        /// <summary>
        /// ROIのサイズ変更と移動を実行します。
        /// </summary>
        public bool hasSelectedRoi = false;

        /// <summary>
        /// マウスドラッグ中ならtrue
        /// </summary>
        private bool _inDrag = false;
        /// <summary>
        /// ROI drawとresize中ならtrue
        /// </summary>
        private bool _inDraw = false;

        public CanvasTest()
        {
            InitializeComponent();
            this.DataContext = this;
            DisplayService.Entry(this.canvas);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(this.canvas);
            _inDrag = false;

            switch (CurrentButtonState)
            {
                case ButtonState.None:
                    break;
                case ButtonState.Add:
                    Roi clickedRoi = CreateRoi(clickedPoint);
                    this.nearestRect = clickedRoi;
                    this.rois.Add(clickedRoi);
                    DrawRoi(clickedRoi);
                    break;
                case ButtonState.Select:
                    _inDrag = true;

                    int index = 0;
                    selectedRoiIndex = -1;
                    selectedRoiRect = new Roi(0, 0, 0, 0);

                    this.hasSelectedRoi = false;
                    foreach (Roi roi in this.rois)
                    {
                        if (roi.Equals(this.nearestRect))
                        {
                            selectedRoiIndex = index;
                            selectedRoiRect = roi;
                            hasSelectedRoi = true;

                            break;
                        }
                        index += 1;
                    }
                    break;
                case ButtonState.Delete:
                    break;
                default:
                    break;
            }
        }

        private void RoiMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = e.GetPosition(this.canvas);

            if (CurrentButtonState == ButtonState.Select)
            {
                if (_inDrag)
                {
                    MoveRoi(mousePoint, e.LeftButton);
                }
                this.ChangeColorBrightnessRoi(mousePoint);
            }
        }

        private void ChangeColorBrightnessRoi(Point mousePoint)
        {
            int indexNearestRect = 0;
            DrawRois();

            foreach (var roi in rois)
            {
                if (!roi.Rect.Contains(mousePoint))
                {
                    continue;
                }

                // マウスポイントと中心位置が一番近いROIを検索する
                double distance = CalculateDistanceSquared(roi.Center, mousePoint);
                double distanceNearest = CalculateDistanceSquared(this.nearestRect.Center, mousePoint);

                if (distance <= distanceNearest)
                {
                    this.nearestRect = roi;
                    indexNearestRect = rois.IndexOf(roi);
                }
            }

            bool hasNearestRoiRect = rois.Any(x => x.Rect.Contains(mousePoint));
            if (hasNearestRoiRect)
            {
                var path = CreatePath(this.nearestRect.Rect, CreateColorBrush());
                DisplayService.Replace(indexNearestRect, path);
                this.rois[indexNearestRect] = this.nearestRect;
            }
        }

        private void MoveRoi(Point mousePoint, MouseButtonState leftButton)
        {
            if (CurrentButtonState == ButtonState.Select && this.hasSelectedRoi)
            {
                if (leftButton == MouseButtonState.Pressed)
                {
                    if (!_inDraw && _inDrag)
                    {
                        Roi roi = CreateRoi(mousePoint);

                        this.rois[selectedRoiIndex] = roi;

                        DrawRois();
                    }
                    // resize ROI
                    //else if (_inDraw && !_inDrag)
                    //{
                    //    this.ResizeSelectedRoi(clickedPoint);
                    //}
                    //else
                    //{
                    //    // Not implemented
                    //}
                    //anchorPoint = clickedPoint;
                }
                else
                {
                    // change mouse point when mouse is on the selected ROI
                    //if (indexNearestRect == selectedRoiIndex)
                    //{
                    //    MouseHitType = SetHitType(clickedPoint);
                    //    SetMouseCursor();
                    //    if (MouseHitType == HitType.Body)
                    //    {
                    //        _inDrag = true;
                    //        _inDraw = false;
                    //    }
                    //    else if (MouseHitType != HitType.None)
                    //    {
                    //        _inDrag = false;
                    //        _inDraw = true;
                    //    }
                    //    else
                    //    {
                    //        _inDrag = false;
                    //        _inDraw = false;
                    //    }
                    //}
                    //else
                    //{
                    //    MouseHitType = HitType.None;
                    //    Mouse.OverrideCursor = Cursors.Arrow;
                    //}
                }
            }
        }

        private double CalculateDistanceSquared(Point point1, Point point2)
        {
            return (point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y);
        }

        private void DrawRoi(Roi roi)
        {
            var path = CreatePath(roi.Rect, new SolidColorBrush(Colors.Blue));
            DisplayService.Add(path);
        }

        public void DrawRois()
        {
            DisplayService.Clear();
            foreach (Roi roi in rois)
            {
                DrawRoi(roi);
            }
        }

        private Roi CreateRoi(Point point)
        {
            var lefttopX = Math.Max(Math.Min(point.X + 50, canvas.ActualWidth) - 100, 0);
            var lefttopY = Math.Max(Math.Min(point.Y + 50, canvas.ActualHeight) - 100, 0);
            var roi = new Roi(lefttopX, lefttopY, 100, 100);
            return roi;
        }

        private Path CreatePath(Rect rect, Brush stroke)
        {
            Path path = new Path
            {
                Data = new RectangleGeometry(rect),
                Stroke = stroke,
                StrokeThickness = 2,
            };
            return path;
        }

        private SolidColorBrush CreateColorBrush()
        {
            SolidColorBrush stroke;
            switch (CurrentButtonState)
            {
                case ButtonState.None:
                    stroke = new SolidColorBrush(Colors.Blue);
                    break;
                case ButtonState.Add:
                    stroke = new SolidColorBrush(Colors.Blue);
                    break;
                case ButtonState.Select:
                    stroke = new SolidColorBrush(Colors.Green);
                    break;
                case ButtonState.Delete:
                    stroke = new SolidColorBrush(Colors.OrangeRed);
                    break;
                default:
                    stroke = new SolidColorBrush(Colors.Blue);
                    break;
            }
            return stroke;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CurrentButtonState = CurrentButtonState == ButtonState.Add ? ButtonState.None : ButtonState.Add;
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            CurrentButtonState = CurrentButtonState == ButtonState.Select ? ButtonState.None : ButtonState.Select;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CurrentButtonState = CurrentButtonState == ButtonState.Delete ? ButtonState.None : ButtonState.Delete;
        }

        /// <summary>
        /// Resize range select rectangle
        /// </summary>
        /// <param name="point">マウスポインターの座標</param>
        private HitType SetHitType(Point point)
        {
            point = new Point(Math.Round(point.X, 1), Math.Round(point.Y, 1));
            //anchorPoint = point;

            Roi moveRect = this.rois.ElementAt(selectedRoiIndex);
            // Return a HitType value to indicate what is at the point.
            double left = Math.Round(moveRect.Rect.X, 1);
            double top = Math.Round(moveRect.Rect.Y, 1);
            double right = left + moveRect.Rect.Width;
            double bottom = top + moveRect.Rect.Height;
            double widthMiddle = Math.Round(left + (moveRect.Rect.Width / 2), 1);
            double heightMiddle = Math.Round(top + (moveRect.Rect.Height / 2), 1);

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
