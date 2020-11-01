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

namespace WPFSample
{
    /// <summary>
    /// TabControlSample.xaml の相互作用ロジック
    /// </summary>
    public partial class TabControlSample : Window
    {
        TabControlViewModel vm = new TabControlViewModel();

        public TabControlSample()
        {
            InitializeComponent();

            this.DataContext = vm;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
