﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// MainWindow2.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private Dictionary<string, Window> sampleList = new Dictionary<string, Window>
        {
            { "Sample1", new TabControlSample() },
            { "Sample2", new FileDialogTest() },
            { "Sample3", new DisplayInfoControlTest() },
            { "Sample4", new PopupTest() },
            { "Sample5", new TooltipSample() },
            { "Sample6", new MediaSample() },
            { "Sample7", new DispatcherObjectTest() },
        };

        public MainWindow2()
        {
            InitializeComponent();
        }

        private void Sample_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;

            var isExist = sampleList.Keys.Contains(clickedButton.Name);
            if (isExist)
            {
                var window = sampleList[clickedButton.Name];

                // WPFでは、子ウィンドウでClosedイベントが走るとインスタンスが破棄される。
                // 保持しているインスタンスを使いまわす場合は、
                // ClosingイベントでCancel = trueとしClosedイベントを呼ばないようにした上で
                // VisibilityでCollapsedにする(Hiddenでも可)
                window.Closing += ChildWindow_Closing;
                window.ShowDialog();
            }
        }

        private void ChildWindow_Closing(object sender, CancelEventArgs e)
        {
            var w = sender as Window;
            e.Cancel = true;
            w.Visibility = Visibility.Collapsed;
            w.Closing -= ChildWindow_Closing; // 二重登録防止のため、呼び出し後にイベントを解除する
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            foreach (var w in sampleList.Values)
            {
                w.Close();
            }
        }
    }
}
