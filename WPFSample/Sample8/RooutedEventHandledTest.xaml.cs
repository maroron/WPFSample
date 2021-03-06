﻿using System;
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

namespace WPFSample.Sample8
{
    /// <summary>
    /// RooutedEventHandledTest.xaml の相互作用ロジック
    /// </summary>
    public partial class RooutedEventHandledTest : Window
    {
        public RooutedEventHandledTest()
        {
            InitializeComponent();
            var parent = new RoutedEventPerson { Name = "parent" };
            var child = new RoutedEventPerson { Name = "child" };
            parent.AddChild(child);
            parent.ToAge += (object s, RoutedEventArgs e) =>
            {
                Console.WriteLine(((RoutedEventPerson)e.Source).Name);
            };
            parent.RaiseEvent(new RoutedEventArgs(RoutedEventPerson.ToAgeEvent));
            child.RaiseEvent(new RoutedEventArgs(RoutedEventPerson.ToAgeEvent));

            this.stackPanel.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.StackPanel_Click));
        }

        #region Right mouse down
        private void Button_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Button_PreviewMouseRightButtonDown");
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Button_MouseRightButtonDown");
        }

        private void StackPanel_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StackPanel_PreviewMouseRightButtonDown");
        }

        private void StackPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StackPanel_MouseRightButtonDown");
        }

        private void DockPanel_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("DockPanel_PreviewMouseRightButtonDown");
        }

        private void DockPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("DockPanel_MouseRightButtonDown");
        }
        #endregion

        #region Left mouse down
        private void DockPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("DockPanel_PreviewMouseLeftButtonDown");
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StackPanel_PreviewMouseLeftButtonDown");
        }


        // コントロールの既定の動作は常にイベントのバブルバージョンで実装する必要がある。
        // アプリケーション開発者がプレビューイベントを使用してロジックにフックしたり、
        // コントロール既定の動作をキャンセルしたりすることが可能になる。

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Button_PreviewMouseLeftButtonDown");
            Console.WriteLine("Handled = true");
            e.Handled = true;
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Button_MouseLeftButtonDown");
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("StackPanel_MouseLeftButtonDown");
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("DockPanel_MouseLeftButtonDown");
        }
        #endregion

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("StackPanel_ClickEvent");
        }
    }

    class Person : FrameworkElement
    {
        // イベント名Eventの命名規約のstaticフィールドに格納する
        public static RoutedEvent ToAgeEvent = EventManager.RegisterRoutedEvent(
            "ToAge", // イベント名
            RoutingStrategy.Tunnel, // イベントタイプ
            typeof(RoutedEventHandler), // イベントハンドラの型
            typeof(Person)); // イベントのオーナー

        // CLRのイベントのラッパー
        public event RoutedEventHandler ToAge 
        {
            add { this.AddHandler(ToAgeEvent, value); }
            remove { this.RemoveHandler(ToAgeEvent, value); }
        }

        // 子を追加するメソッド
        public void AddChild(Person child)
        {
            this.AddLogicalChild(child);
        }
    }
}
