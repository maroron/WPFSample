﻿using Microsoft.Win32;
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
using System.Drawing;
using System.Drawing.Imaging;

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

        private ImageData32 imageData { get; set; }

        public MediaSample()
        {
            InitializeComponent();

            this.imageData = new ImageData32(512, 512);
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

        private BitmapSource CreateBitMapsource(ImageData32 image)
        {
            int stride = image.Width * PixelFormats.Gray32Float.BitsPerPixel / 8;
            BitmapSource bitmap = BitmapSource.Create(512, 512, 96, 96, PixelFormats.Gray32Float, null, image.Data, stride);
            return bitmap;
        }

        private ImageData32 Filter2D(FilterType type, ImageData32 src)
        {
            float[,] kernel = GetFilter(type);
            var dst = new ImageData32(512, 512);
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

        private void OpenFile_Click(object sender, RoutedEventArgs e)
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
            {
                s.CopyTo(ms);
            }

            // ストリームの位置をリセット
            ms.Seek(0, SeekOrigin.Begin);

            // ストリームをもとにBitmapImageを作成 
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.Rotation = Rotation.Rotate90; // rotateをプロパティで設定するには、BeginInitとEndInitの間で行う必要があるみたい。
            bmp.EndInit();

            if (bmp.PixelWidth < 512 && bmp.PixelHeight < 512) // 512 までしか許可してない
            {
                int stride = bmp.PixelWidth * PixelFormats.Gray32Float.BitsPerPixel / 8;
                bmp.CopyPixels(this.imageData.Data, stride, 0);

                // BitmapImageをSourceに指定して画面に表示する
                this.displayImage.Source = bmp;
            }
            else
            {
                MessageBox.Show("Invalid Format");
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "画像|*.jpg;*.jpeg;*.png;*.bmp";
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var width = this.imageData.Width;
            var height = this.imageData.Height;
            using (Bitmap saveImg = new Bitmap(width, height))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = (int)(this.imageData.Data[width * y + x] * 255);
                        saveImg.SetPixel(x, y, System.Drawing.Color.FromArgb(pixel, pixel, pixel));
                    }
                }
                saveImg.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }

        private RotateFlipType nowRotate = RotateFlipType.RotateNoneFlipNone;

        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {
            var width = this.imageData.Width;
            var height = this.imageData.Height;
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = (int)(this.imageData.Data[width * y + x] * 255);
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(pixel, pixel, pixel));
                    }
                }
                bitmap.RotateFlip(NextRotateFlipType());

                // BitmapImageをSourceに指定して画面に表示する
                this.displayImage.Source = ConvertBitmapSource(bitmap);
            }
        }

        private RotateFlipType NextRotateFlipType()
        {
            switch (nowRotate)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    nowRotate = RotateFlipType.Rotate90FlipNone;
                    break;
                case RotateFlipType.Rotate90FlipNone:
                    nowRotate = RotateFlipType.Rotate180FlipNone;
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    nowRotate = RotateFlipType.Rotate270FlipNone;
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    nowRotate = RotateFlipType.RotateNoneFlipNone;
                    break;
                default:
                    nowRotate = RotateFlipType.RotateNoneFlipNone;
                    break;
            }
            return nowRotate;
        }

        private BitmapSource ConvertBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource;

            // MemoryStreamを利用した変換処理
            using (var ms = new System.IO.MemoryStream())
            {
                // MemoryStreamに書き出す
                bitmap.Save(ms, ImageFormat.Bmp);

                // MemoryStreamをシーク
                ms.Seek(0, SeekOrigin.Begin);

                // MemoryStreamからBitmapFrameを作成
                bitmapSource =
                    BitmapFrame.Create(
                        ms,
                        System.Windows.Media.Imaging.BitmapCreateOptions.None,
                        System.Windows.Media.Imaging.BitmapCacheOption.OnLoad
                    );
            }
            return bitmapSource;
        }
    }

    class ImageData32
    {
        public float[] Data { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public ImageData32(int width, int height)
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

        public ImageData32(int width, int height, float[] data)
        {
            this.Data = data;
            this.Width = width;
            this.Height = height;
        }

        public ImageData32(ImageData32 data)
        {
            this.Data = new float[data.Width * data.Height];
            this.Width = data.Width;
            this.Height = data.Height;
        }
    }

    class ImageData16
    {
        public ushort[] Data { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public ImageData16(int width, int height)
        {
            this.Data = new ushort[width * height];
            this.Width = width;
            this.Height = height;

            for (int h = 0; h < this.Height; h++)
            {
                for (int w = 0; w < this.Width; w++)
                {
                    this.Data[h * Width + w] = (ushort)(h / this.Height);
                }
            }
        }

        public ImageData16(int width, int height, ushort[] data)
        {
            this.Data = data;
            this.Width = width;
            this.Height = height;
        }

        public ImageData16(ImageData16 data)
        {
            this.Data = new ushort[data.Width * data.Height];
            this.Width = data.Width;
            this.Height = data.Height;
        }
    }
}
