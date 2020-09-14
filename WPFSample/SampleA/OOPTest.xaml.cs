﻿using System;
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
using TextFileProcessor;
using WPFSample.SampleA.TemplatePattern;

namespace WPFSample.SampleA
{
    /// <summary>
    /// OOPTest.xaml の相互作用ロジック
    /// </summary>
    public partial class OOPTest : Window
    {
        private OOPTestViewModel vm = new OOPTestViewModel();

        public OOPTest()
        {
            InitializeComponent();
            this.DataContext = vm;

            // Test Key Event
            KeyEventFactory.Entry(this);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TextProcessor.Run<LineCounterProcessor>(this.textbox.Text);
        }

        private void templatePettern_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            KeyEventFactory.SetKeyEvent(button.Name);
        }

        private void Singleton_Click(object sender, RoutedEventArgs e)
        {
            var singleton1 = Singleton.GetInstance;
            var singleton2 = Singleton.GetInstance;

            if (Object.ReferenceEquals(singleton1, singleton2))
            {
                vm.TextProperty += "singleton instance is same." + Environment.NewLine;
            }
            else
            {
                vm.TextProperty += "singleton instance is different." + Environment.NewLine;
            }
        }
    }
}
