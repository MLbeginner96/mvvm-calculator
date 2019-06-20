using System.Collections.Generic;

namespace Calc.Helpers
{
    public static class CalcSymbols
    {
        public const string D0 = "0";

        public const string D1 = "1";

        public const string D2 = "2";

        public const string D3 = "3";

        public const string D4 = "4";

        public const string D5 = "5";

        public const string D6 = "6";

        public const string D7 = "7";

        public const string D8 = "8";

        public const string D9 = "9";

        public const string PlusMinus = "±";

        public const string Decimal = ",";

        public const string Ce = "CE";

        public const string C = "C";

        public const string BackSpace = "⌫";

        public const string Percent = "%";

        public const string SquareRoot = "√";

        public const string X2 = "x2";

        public const string X3 = "x3";

        public const string Fraction = "1/x";

        public const string Divide = "÷";

        public const string Multiply = "×";

        public const string Plus = "+";

        public const string Minus = "-";

        public const string Eqls = "=";


        public const string ErrIncorrectInput = "Введены неверные данные";

        public const string ErrDivisionByZero = "Деление на ноль невозможно";


        public static readonly List<string> Digits = new List<string> { D0, D1, D2, D3, D4, D5, D6, D7, D8, D9 };

        public static readonly List<string> ArithmeticOperations = new List<string> { Divide, Multiply, Plus, Minus };
    }
}