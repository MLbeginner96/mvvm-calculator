using System;
using Calc.Helpers;

namespace Calc.Model
{
    public class CalcModel
    {
        private string Variable1 { get; set; } = string.Empty;

        private string Variable2 { get; set; } = string.Empty;

        private string Operation { get; set; } = string.Empty;

        private string LastOperation { get; set; } = string.Empty;

        private string LastInput { get; set; } = string.Empty;

        public string Display { get; set; } = CalcSymbols.D0;

        private bool startNewDisplay;


        public string Calculate(string input)
        {
            try
            {
                switch(input)
                {
                    case CalcSymbols.D0:
                    case CalcSymbols.D1:
                    case CalcSymbols.D2:
                    case CalcSymbols.D3:
                    case CalcSymbols.D4:
                    case CalcSymbols.D5:
                    case CalcSymbols.D6:
                    case CalcSymbols.D7:
                    case CalcSymbols.D8:
                    case CalcSymbols.D9:
                        InputDigits(input);
                        break;

                    case CalcSymbols.Decimal:
                        InputDemical();
                        break;

                    case CalcSymbols.PlusMinus:
                        InputPlusMinus();
                        break;

                    case CalcSymbols.Ce:
                        InputCe();
                        break;

                    case CalcSymbols.C:
                        InputC();
                        break;

                    case CalcSymbols.BackSpace:
                        InputBackSpace();
                        break;

                    case CalcSymbols.Percent:
                        InputPercent();
                        break;

                    case CalcSymbols.SquareRoot:
                        InputSquareRoot();
                        break;

                    case CalcSymbols.X2:
                        InputX2();
                        break;

                    case CalcSymbols.X3:
                        InputX3();
                        break;

                    case CalcSymbols.Fraction:
                        InputFraction();
                        break;

                    case CalcSymbols.Plus:
                    case CalcSymbols.Minus:
                    case CalcSymbols.Multiply:
                    case CalcSymbols.Divide:
                    case CalcSymbols.Eqls:
                        InputArithmetic(input);
                        break;
                }

                LastInput = input;

                return Display;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private void InputDigits(string input)
        {
            if(Display == CalcSymbols.D0 || startNewDisplay)
            {
                Display = input;
                startNewDisplay = false;
            }
            else
            {
                Display += input;
            }
        }

        private void InputDemical()
        {
            if(startNewDisplay) Display = CalcSymbols.D0 + CalcSymbols.Decimal;
            else
            {
                if(!Display.Contains(CalcSymbols.Decimal)) Display = Display + CalcSymbols.Decimal;
            }
        }

        private void InputPlusMinus()
        {
            if(Display.Contains(CalcSymbols.Minus) || Display == CalcSymbols.D0) Display = Display.Remove(Display.IndexOf(CalcSymbols.Minus, StringComparison.Ordinal), 1);
            else Display = CalcSymbols.Minus + Display;
        }

        private void InputCe()
        {
            Display = CalcSymbols.D0;
            startNewDisplay = true;
        }

        private void InputC()
        {
            Variable1 = string.Empty;
            Variable2 = string.Empty;
            Operation = string.Empty;
            LastOperation = string.Empty;
            Display = CalcSymbols.D0;

            startNewDisplay = true;
        }

        private void InputBackSpace()
        {
            Display = Display.Length > 1 ? Display.Substring(0, Display.Length - 1) : CalcSymbols.D0;
        }

        private void InputPercent()
        {
            if(Variable1 == string.Empty) Display = CalcSymbols.D0;
            else
            {
                var variable1 = Convert.ToDouble(Variable1);
                var variable2 = Convert.ToDouble(Display);
                var percent = variable1 * variable2 / 100;
                Display = percent.ToString();
            }

            startNewDisplay = true;
        }

        private void InputSquareRoot()
        {
            var variable = Convert.ToDouble(Display);
            if(variable < 0)
            {
                Display = CalcSymbols.ErrIncorrectInput;
            }
            else
            {
                var squareRoot = Math.Sqrt(variable);
                Display = squareRoot.ToString();
            }

            startNewDisplay = true;
        }

        private void InputX2()
        {
            var variable = Convert.ToDouble(Display);
            var squareRoot = Math.Pow(variable, 2);
            Display = squareRoot.ToString();
                        
            startNewDisplay = true;
        }

        private void InputX3()
        {
            var variable = Convert.ToDouble(Display);
            var squareRoot = Math.Pow(variable, 3);
            Display = squareRoot.ToString();
                        
            startNewDisplay = true;
        }

        private void InputFraction()
        {
            var variable = Convert.ToDouble(Display);
            var squareRoot = 1 / variable;
            Display = squareRoot.ToString();
                        
            startNewDisplay = true;
        }
        
        private void InputArithmetic(string input)
        {
            if(CalcSymbols.ArithmeticOperations.Contains(input) && CalcSymbols.ArithmeticOperations.Contains(LastInput)) // повторное нажатие [+ - * /]
            {
                LastOperation = input;
                return;
            }

            if(Variable1 == string.Empty || LastOperation == CalcSymbols.Eqls && input != CalcSymbols.Eqls)
            {
                Variable1 = Display;
                LastOperation = input;
            }
            else
            {
                if(input != CalcSymbols.Eqls || LastOperation != CalcSymbols.Eqls) // проверка на повторное нажатие [=]
                {
                    Variable2 = Display;
                    Operation = LastOperation;
                    LastOperation = input;
                }

                if(string.IsNullOrEmpty(Variable2)) return;

                var variable1 = Convert.ToDouble(Variable1);
                var variable2 = Convert.ToDouble(Variable2);

                switch(Operation)
                {
                    case CalcSymbols.Plus:
                        Display = (variable1 + variable2).ToString();
                        break;

                    case CalcSymbols.Minus:
                        Display = (variable1 - variable2).ToString();
                        break;

                    case CalcSymbols.Multiply:
                        Display = (variable1 * variable2).ToString();
                        break;

                    case CalcSymbols.Divide:
                        Display = variable2 == 0 ? CalcSymbols.ErrDivisionByZero : (variable1 / variable2).ToString();
                        break;
                }


                Variable1 = Display;
            }

            startNewDisplay = true;
        }
    }
}
