using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFSample.Sample9
{
    /// <summary>
    /// DataGridTest.xaml の相互作用ロジック
    /// </summary>
    public partial class DataGridTest : Window
    {
        public DataGridTest()
        {
            InitializeComponent();

            // Left Side DataGrid
            var data = new ObservableCollection<Person>(
                Enumerable.Range(1, 100).Select(i => new Person
                {
                    Name = "田中　太郎" + i,
                    Gender = i % 2 == 0 ? Gender.Men : Gender.Women,
                    Age = 20 + i % 50,
                    AuthMember = i % 5 == 0,
                }));
            this.dataGrid.ItemsSource = data;

            // Right Side DataGrid
            var data2 = new ObservableCollection<Person>(
                Enumerable.Range(1, 100).Select(i => new Person
                {
                    Name = "田中　太郎" + i,
                    Gender = i % 2 == 0 ? Gender.Men : Gender.Women,
                    AuthMember = i % 5 == 0,
                }));
            this.dataGrid2.ItemsSource = data2;
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // プロパティ名から自動生成する列をカスタマイズする
            switch (e.PropertyName)
            {
                case "Name":
                    e.Column.Header = "名前";
                    e.Column.DisplayIndex = 0;
                    break;

                case "Age":
                    e.Column.Header = "年齢";
                    e.Column.DisplayIndex = 1;
                    break;

                case "Gender":
                    // Gender プロパティは表示しない
                    e.Cancel = true;
                    break;

                case "AuthMember":
                    e.Column.Header = "承認済み";
                    e.Column.DisplayIndex = 2;
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
