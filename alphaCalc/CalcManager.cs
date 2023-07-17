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
        int _prevOperate;
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

        public void setNowHist()
        {
            History.AddHistData(_currentFormula);
        }

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


        public bool AddNumber(string num)
        {
            bool result = false;
            double d_num;

            if(_preMode == 1)
            {
                _currentNum = 0;
                _currentNumString = "";
                System.Diagnostics.Debug.WriteLine($"式の後の数字です。　前式{_preOperate.GetOperateString()} 前式２{_preOperate.GetOperateStringFromCode(_prevOperate)}");
                if (_preOperate.GetOperate() == Operates.Operate_EQUAL)
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

        private double RunCalc(int ope, double n1, double n2)
        {
            switch (ope)
            {
                case Operates.Operate_PLUS: // +
                    n1 += n2;
                    break;

                case Operates.Operate_MINUS: // -
                    n1 -= n2;
                    break;

                case Operates.Operate_MULT: // ×
                    n1 *= n2;
                    break;

                case Operates.Operate_PER: // ÷
                    n1 /= n2;
                    break;
            }
            return n1;
        }

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
        public bool AddOperate(string op)
        {
            bool result = false;
            double d_num;
            //_preOperate = _currentOperate;
            //_preNum = _currentNum;

            System.Diagnostics.Debug.WriteLine($"<---pOpe:{_preOperate.GetOperate()} cOpe:{_currentOperate.GetOperate()}");

            _currentOperate.SetOperate(op);
            System.Diagnostics.Debug.WriteLine($"★{op}, {_currentOperate.GetOperate()}");
            if(_currentOperate.GetOperate() == Operates.Operate_CA)
            {
                _currentFormula = "";
                _currentNum = 0;
                _currentNumString = "0";
                _currentResult = 0;
                _preNum = 0;
                _preOperate.SetOperate("");
                _preMode = 0;
                _currentOperate.SetOperate("");
                return result;
            }
            if (_currentOperate.GetOperate() == Operates.Operate_C)
            {
                _currentNum = 0;
                _currentNumString = "0";

                System.Diagnostics.Debug.WriteLine($"????{_currentNum}");
                return result;
            }
            /**
            if (_currentOperate.GetOperate() == Operates.Operate_PM)
            {
                _currentResult *= -1;
                _currentFormula += "×(-1)";
                System.Diagnostics.Debug.WriteLine($"????{_currentNum}");
                return result;
            }
            **/
            System.Diagnostics.Debug.WriteLine($"<---pOpe:{_preOperate.GetOperate()} cOpe:{_currentOperate.GetOperate()}");
            if (_preMode == 0)
            {
                if (_preOperate.GetOperate() == Operates.Operate_NONE)
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
                    _currentResult = RunCalc(_preOperate.GetOperate(), _currentResult, _currentNum);
                    _currentFormula += $" {_preOperate.GetOperateString()} {_currentNum}";
                    _prevOperate = _preOperate.GetOperate();
                    result = true;
                }
            }
            else
            {
                _preOperate.SetOperate(op);
                if(_currentOperate.GetOperate() == Operates.Operate_EQUAL && _preOperate.GetOperate() == Operates.Operate_EQUAL)
                {
                    _currentResult = RunCalc(_prevOperate, _currentResult, _preNum);
                    _currentFormula += $" {_currentOperate.GetOperateStringFromCode(_prevOperate)} {_preNum}";
                    result = true;
                }
            }

            if (result)
            {
                _currentNumString = _currentResult.ToString();
                _preOperate = _currentOperate.ShallowCopy();
                _preNum = _currentNum;
                System.Diagnostics.Debug.WriteLine($"--->pOpe:{_preOperate.GetOperate()} pNum:{_preNum} cOpe:{_currentOperate.GetOperate()} cNum:{_currentNum} cNumS:{_currentNumString}");

            }

            _preMode = 1;
            return (result);
        }
    }
}
