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

namespace WPFSample.Sample3
{
    /// <summary>
    /// HowToUseList.xaml の相互作用ロジック
    /// </summary>
    public partial class HowToUseList : Window
    {
        private List<string> list = new List<string>
        {
            "Tokyo",
            "New Delhi",
            "Bangkok",
            "London",
            "Paris",
            "Berlin",
            "Canberra",
            "Hong Kong",
        };

        public HowToUseList()
        {
            InitializeComponent();

            inputData.Text = CreateDispString(list);
        }

        private void ClearText()
        {
            resultData.Text = "";
        }

        private string CreateDispString(List<string> dispstrings)
        {
            var builder = new StringBuilder();
            foreach (var item in dispstrings)
            {
                builder.AppendLine(item);
            }
            return builder.ToString();
        }

        private void Exists_Click(object sender, RoutedEventArgs e)
        {
            ClearText();

            var exists = list.Exists(s => s[0] == 'A');
            resultData.Text = "Exists(s => s[0] == 'A') -> " + exists;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            ClearText();

            var name = list.Find(s => s.Length == 6);
            resultData.Text = "Find(s => s.Length == 6) -> " + name;
        }

        private void FindIndex_Click(object sender, RoutedEventArgs e)
        {
            ClearText();

            var index = list.FindIndex(s => s == "Berlin");
            resultData.Text = "FindIndex(s => s == \"Berlin\") -> " + index;
        }

        private void FindAll_Click(object sender, RoutedEventArgs e)
        {
            ClearText();

            var names = list.FindAll(s => s.Length <= 5);
            resultData.Text = "FindAll(s => s.Length <= 5) -> \n\n";
            resultData.Text += CreateDispString(names);
        }

        private void RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            ClearText();

            // 元のデータは残したいので別のインスタンスを作る
            var removelist = new List<string>(list);
            var removedConunt = removelist.RemoveAll(s => s.Contains("on"));
            resultData.Text = "RemoveAll(s => s.Contains(\"on\")) -> " + removedConunt;
        }

        private void ConvertAll_Click(object sender, RoutedEventArgs e)
        {
            ClearText();

            var lowerList = list.ConvertAll(s => s.ToLower());
            resultData.Text = "ConertAll s => s.ToLower() -> \n\n";
            resultData.Text += CreateDispString(lowerList);
        }
    }
}
