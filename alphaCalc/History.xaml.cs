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

namespace alphaCalc
{
    /// <summary>
    /// History.xaml の相互作用ロジック
    /// </summary>
    public partial class History : Window
    {
        HistoryData data;
        public History()
        {
            InitializeComponent();
        }
        
        public void setHistory(HistoryData historyData)
        {
            data = historyData; 

            foreach(string d in data.HistoryDataList)
            {
                HistoryListView.Items.Add(d);
            }
        }

        private void MenuItem_CopyClick(object sender, RoutedEventArgs e)
        {
            string Clip = "";

            foreach(string d in data.HistoryDataList)
            {
                Clip += d + "\n";
            }
            Clipboard.SetText(Clip);
        }

        private void MenuItem_SaveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
