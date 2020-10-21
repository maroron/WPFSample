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

namespace WPFSample.SampleF
{
    /// <summary>
    /// SelectControlTest.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectControlTest : Window
    {
        public SelectControlTest()
        {
            InitializeComponent();

            var items = Enumerable.Range(1, 10).
                Select(i => new Person { Name = "tanaka" + i, Age = 20 + i }).ToList();
            this.comboBox.ItemsSource = items;
            this.comboBoxEditable.ItemsSource = items;
            this.listBox.ItemsSource = items;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.textblock.Text = "ON";
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.textblock.Text = "OFF";
        }

        private void CheckBox_Indeterminate(object sender, RoutedEventArgs e)
        {
            this.textblock.Text = "Other";
        }
    }
}
