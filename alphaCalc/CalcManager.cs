using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace alphaCalc
{
    internal class CalcManager
    {
        CalcOperate _currentOperate;
        CalcOperate _preOperate;
        string _currentFormula = "";
        double _currentResult;
        string _currentNumString = "";
        double _currentNum;
        double _preNum;
        int _preMode = 0;   //mode:0 = number   1 = Operate 
        Operates _prevOperate;
        HistoryData _history = new HistoryData();


        public CalcOperate CurrentOperate { get => _currentOperate; set => _currentOperate = value; }
        public string CurrentFormula { get => _currentFormula; set => _currentFormula = value; }
        public double CurrentResult { get => _currentResult; set => _currentResult = value; }
        public double CurrentNum { get => _currentNum; set => _currentNum = value; }
        public string CurrentNumString { get => _currentNumString; set => _currentNumString = value; }
        internal HistoryData History { get => _history; set => _history = value; }

        public CalcManager() 
        {
            _currentOperate = new CalcOperate();
            _preOperate = new CalcOperate();
            _preOperate.SetOperate("");
            _preNum = 0;
            _currentNumString = "0";
            _currentFormula = "";
            System.Diagnostics.Debug.WriteLine("リセット");
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// 履歴データを登録
        /// </summary>
        public void setNowHist()
        {
            History.AddHistData(_currentFormula);
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// 現在の計算をリセットする
        /// </summary>
        public void ResetCalc()
        {
            History.AddHistData($"{_currentFormula} = {_currentResult}" );
            _currentFormula = "";
            _currentNum = 0;
            _currentNumString = "0";
            _currentResult = 0;
            _preNum = 0;
            _preOperate.SetOperate("");
            _preMode = 0;
            _currentOperate.SetOperate("");
        }

        /// <summary>
        /// 2023.07.17 D.Honjyou
        /// 数字キーが入力された時にデータ管理する
        /// </summary>
        /// <param name="num">入力されたキー</param>
        /// <returns></returns>
        public bool AddNumber(string num)
        {
            bool result = false;
            double d_num;

            if(_preMode == 1)
            {
                _currentNum = 0;
                _currentNumString = "";
                System.Diagnostics.Debug.WriteLine($"式の後の数字です。　前式{_preOperate.GetOperateString()} 前式２{CalcOperate.GetOperateStringFromCode(_prevOperate)}");
                if (_preOperate.OperateCode == Operates.Equal)
                {
                    ResetCalc();
                }

            }

            if (num == "back")
            {
                if (_preMode == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"★back {_currentNumString} --> {_currentNumString.Substring(0, _currentNumString.Length - 1)}");
                    _currentNumString = _currentNumString.Substring(0, _currentNumString.Length - 1);
                    if (double.TryParse(_currentNumString, out d_num))
                    {

                        _currentNum = d_num;
                        _currentNumString = _currentNum.ToString();
                        result = true;
                    }
                }
            }
            else if (num == "dot")
            {
                _currentNumString = _currentNum.ToString();
                _currentNumString += ".";
                _currentNum = double.Parse(_currentNumString);
                result = true;
            }
            else
            {
                if (_currentNumString.Length > 0 && _currentNumString.Substring(_currentNumString.Length - 1, 1) == ".")
                {
                    _currentNumString += num;
                    if (double.TryParse(_currentNumString, out d_num))
                    {

                        _currentNum = d_num;
                        _currentNumString = _currentNum.ToString();
                        result = true;
                    }
                }
                else if (double.TryParse(num, out d_num))
                {
                    _currentNumString = _currentNum.ToString();
                    _currentNumString += num;

                    if (double.TryParse(_currentNumString, out d_num))
                    {

                        _currentNum = d_num;
                        _currentNumString = _currentNum.ToString();
                        result = true;
                    }

                }
            }
            _preMode = 0;
            
            return(result);
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// 四則演算実行処理
        /// </summary>
        /// <param name="ope">演算記号</param>
        /// <param name="n1">これまでの計算結果</param>
        /// <param name="n2">演算する数値</param>
        /// <returns></returns>
        private double RunCalc(Operates ope, double n1, double n2)
        {
            switch (ope)
            {
                case Operates.Plus: // +
                    n1 += n2;
                    break;

                case Operates.Minus: // -
                    n1 -= n2;
                    break;

                case Operates.Mult: // ×
                    n1 *= n2;
                    break;

                case Operates.Per: // ÷
                    n1 /= n2;
                    break;
            }
            return n1;
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// プラスマイナスを追加する
        /// </summary>
        /// <param name="op">演算記号</param>
        /// <returns></returns>
        public bool AddPM(string op)
        {
            bool result = false;

            if(_preMode == 0)
            {
                _currentNum *= -1;
                _currentNumString = _currentNum.ToString();
                result = true;
            }
            else
            {
                _currentResult *= -1;
                _currentFormula += " ×(-1)";
                _currentNumString = _currentResult.ToString();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 演算を追加する
        /// </summary>
        /// <param name="op">演算記号</param>
        /// <returns></returns>
        public bool AddOperate(string op)
        {
            bool result = false;
            double d_num;
            //_preOperate = _currentOperate;
            //_preNum = _currentNum;

            System.Diagnostics.Debug.WriteLine($"<---pOpe:{_preOperate.OperateCode} cOpe:{_currentOperate.OperateCode}");

            _currentOperate.SetOperate(op);
            
            if(_currentOperate.OperateCode == Operates.Ca)
            {
                System.Diagnostics.Debug.WriteLine($"▼pre{_prevOperate}, pre2={_preOperate.OperateCode} cur{_currentOperate.OperateCode}");
                if (_currentFormula.Length > 0 && _preOperate.OperateCode == Operates.Equal)
                {
                    ResetCalc();
                }
                else if (_currentFormula.Length > 0)
                {
                    _currentResult = RunCalc(_preOperate.OperateCode, _currentResult, _currentNum);
                    _currentFormula += $" {_preOperate.GetOperateString()} {_currentNum}";
                    ResetCalc();
                }
                
                return result;
            }
            if (_currentOperate.OperateCode == Operates.C)
            {
                _currentNum = 0;
                _currentNumString = "0";

                //System.Diagnostics.Debug.WriteLine($"????{_currentNum}");
                return result;
            }
            
            //System.Diagnostics.Debug.WriteLine($"<---pOpe:{_preOperate.GetOperate()} cOpe:{_currentOperate.GetOperate()}");
            if (_preMode == 0)
            {
                if (_preOperate.OperateCode == Operates.None)
                {
                    _currentResult = _currentNum;
                    if (double.TryParse(CurrentNumString, out d_num))
                    {
                        _currentResult = d_num;
                    }
                    else
                    {
                        _currentResult = 0;
                    }

                    _currentFormula += $"{_currentResult}";
                    result = true;
                }
                else
                {
                    _currentResult = RunCalc(_preOperate.OperateCode, _currentResult, _currentNum);
                    _currentFormula += $" {_preOperate.GetOperateString()} {_currentNum}";
                    _prevOperate = _preOperate.OperateCode;
                    result = true;
                }
            }
            else
            {
                _preOperate.SetOperate(op);
                if(_currentOperate.OperateCode == Operates.Equal && _preOperate.OperateCode == Operates.Equal)
                {
                    _currentResult = RunCalc(_prevOperate, _currentResult, _preNum);
                    _currentFormula += $" {CalcOperate.GetOperateStringFromCode(_prevOperate)} {_preNum}";
                    result = true;
                }
            }

            if (result)
            {
                _currentNumString = _currentResult.ToString();
                _preOperate = _currentOperate.ShallowCopy();
                _preNum = _currentNum;
                System.Diagnostics.Debug.WriteLine($"--->pOpe:{_preOperate.OperateCode} pNum:{_preNum} cOpe:{_currentOperate.OperateCode} cNum:{_currentNum} cNumS:{_currentNumString}");

            }

            _preMode = 1;
            return (result);
        }
    }
}
