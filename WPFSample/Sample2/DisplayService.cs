using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFSample.Sample2
{
    public static class DisplayService
    {
        private static Object lockobject = new object();

        private static Canvas canvas;

        private static List<Path> paths = new List<Path>();

        private static DispatcherTimer timer;

        public static void Entry(Canvas canvas)
        {
            DisplayService.canvas = canvas;

            // DispatcherTimerクラスでは、タイマメソッドは必ずUIスレッドで呼び出される。
            // タイマメソッドの中から安心してUIにアクセスできる。
            // その代わり、タイマメソッドで時間のかかる処理を単純に実行するとUIがフリーズしてしまう
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += new EventHandler(TimerMethod);
            timer.Start();
        }

        public static void End()
        {
            timer.Stop();
        }

        private static void TimerMethod(object sender, EventArgs e)
        {
            lock (lockobject)
            {
                foreach (var path in paths)
                {
                    // Canvasの子に同じポリゴンを設定しようとするとエラーが発生するので、チェックする
                    if (!canvas.Children.Contains(path))
                    {
                        DisplayService.canvas.Children.Add(path);
                    }
                }
            }
        }

        public static void Add(Path path)
        {
            lock (lockobject)
            {
                DisplayService.paths.Add(path);
            }
        }
    }
}
