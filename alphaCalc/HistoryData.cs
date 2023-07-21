using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alphaCalc
{
    public class HistoryData
    {
        private ArrayList _historyDataList = new ArrayList();

        public ArrayList HistoryDataList { get => _historyDataList; set => _historyDataList = value; }

        public HistoryData() 
        {
            _historyDataList = new ArrayList();
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// 履歴データを追加
        /// </summary>
        /// <param name="data"></param>
        public void AddHistData(string data)
        {
            _historyDataList.Add(data);
        }


    }
}
