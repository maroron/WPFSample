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

            var pValueCheck = new PersonObject();
            pValueCheck.Age = 10;
            pValueCheck.Age = -10;
            pValueCheck.Age = 150;
            try
            {
                pValueCheck.Age = int.MaxValue;
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e);
            }
            var attachedPerson = new PersonObject();
            attachedPerson.SetValue(SamplePerson.BirthdayProperty, DateTime.Now);
            Console.WriteLine(attachedPerson.GetValue(SamplePerson.BirthdayProperty));

            var attachedPerson2 = new PersonObject();
            SamplePerson.SetBirthday(attachedPerson2, DateTime.Now);
            Console.WriteLine(SamplePerson.GetBirthday(attachedPerson2));
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
                "Name",                                     // プロパティ名
                typeof(string),                             // プロパティの型
                typeof(PersonObject),                       // プロパティを所有する型
                new PropertyMetadata("defalut name",        // メタデータ　ここではデフォルト値を設定
                                     NamePropertyChanged)); // メタデータ　プロパティの変更時に呼ばれるコールバック

        private static void NamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("Nameプロパティが{0}から{1}に変わりました", e.OldValue, e.NewValue);
        }

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

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register(
                "Age",                                      // プロパティ名
                typeof(int),                                // プロパティの型
                typeof(PersonObject),                       // プロパティを所有する型
                new PropertyMetadata(0,                     // メタデータ　ここではデフォルト値を設定
                                     AgePropertyChanged,    // メタデータ　プロパティの変更時に呼ばれるコールバック
                                     CoerceAgeValue         // メタデータ　データのバリデーション
                    ),
                ValidateAgeValue
                ); 

        private static void AgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("Ageプロパティが{0}から{1}に変わりました。", e.OldValue, e.NewValue);
        }

        private static object CoerceAgeValue(DependencyObject d, object baseValue)
        {
            // 年齢は0-120の間
            var value = (int)baseValue;
            if (value < 0)
            {
                return 0;
            }
            if (value > 120)
            {
                return 120;
            }
            return value;
        }

        private static bool ValidateAgeValue(object value)
        {
            int age = (int)value;
            return age != int.MaxValue && age != int.MinValue;
        }

        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        // 読込専用のDependencyProperty
        private static readonly DependencyPropertyKey BirthdayPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Birthday",
                typeof(DateTime),
                typeof(PersonObject),
                new PropertyMetadata(DateTime.Now));

        // DependencyPropertyは、DependencyPropertyKeyから取得する
        public static readonly DependencyProperty BirthdayProperty = BirthdayPropertyKey.DependencyProperty;

        public DateTime Birthday
        {
            // getは従来通り
            get { return (DateTime)GetValue(BirthdayProperty); }

            // setはDependencyPropertyKeyを使って行う
            private set { SetValue(BirthdayPropertyKey, value); }
        }


        public static readonly DependencyProperty FirstNameProperty = 
            DependencyProperty.Register("FirstName", typeof(string),
                typeof(PersonObject), new FrameworkPropertyMetadata(null));

        public string FirstName 
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(
                "LastName",
                typeof(string),
                typeof(PersonObject),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.Inherits)); // 子要素へ継承するプロパティ

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }
    }

    public static class SamplePerson
    {
        public static readonly DependencyProperty BirthdayProperty =
            DependencyProperty.RegisterAttached(
                "Birthday",
                typeof(DateTime),
                typeof(SamplePerson),
                new PropertyMetadata(DateTime.MinValue));

        // プログラムからアクセスするための添付プロパティのラッパー
        public static DateTime GetBirthday(DependencyObject obj)
        {
            return (DateTime)obj.GetValue(BirthdayProperty);
        }

        public static void SetBirthday(DependencyObject obj, DateTime value)
        {
            obj.SetValue(BirthdayProperty, value);
        }
    }
}
