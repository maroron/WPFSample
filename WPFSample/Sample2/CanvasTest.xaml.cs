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

        // The part of the rectangle under the mouse.
        private HitType MouseHitType = HitType.None;

        /// <summary>
        /// Roi選択および削除モードでmouseoverしたときにROI
        /// </summary>
        private Rect nearestRect = new Rect(0, 0, 0, 0);

        /// <summary>
        /// BrightnessRoiリスト
        /// </summary>
        private List<Rect> roiRectList = new List<Rect>();

        /// <summary>
        /// コンボボックスから選択されたROIアウトラインを強調表示する
        /// </summary>
        private int selectedRoiIndex;

        /// <summary>
        /// ROIサイズ変更と移動ため
        /// </summary>
        private Rect selectedRoiRect = new Rect(0, 0, 0, 0);

        /// <summary>
        /// ROIのサイズ変更と移動を実行します。
        /// </summary>
        public bool hasSelectedRoi = false;

        /// <summary>
        /// Roi選択および削除モードでmouseoverしたときにROIを強調表示する
        /// </summary>
        private bool hasNearestRoiRect = false;

        /// <summary>
        /// ハイライトされたROIのインデックス
        /// </summary>
        private int indexNearestRect;

        public CanvasTest()
        {
            InitializeComponent();
            this.DataContext = this;
            DisplayService.Entry(this.canvas);
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(this.canvas);

            switch (CurrentButtonState)
            {
                case ButtonState.None:
                    break;
                case ButtonState.Add:
                    Rect rect = new Rect(clickedPoint.X - 50, clickedPoint.Y - 50, 100, 100);
                    DrawRoi(rect);
                    this.roiRectList.Add(rect);
                    break;
                case ButtonState.Select:
                    int index = 0;
                    selectedRoiIndex = -1;
                    selectedRoiRect = new Rect(0, 0, 0, 0);

                    foreach (Rect iRect in this.roiRectList)
                    {
                        this.hasSelectedRoi = false;
                        if (iRect.Equals(this.nearestRect))
                        {
                            selectedRoiIndex = index;
                            selectedRoiRect = iRect;
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

            bool isSelect = CurrentButtonState == ButtonState.Select;

            this.ChangeColorBrightnessRoi(mousePoint, isSelect);
        }

        private void ChangeColorBrightnessRoi(Point mousePoint, bool isSelectMode)
        {
            // マウスが内部にあるroiRectを抽出
            List<Rect> mouseContainRoiRect = new List<Rect>();
            List<int> indexMouseContainRoiRect = new List<int>();
            int index = 0;
            foreach (Rect iRoiRect in roiRectList)
            {
                if (iRoiRect.Contains(mousePoint))
                {
                    mouseContainRoiRect.Add(iRoiRect);
                    indexMouseContainRoiRect.Add(index);
                }
                else
                {
                    // Not implemented
                }
                index += 1;
            }

            DrawRois();
            this.hasNearestRoiRect = false;

            if (mouseContainRoiRect.Count > 0)
            {
                this.nearestRect = mouseContainRoiRect[0];
                indexNearestRect = indexMouseContainRoiRect[0];
                int indexRect = 0;
                foreach (Rect iRoiRect in mouseContainRoiRect)
                {
                    // iroiRectとマウスポイントとの距離
                    Point centerRoi = new Point(iRoiRect.X + 0.5 * iRoiRect.Width, iRoiRect.Y + 0.5 * iRoiRect.Height);
                    double distance = CalculateDistanceSquared(centerRoi, mousePoint);

                    // nearestRectとマウスポイントとの距離
                    Point centernearestRoi =
                        new Point(this.nearestRect.X + 0.5 * this.nearestRect.Width, this.nearestRect.Y + 0.5 * this.nearestRect.Height);
                    double distanceNearest = CalculateDistanceSquared(centernearestRoi, mousePoint);

                    if (distance <= distanceNearest)
                    {
                        this.nearestRect = iRoiRect;
                        indexNearestRect = indexMouseContainRoiRect[indexRect];
                    }
                    indexRect += 1;
                }

                Rect roi = new Rect(
                    this.nearestRect.X,
                    this.nearestRect.Y,
                    this.nearestRect.Width,
                    this.nearestRect.Height
                    );

                SolidColorBrush stroke = isSelectMode ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Color.FromRgb(255, 82, 82));
                Path path = new Path
                {
                    Data = new RectangleGeometry(roi),
                    Stroke = stroke,
                    StrokeThickness = 2,
                };
                DisplayService.Add(path);
                this.roiRectList.Add(roi);

                this.hasNearestRoiRect = true;
            }
            else
            {
                MouseHitType = HitType.None;
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private double CalculateDistanceSquared(Point point1, Point point2)
        {
            return (point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y);
        }

        private void DrawRoi(Rect rect)
        {
            bool isSelectedMode = false;
            SolidColorBrush stroke = isSelectedMode ? new SolidColorBrush(Color.FromRgb(255, 255, 0)) : new SolidColorBrush(Color.FromRgb(0, 170, 255));
            Path path = new Path
            {
                Data = new RectangleGeometry(rect),
                Stroke = stroke,
                StrokeThickness = 2,
            };
            DisplayService.Add(path);
        }

        public void DrawRois()
        {
            foreach (Rect iRectRoi in roiRectList)
            {
                DrawRoi(iRectRoi);
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
