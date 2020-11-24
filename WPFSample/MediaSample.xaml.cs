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

        private ImageData imageData { get; set; }

        public MediaSample()
        {
            InitializeComponent();

            this.imageData = new ImageData(512, 512);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Random random = new System.Random();
            for (int vertex = 0; vertex < imageData.Height; ++vertex)
            {
                for (int horizon = 0; horizon < imageData.Width; ++horizon)
                {
                    double randnum = random.NextDouble();
                    this.imageData.Data[vertex * imageData.Height + horizon] = (float)randnum;
                }
            }

            this.displayImage.Source = CreateBitMapsource(this.imageData);
        }

        private void Gaussian_Button_Click(object sender, RoutedEventArgs e)
        {
            var filterdImage = Filter2D(FilterType.Gauss, this.imageData, 3);
            this.displayImage.Source = CreateBitMapsource(filterdImage);
        }

        private BitmapSource CreateBitMapsource(ImageData image)
        {
            int stride = image.Width * PixelFormats.Gray32Float.BitsPerPixel / 8;
            BitmapSource bitmap = BitmapSource.Create(512, 512, 96, 96, PixelFormats.Gray32Float, null, image.Data, stride);
            return bitmap;
        }

        private ImageData Filter2D(FilterType type, ImageData src, int kernelSize)
        {
            var dst = new ImageData(512, 512);
            int radius = (kernelSize - 1) / 2;
            float N = kernelSize * kernelSize;

            for (int h = radius; h < src.Height - radius; h++)
            {
                for (int w = radius; w < src.Width - radius; w++)
                {
                    float sum = 0.0f;
                    for (int kH = h - radius; kH < h - radius + kernelSize; kH++)
                    {
                        for (int kW = w - radius; kW < w - radius + kernelSize; kW++)
                        {
                            var temp = kH * src.Width + kW;
                            float testt = src.Data[temp];
                            sum += testt;
                        }
                    }
                    float test = sum / N;
                    dst.Data[h * src.Width + w] = test; 
                }
            }
            return dst;
        }
    }

    class ImageData
    {
        public float[] Data { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public ImageData(int width, int height)
        {
            this.Data = new float[width * height];
            this.Width = width;
            this.Height = height;

            for (int h = 0; h < this.Height; h++)
            {
                for (int w = 0; w < this.Width; w++)
                {
                    this.Data[h * Width + w] = (float)h / (float)this.Height;
                }
            }
        }

        public ImageData(int width, int height, float[] data)
        {
            this.Data = data;
            this.Width = width;
            this.Height = height;
        }

        public ImageData(ImageData data)
        {
            this.Data = new float[data.Width * data.Height];
            this.Width = data.Width;
            this.Height = data.Height;
        }
    }
}
