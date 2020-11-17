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
    /// MediaSample.xaml の相互作用ロジック
    /// </summary>
    public partial class MediaSample : Window
    {
        private enum FilterType
        {
            Mean,
            Gauss,
            Median,
        }

        public MediaSample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float[] data = new float[512 * 512];

            System.Random random = new System.Random();
            for (int vertex = 0; vertex < 512; ++vertex)
            {
                for (int horizon = 0; horizon < 512; ++horizon)
                {
                    double randnum = random.NextDouble();
                    data[vertex * 512 + horizon] = (float)randnum * 2.0f - 1.0f;
                }
            }

            int stride = 512 * PixelFormats.Gray32Float.BitsPerPixel / 8;
            BitmapSource bitmap = BitmapSource.Create(512, 512, 96, 96, PixelFormats.Gray32Float, null, data, stride);
            this.displayImage.Source = bitmap;
        }

        private void Gaussian_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //private float[] Filter2D(FilterType type, float[] src, int kernelSize)
        //{
        //    float[] dst = new float[;
        //    return dst;
        //}
    }
}
