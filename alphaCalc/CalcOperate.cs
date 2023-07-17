using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

/***
< Button x: Name = "k_ca" Content = "CA" Grid.Row = "0" Grid.Column = "0" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_c" Content = "C" Grid.Row = "0" Grid.Column = "1" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_7" Content = "7" Grid.Row = "1" Grid.Column = "0" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_8" Content = "8" Grid.Row = "1" Grid.Column = "1" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_9" Content = "9" Grid.Row = "1" Grid.Column = "2" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_4" Content = "4" Grid.Row = "2" Grid.Column = "0" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_5" Content = "5" Grid.Row = "2" Grid.Column = "1" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_6" Content = "6" Grid.Row = "2" Grid.Column = "2" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_1" Content = "1" Grid.Row = "3" Grid.Column = "0" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_2" Content = "2" Grid.Row = "3" Grid.Column = "1" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_3" Content = "3" Grid.Row = "3" Grid.Column = "2" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_0" Content = "0" Grid.Row = "4" Grid.Column = "0" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_00" Content = "00" Grid.Row = "4" Grid.Column = "1" Margin = "3,3" Style = "{StaticResource CalcButton}" Click = "NumClick" />
            < Button x: Name = "k_dot" Content = "." Grid.Row = "4" Grid.Column = "2" Margin = "3,3" Style = "{StaticResource CalcButton}" />
            < Button x: Name = "k_percent" Content = "％" Grid.Row = "1" Grid.Column = "4" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_mult" Content = "×" Grid.Row = "2" Grid.Column = "4" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_plas" Content = "＋" Grid.Row = "3" Grid.Column = "4" Grid.RowSpan = "2" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_back" Content = "←" Grid.Row = "1" Grid.Column = "5" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_per" Content = "÷" Grid.Row = "2" Grid.Column = "5" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_minus" Content = "－" Grid.Row = "3" Grid.Column = "5" Margin = "3,3" Style = "{StaticResource OperateButton}" />
            < Button x: Name = "k_equal" Content = "＝" Grid.Row = "4" Grid.Column = "5" Margin = "3,3" Style = "{StaticResource OperateButton}" />
***/
namespace alphaCalc
{
    static class Operates
    {
        public const int Operate_CA = 0;
        public const int Operate_C = 1;
        public const int Operate_00 = 2;
        public const int Operate_DOT = 3;
        public const int Operate_PERCENT = 4;
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

        public int GetOperate() 
        { 
            return _operateCode; 
        }

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
            if (op == "") _operateCode = Operates.Operate_NONE;

            if (_operateCode >= 0) result = true;

            return result;
        }

        public CalcOperate ShallowCopy()
        {
            return((CalcOperate)MemberwiseClone());
        }
    }

    
}
