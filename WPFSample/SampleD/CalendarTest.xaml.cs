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

namespace WPFSample.SampleD
{
    /// <summary>
    /// Calendar.xaml の相互作用ロジック
    /// </summary>
    public partial class CalendarTest : Window
    {
        public CalendarTest()
        {
            InitializeComponent();

            // 今日より前は、選択不可能にする。
            this.calendar.BlackoutDates.AddDatesInPast();
            // 翌日から4日間も選択不可能にする
            this.calendar.BlackoutDates.Add(
                new CalendarDateRange(
                    DateTime.Today.AddDays(1),
                    DateTime.Today.AddDays(4)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.calendar.SelectedDate.ToString());
        }
    }
}
