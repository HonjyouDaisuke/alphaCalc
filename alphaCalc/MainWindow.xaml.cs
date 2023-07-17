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
           // InitializeComponent();

            /**
            var dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@"Resources/Styles/ControlStyle.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dictionary);

            dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@"Resources/Styles/ControlStyleBlue.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dictionary);


            // WPFテーマをリソース・ディクショナリのソースに指定
            string themeUri = String.Format(
              "pack://application:,,,/Themes/{0}.xaml", themeName);
            dict.Source = new Uri(themeUri);
            **/
            InitializeComponent();
            //ComboBoxItem item = (ComboBoxItem)comboBox1.SelectedItem;
            //if (item == null) return;
            //string themeName = item.Content.ToString().Replace(" ", "");
            // ChangeThemeメソッドを呼び出す
            var dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@"Resources/Styles/ControlStyleBlue.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dictionary);

            InitializeComponent();
        }

        private void NumClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string res;
            res = b.Name.Substring(b.Name.IndexOf("_") + 1, b.Name.Length - b.Name.IndexOf("_") - 1);

            cManage.AddNumber(res);
            ResultText.Text = cManage.CurrentNumString;
            
        }

        private void OpeClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string res;
            res = b.Name.Substring(b.Name.IndexOf("_") + 1, b.Name.Length - b.Name.IndexOf("_") - 1);

            cManage.AddOperate(res);
            FormulaText.Text = cManage.CurrentFormula;
            ResultText.Text = cManage.CurrentResult.ToString();

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            int test_num;
            string s;
            bool bShift = false;

            
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
                }

                if(e.Key == Key.OemQuestion)
                {
                    cManage.AddOperate("per");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                }

                if(e.Key == Key.OemMinus)
                {
                    cManage.AddOperate("minus");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                }
                if (e.Key == Key.Enter)
                {
                    if (!k_equal.IsMouseOver)
                    {
                        cManage.AddOperate("equal");
                        FormulaText.Text = cManage.CurrentFormula;
                        ResultText.Text = cManage.CurrentResult.ToString();
                        System.Diagnostics.Debug.WriteLine("---equal(enter)---");
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
                }

                if (e.Key == Key.Oem1)
                {
                    cManage.AddOperate("mult");
                    FormulaText.Text = cManage.CurrentFormula;
                    ResultText.Text = cManage.CurrentResult.ToString();
                }
            }
        }

        private void SwitchTheme(string themeName)
        {
            var dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@$"Resources/Styles/ControlStyle{themeName}.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(dictionary);

        }

        private void Blue(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Blue");
        }

        
        private void Black(object sender, RoutedEventArgs e)
        {
            SwitchTheme("");

            //((App)Application.Current).ChangeTheme("");
        }

        private void Pink(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Pink");

            //((App)Application.Current).ChangeTheme("");
        }
        private void Green(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Green");

            //((App)Application.Current).ChangeTheme("");
        }
        private void Orange(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Orange");

            //((App)Application.Current).ChangeTheme("");
        }

    }
}
