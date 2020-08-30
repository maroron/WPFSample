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

namespace WPFSample.Sample5
{
    /// <summary>
    /// WindowLifeTime.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowLifeTime : Window
    {
        // 実行結果
        // Constructor start
        // Initialized
        // Constructor end
        // Activated
        // Loaded
        // ContentRendered
        // Closing
        // Deactivated
        // Closed
        // Unloaded

        // 最も使用されるイベントは、
        // Loaded
        // Closing
        // Closed
        // の３つ

        public WindowLifeTime()
        {
            Console.WriteLine("Constructor start" );
            InitializeComponent();
            Console.WriteLine("Constructor end" );
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Console.WriteLine("Initialized" );
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // Activate はユーザーがシステムで実行されているウィンドウをActivateするたびに発生する
            Console.WriteLine("Activated");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ユーザーへの表示が行われる直前に何らかの処理を行う場合はLoadedを使用する
            Console.WriteLine("Loaded");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // ウィンドウが最初に完全にレンダリングされたときに発生する
            // ユーザーへの表示が行われた直後に何らかの処理を行う場合はContentRenderedを使用する
            Console.WriteLine("ContentRendered");
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            // Deactivate はユーザーがシステムで実行されているウィンドウをDeactivateするたびに発生する
            Console.WriteLine("Deactivated");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // ウィンドウが閉じる前に発生する
            // このイベントはキャンセルできるので、ウィンドウが閉じるのを防ぐことができる

            // ウィンドウが閉じるのを防ぐ
            // e.Cancel = true;
            Console.WriteLine("Closing");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // ウィンドウが実際に閉じる(ユーザーに表示されなくなる)ときに発生する
            // もしClosedイベントに書いた処理が失敗したとき(ファイルの保存など)、
            // ユーザーはキャンセルすることができないので、作業の保存などの処理はClosingで行うべき
            Console.WriteLine("Closed");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Unloaded");
        }
    }
}
