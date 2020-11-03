using Microsoft.Win32;
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
    /// FileDialogTest.xaml の相互作用ロジック
    /// </summary>
    public partial class FileDialogTest : Window
    {
        // 1. SaveFileDialog ファイルを保存するときに使用するダイアログ
        // 2. OpenFileDialog ファイルを開くときに使用するダイアログ
        //
        // これらのダイアログは、主に以下のような流れで使用する。
        // - インスタンスを生成する。 
        // - Titleプロパティと、Filterプロパティを設定する。
        // - ShowDialogを呼び出してダイアログを表示する。
        // - ダイアログの戻り値を確認してOKが押されている場合は、FileNameプロパティかFileNamesプロパティを使用して選択されたファイルを取得する。
        public FileDialogTest()
        {
            InitializeComponent();
        }

        private void FileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "ファイルを開く";
            dialog.Filter = "全てのファイル(*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                this.textBlockFileName.Text = dialog.FileName;
            }
            else
            {
                this.textBlockFileName.Text = "キャンセルされました";
            }

        }

        private void FileSaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = "ファイルを保存";
            dialog.Filter = "テキストファイル|*.txt";
            if (dialog.ShowDialog() == true)
            {
                this.textBlockFileName.Text = dialog.FileName;
            }
            else
            {
                this.textBlockFileName.Text = "キャンセルされました";
            }
        }
    }
}
