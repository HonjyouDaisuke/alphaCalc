using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace alphaCalc
{
    static class Operates
    {
        public const int Operate_CA = 0;
        public const int Operate_C = 1;
        public const int Operate_00 = 2;
        public const int Operate_DOT = 3;
        public const int Operate_PM = 4;
        public const int Operate_MULT = 5;
        public const int Operate_PLUS = 6;
        public const int Operate_BACK = 7;
        public const int Operate_PER = 8;
        public const int Operate_MINUS = 9;
        public const int Operate_EQUAL = 10;
        public const int Operate_NONE = -1;
    }

    internal class CalcOperate
    {
        private int _operateCode;
        
        public CalcOperate()
        {
            _operateCode = -1;
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// 演算記号コードを返す
        /// </summary>
        /// <returns></returns>
        public int GetOperate() 
        { 
            return _operateCode; 
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// 演算記号を文字で返す
        /// </summary>
        /// <returns></returns>
        public string GetOperateString()
        {
            string ret = "";
            if (_operateCode == Operates.Operate_PLUS) ret = "＋";
            if (_operateCode == Operates.Operate_MINUS) ret = "－";
            if (_operateCode == Operates.Operate_PER) ret = "÷";
            if (_operateCode == Operates.Operate_MULT) ret = "×";
            if (_operateCode == Operates.Operate_EQUAL) ret = "＝";
            return (ret);
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// 演算記号コードから文字を取得
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetOperateStringFromCode(int c)
        {
            string ret = "";
            if (c == Operates.Operate_PLUS) ret = "＋";
            if (c == Operates.Operate_MINUS) ret = "－";
            if (c == Operates.Operate_PER) ret = "÷";
            if (c == Operates.Operate_MULT) ret = "×";
            if (c == Operates.Operate_EQUAL) ret = "＝";
            return (ret);
        }

        /// <summary>
        /// 演算をセットする
        /// </summary>
        /// <param name="op">演算記号</param>
        /// <returns></returns>
        public bool SetOperate(string op)
        {
            bool result = false;
            _operateCode = Operates.Operate_NONE;

            if (op == "plus") _operateCode = Operates.Operate_PLUS;
            if (op == "minus") _operateCode = Operates.Operate_MINUS;
            if (op == "per") _operateCode = Operates.Operate_PER;
            if (op == "mult") _operateCode = Operates.Operate_MULT;
            if (op == "equal") _operateCode= Operates.Operate_EQUAL;
            if (op == "ca") _operateCode = Operates.Operate_CA;
            if (op == "c") _operateCode = Operates.Operate_C;
            if (op == "plusminus") _operateCode = Operates.Operate_PM;
            if (op == "") _operateCode = Operates.Operate_NONE;

            if (_operateCode >= 0) result = true;

            return result;
        }

        /// <summary>
        /// 2023.07.21 D.Honjyou
        /// クラス複製用のコピー作成
        /// </summary>
        /// <returns>CalcOperateクラス</returns>
        public CalcOperate ShallowCopy()
        {
            return((CalcOperate)MemberwiseClone());
        }
    }

    
}
