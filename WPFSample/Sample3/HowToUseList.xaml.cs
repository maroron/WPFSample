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

            var builder = new StringBuilder();
            foreach (var item in list)
            {
                builder.AppendLine(item);
            }
            inputData.Text = builder.ToString();
        }
    }
}
