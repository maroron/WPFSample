﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WPFSample
{
    /// <summary>
    /// DispatcherObjectTest.xaml の相互作用ロジック
    /// </summary>
    public partial class DispatcherObjectTest : Window
    {
        public DispatcherObjectTest()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッドからの普通の呼び出しなのでOK
            var d = new DrivedObject();
            d.DoSomething();
        }

        private async void NGButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッド以外からの呼び出しなので例外が出る
            var d = new DrivedObject();
            try
            {
                await Task.Run(() => d.DoSomething());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void DispatcherButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッド以外だがDispatcher経由での呼び出しなのでOK
            var d = new DrivedObject();
            await Task.Run(async () =>
                {
                    if (!d.CheckAccess())
                    {
                        await d.Dispatcher.InvokeAsync(() => d.DoSomething()); // OK
                    }
                });
        }
    }

    public class DrivedObject : DispatcherObject
    {
        public void DoSomething()
        {
            // UIスレッドからのアクセスかチェックする
            this.VerifyAccess();
            Debug.WriteLine("DoSomething"); 
        }
    }
}
