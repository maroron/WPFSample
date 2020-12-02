using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WPFSample
{
    /// <summary>
    /// DispatcherObjectTest.xaml の相互作用ロジック
    /// </summary>
    public partial class DispatcherObjectTest : Window
    {
        public DispatcherObjectTest()
        {
            InitializeComponent();

            var p = new PersonObject();

            // 冗長な書き方
            Console.WriteLine(p.GetValue(PersonObject.NameProperty));
            p.SetValue(PersonObject.NameProperty, "personA");
            Console.WriteLine(p.GetValue(PersonObject.NameProperty));

            // C#らしいプロパティの使用方法にカスタマイズしたやり方
            Console.WriteLine(p.Name);
            p.Name = "personB";
            Console.WriteLine(p.Name);

            // メタデータが共有されることの確認
            var p1 = new PersonObject();
            var p2 = new PersonObject();
            p1.Children.Add(new PersonObject());
            p2.Children.Add(new PersonObject());

            Console.WriteLine("p1.Children.Count = {0}", p1.Children.Count);
            Console.WriteLine("p2.Children.Count = {0}", p2.Children.Count);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッドからの普通の呼び出しなのでOK
            var d = new DrivedObject();
            d.DoSomething();
        }

        private async void NGButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッド以外からの呼び出しなので例外が出る
            var d = new DrivedObject();
            try
            {
                await Task.Run(() => d.DoSomething());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void DispatcherButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッド以外だがDispatcher経由での呼び出しなのでOK
            var d = new DrivedObject();
            await Task.Run(async () =>
                {
                    if (!d.CheckAccess())
                    {
                        await d.Dispatcher.InvokeAsync(() => d.DoSomething()); // OK
                    }
                });
        }
    }

    public class DrivedObject : DispatcherObject
    {
        public void DoSomething()
        {
            // UIスレッドからのアクセスかチェックする
            this.VerifyAccess();
            Debug.WriteLine("DoSomething"); 
        }
    }

    public class PersonObject : DependencyObject
    {
        public PersonObject()
        {
            // デフォルト値をコンストラクタで指定するようにする
            // こうすることでメタデータが共有されることを防げる
            this.Children = new List<PersonObject>();
        }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(
                "Name",                                 // プロパティ名
                typeof(string),                         // プロパティの型
                typeof(PersonObject),                   // プロパティを所有する型
                new PropertyMetadata("defalut name"));  // メタデータ　ここではデフォルト値を設定

        // 依存関係プロパティのCLRのプロパティのラッパー
        public string Name 
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register("Children", typeof(List<PersonObject>),
                typeof(PersonObject), new PropertyMetadata(new List<PersonObject>()));

        public List<PersonObject> Children 
        {
            get { return (List<PersonObject>)GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }
    }
}
