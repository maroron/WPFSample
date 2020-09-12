using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPFSample.SampleB
{
    /// <summary>
    /// HttpReader.xaml の相互作用ロジック
    /// </summary>
    public partial class HttpReader : Window
    {
        public HttpReader()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // RSS ファイルの取得
            int cityCode = Int32.Parse(this.cityCode.Text);
            var results = GetWeatherReportFromYahoo(cityCode);
            var sb = new StringBuilder();

            foreach (var s in results)
            {
                sb.AppendLine(s);
            }
            this.textblock.Text = sb.ToString();
        }

        private IEnumerable<string> GetWeatherReportFromYahoo(int cityCode)
        {
            using(var wc = new WebClient())
            {
                wc.Headers.Add("Content-type", "charaset=UTF-8");
                var uriString = string.Format(
                    @"https://rss-weather.yahoo.co.jp/rss/days/{0}.xml", cityCode);
                var url = new Uri(uriString);
                var stream = wc.OpenRead(url);

                XDocument xdoc = XDocument.Load(stream);
                var nodes = xdoc.Root.Descendants("title");
                foreach (var node in nodes)
                {
                    string s = Regex.Replace(node.Value, "【|】", "");
                    yield return node.Value;
                } 
            }
        }
    }
}
