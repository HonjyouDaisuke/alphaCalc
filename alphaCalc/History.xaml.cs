using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace alphaCalc
{
    /// <summary>
    /// History.xaml の相互作用ロジック
    /// </summary>
    public partial class History : Window
    {
        HistoryData data = new HistoryData();
        public History(string themeName)
        {
            InitializeComponent();
            var dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@$"Resources/Styles/ControlStyle{themeName}.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(dictionary);
        }

        /// <summary>
        /// 2023.07.20 D.Honjyou
        /// 履歴データをセット
        /// </summary>
        /// <param name="historyData"></param>
        public void setHistory(HistoryData historyData)
        {
            data = historyData; 

            foreach(string d in data.HistoryDataList)
            {
                HistoryListView.Items.Add(d);
            }
        }

        /// <summary>
        /// 2023.07.20 D.Honjyou
        /// 履歴をクリップボードに保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_CopyClick(object sender, RoutedEventArgs e)
        {
            string Clip = "";

            foreach(string d in data.HistoryDataList)
            {
                Clip += d + "\n";
            }
            Clipboard.SetText(Clip);
        }

        /// <summary>
        /// 2023.07.20 D.Honjyou
        /// ファイルに保存メニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_SaveClick(object sender, RoutedEventArgs e)
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                //　using文を使ってファイルをクローズするコード

                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    foreach (string hist in data.HistoryDataList)
                    {
                        // ファイルに書き込む
                        writer.WriteLine(hist + "\n");
                    }
                }// 完了をメッセージボックスに表示
                MessageBox.Show("保存しました。", "alphaCalc", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
