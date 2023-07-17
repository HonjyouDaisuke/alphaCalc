using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace alphaCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalcManager cManage = new CalcManager();

        public MainWindow()
        {
            InitializeComponent();
            
            SwitchTheme("Blue");
            mThemeBlue.IsChecked = true;
            k_back.IsEnabled = false;
        }

        /// <summary>
        /// 番号キーが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string res;
            res = b.Name.Substring(b.Name.IndexOf("_") + 1, b.Name.Length - b.Name.IndexOf("_") - 1);

            cManage.AddNumber(res);
            ResultText.Text = cManage.CurrentNumString;
            FormulaText.Text = cManage.CurrentFormula;
            k_back.IsEnabled = true;
        }

        private void PMClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string res;
            res = b.Name.Substring(b.Name.IndexOf("_") + 1, b.Name.Length - b.Name.IndexOf("_") - 1);

            cManage.AddPM(res);
            ResultText.Text = cManage.CurrentNumString;
            FormulaText.Text = cManage.CurrentFormula;

        }
        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// 計算キーが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpeClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string res;
            res = b.Name.Substring(b.Name.IndexOf("_") + 1, b.Name.Length - b.Name.IndexOf("_") - 1);

            cManage.AddOperate(res);
            FormulaText.Text = cManage.CurrentFormula;
            ResultText.Text = cManage.CurrentResult.ToString();
            k_back.IsEnabled = true;
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// キーボードが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            int test_num;
            string s;
            bool bShift = false;

            System.Diagnostics.Debug.WriteLine($"KeyBoard={e.Key.ToString()}");
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                bShift = true;
            }
            if (!bShift)
            {
                if (int.TryParse(e.Key.ToString().Substring(1, 1), out test_num))
                {
                    cManage.AddNumber(e.Key.ToString().Substring(1, 1));
                    ResultText.Text = cManage.CurrentNumString;
                    FormulaText.Text = cManage.CurrentFormula;
                    k_back.IsEnabled = true;
                }
                if(e.Key == Key.OemPeriod)
                {
                    cManage.AddNumber("dot");
                    ResultText.Text = cManage.CurrentNumString;
                    FormulaText.Text = cManage.CurrentFormula;
                    k_back.IsEnabled = true;
                }
                if(e.Key == Key.OemQuestion)
                {
                    cManage.AddOperate("per");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                    k_back.IsEnabled = false;
                }

                if(e.Key == Key.OemMinus)
                {
                    cManage.AddOperate("minus");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                    k_back.IsEnabled = false;
                }
                if (e.Key == Key.Enter)
                {
                    if (!k_equal.IsMouseOver)
                    {
                        cManage.AddOperate("equal");
                        FormulaText.Text = cManage.CurrentFormula;
                        ResultText.Text = cManage.CurrentResult.ToString();
                        System.Diagnostics.Debug.WriteLine("---equal(enter)---");
                        k_back.IsEnabled = false;
                    }
                    
                }
                if(e.Key == Key.Back || e.Key == Key.Delete)
                {
                    if(k_back.IsEnabled == true)
                    {
                        cManage.AddNumber("back");
                        ResultText.Text = cManage.CurrentResult.ToString();
                        k_back.IsEnabled = true;
                    }
                }
            }
            else
            {
                if (e.Key == Key.OemPlus)
                {
                    cManage.AddOperate("plus");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                    k_back.IsEnabled = false;
                }

                if (e.Key == Key.Oem1)
                {
                    cManage.AddOperate("mult");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                    k_back.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマの変更
        /// </summary>
        /// <param name="themeName">テーマファイルの名称</param>
        private void SwitchTheme(string themeName)
        {
            var dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@$"Resources/Styles/ControlStyle{themeName}.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(dictionary);

        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマメニューのチェックをすべて外す
        /// </summary>
        private void ThemeCheckOff()
        {
            mThemeBlue.IsChecked = false;
            mThemeBlack.IsChecked = false;
            mThemeGreen.IsChecked = false;
            mThemeOrange.IsChecked = false;
            mThemePink.IsChecked = false;
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマをブルーにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Theme_Blue(object sender, RoutedEventArgs e)
        {
            ThemeCheckOff();
            mThemeBlue.IsChecked = true;
            SwitchTheme("Blue");
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマを黒にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Theme_Black(object sender, RoutedEventArgs e)
        {
            ThemeCheckOff();
            mThemeBlack.IsChecked = true;
            SwitchTheme("");

            //((App)Application.Current).ChangeTheme("");
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマをピンクにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Theme_Pink(object sender, RoutedEventArgs e)
        {
            ThemeCheckOff();
            mThemePink.IsChecked = true;
            SwitchTheme("Pink");

            //((App)Application.Current).ChangeTheme("");
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマをグリーンにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Theme_Green(object sender, RoutedEventArgs e)
        {
            ThemeCheckOff();
            mThemeGreen.IsChecked = true;
            SwitchTheme("Green");

            //((App)Application.Current).ChangeTheme("");
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// テーマをオレンジにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Theme_Orange(object sender, RoutedEventArgs e)
        {
            ThemeCheckOff();
            mThemeOrange.IsChecked = true;
            SwitchTheme("Orange");

            //((App)Application.Current).ChangeTheme("");
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// 履歴画面を立ち上げる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hist(object sender, RoutedEventArgs e)
        {
            History histWindow = new History();
            cManage.setNowHist();
            histWindow.setHistory(cManage.History);
            histWindow.Show();
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// メニューのコピーキーが押された時
        /// クリップボードに現在の回答欄をコピーする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyResultClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultText.Text);
        }
    }
}
