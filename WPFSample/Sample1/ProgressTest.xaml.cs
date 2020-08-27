using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFSample.Sample1
{
    /// <summary>
    /// ProgressTest.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgressTest : Window
    {
        private BackgroundWorker worker = new BackgroundWorker();

        public ProgressTest()
        {
            InitializeComponent();

            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            // Progressあり設定　デフォルトはfalse
            worker.WorkerReportsProgress = true;

            // Cancelあり設定　デフォルトはfalse
            worker.WorkerSupportsCancellation = true;

            SetProgressState(true);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progress.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetProgressState(true);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var collection = Enumerable.Range(1, 200).ToArray();
            int count = 1;

            foreach (var num in collection)
            {
                if (worker.CancellationPending)
                {
                    break;
                }

                // DoWork(num)
                var per = count * 100 / collection.Length;
                worker.ReportProgress(per, null);
                count++;
                Thread.Sleep(10);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            SetProgressState(false);
            worker.RunWorkerAsync();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        private void SetProgressState(bool isEnable)
        {
            this.Start.IsEnabled = isEnable;
            this.Stop.IsEnabled = !isEnable;

            this.Start.Opacity = isEnable ? 1.0 : 0.4;
            this.Stop.Opacity = !isEnable ? 1.0 : 0.4;

            this.progressText.Text = !isEnable ? "Start" : "Stop";
        }
    }
}
