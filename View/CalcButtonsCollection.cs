using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Calc.Helpers;

namespace Calc.View
{
    public class CalcButtonsCollection
    {
        private int ColumnsCount { get; }

        private int RowsCount { get; }

        private IEnumerable<string> Symbols { get; }


        public CalcButtonsCollection(int columnsCount, int rowsCount, IEnumerable<string> buttons)
        {
            ColumnsCount = columnsCount;

            RowsCount = rowsCount;

            Symbols = buttons;
        }

        public void SetCalcButtons(Grid grid)
        {
            try
            {
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                grid.Children.Clear();

                for(var i = 0; i < ColumnsCount; i++) grid.ColumnDefinitions.Add(new ColumnDefinition());

                for(var i = 0; i < RowsCount; i++) grid.RowDefinitions.Add(new RowDefinition());

                foreach(var button in GetButtons()) grid.Children.Add(button);
            }
            catch(Exception)
            {
                return;
            }
        }

        private IEnumerable<Button> GetButtons()
        {
            var columnIndex = 0;

            var rowIndex = 0;

            var buttons = new List<Button>();

            foreach(var symbol in Symbols)
            {
                var button = new Button();
                button.SetBinding(ButtonBase.CommandProperty, new Binding { Path = new PropertyPath("InputCommand") });
                button.CommandParameter = symbol;
                button.Content = symbol;
                button.Style = Application.Current.Resources[CalcSymbols.Digits.Contains(symbol) ? "CalcButtonDigit" : "CalcButton"] as Style;
                if(symbol == CalcSymbols.Eqls) button.IsDefault = true;
                button.Focusable = false;
                
                Grid.SetColumn(button, columnIndex);
                Grid.SetRow(button, rowIndex);

                buttons.Add(button);


                columnIndex++;
                if(columnIndex == ColumnsCount)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
                
            }

            return buttons;
        }

    }
}
