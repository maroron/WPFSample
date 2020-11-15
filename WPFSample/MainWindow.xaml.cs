using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFSample.Sample1;
using WPFSample.Sample2;
using WPFSample.Sample3;
using WPFSample.Sample4;
using WPFSample.Sample5;
using WPFSample.Sample6;
using WPFSample.Sample7;
using WPFSample.Sample8;
using WPFSample.Sample9;
using WPFSample.SampleA;
using WPFSample.SampleB;
using WPFSample.SampleC;
using WPFSample.SampleD;
using WPFSample.SampleE;
using WPFSample.SampleF;

namespace WPFSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Window> 
            sampleList = new Dictionary<string, Window>
            {
                { "Sample1", new ProgressTest() },
                { "Sample2", new CanvasTest() },
                { "Sample3", new HowToUseList() },
                { "Sample4", new DialogTest() },
                { "Sample5", new WindowLifeTime() },
                { "Sample6", new ControlTest() },
                { "Sample7", new DataBindingTest() },
                { "Sample8", new RooutedEventHandledTest() },
                { "Sample9", new DataGridTest() },
                { "Sample10", new OOPTest() },
                { "Sample11", new HttpReader() },
                { "Sample12", new TreeViewTest() },
                { "Sample13", new CalendarTest() },
                { "Sample14", new MenuTest() },
                { "Sample15", new SelectControlTest() },
                { "Sample16", new MainWindow2() },
            };

        public MainWindow()
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
                window.Closing += (s, ev) =>
                {
                    ev.Cancel = true;
                    window.Visibility = Visibility.Collapsed;
                };
                window.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var w in sampleList.Values)
            {
                w.Close();
            }
        }
    }
}
