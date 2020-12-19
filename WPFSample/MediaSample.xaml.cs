using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            this.imageData = Filter2D(FilterType.Gauss, this.imageData);
            this.displayImage.Source = CreateBitMapsource(this.imageData);
        }

        private BitmapSource CreateBitMapsource(ImageData image)
        {
            int stride = image.Width * PixelFormats.Gray32Float.BitsPerPixel / 8;
            BitmapSource bitmap = BitmapSource.Create(512, 512, 96, 96, PixelFormats.Gray32Float, null, image.Data, stride);
            return bitmap;
        }

        private ImageData Filter2D(FilterType type, ImageData src)
        {
            float[,] kernel = GetFilter(type);
            var dst = new ImageData(512, 512);
            int kernelW = kernel.GetLength(0);
            int kernelH = kernel.GetLength(1);
            int radius = (kernelH - 1) / 2;

            for (int h = radius; h < src.Height - radius; h++)
            {
                for (int w = radius; w < src.Width - radius; w++)
                {
                    float sum = 0.0f;
                    for (int kH = h - radius, ky = 0; kH < h - radius + kernelH; kH++, ky++)
                    {
                        for (int kW = w - radius, kx = 0; kW < w - radius + kernelW; kW++, kx++)
                        {
                            sum += src.Data[kH * src.Width + kW] * kernel[ky, kx];
                        }
                    }
                    dst.Data[h * src.Width + w] = sum; 
                }
            }
            return dst;
        }

        private float[,] GetFilter(FilterType type)
        {
            float[,] filter;

            switch (type)
            {
                case FilterType.Mean:
                    float[,] mean = {
                        { 1.0f/9.0f, 1.0f/9.0f, 1.0f/9.0f},
                        { 1.0f/9.0f, 1.0f/9.0f, 1.0f/9.0f},
                        { 1.0f/9.0f, 1.0f/9.0f, 1.0f/9.0f},
                    };
                    filter = mean;
                    break;
                case FilterType.Gauss:
                    float[,] gauss = {
                        { 1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
                        { 1.0f/ 8.0f, 1.0f/4.0f, 1.0f/ 8.0f},
                        { 1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
                    };
                    filter = gauss;
                    break;
                case FilterType.Median:
                    float[,] median = {
                        { 1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
                        { 1.0f/ 8.0f, 1.0f/4.0f, 1.0f/ 8.0f},
                        { 1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
                    };
                    filter = median;
                    break;
                default:
                    float[,] none = {
                        { 1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
                        { 1.0f/ 8.0f, 1.0f/4.0f, 1.0f/ 8.0f},
                        { 1.0f/16.0f, 1.0f/8.0f, 1.0f/16.0f},
                    };
                    filter = none;
                    break;
            }

            return filter;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 画像を開く
            var dialog = new OpenFileDialog();
            dialog.Filter = "画像|*.jpg;*.jpeg;*.png;*.bmp";
            if (dialog.ShowDialog() != true)
            {
                return;
            }
            // ファイルをメモリにコピー
            var ms = new MemoryStream();
            using (var s = new FileStream(dialog.FileName, FileMode.Open))
            { s.CopyTo(ms); }
            // ストリームの位置をリセット
            ms.Seek(0, SeekOrigin.Begin);
            // ストリームをもとにBitmapImageを作成 
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.EndInit();

            int stride = bmp.PixelWidth * PixelFormats.Gray32Float.BitsPerPixel / 8;
            bmp.CopyPixels(this.imageData.Data, stride, 0);

            // BitmapImageをSourceに指定して画面に表示する
            this.displayImage.Source = bmp;
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
