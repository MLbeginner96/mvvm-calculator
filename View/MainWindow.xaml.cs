using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Calc.Helpers;

namespace Calc.View
{
    public partial class MainWindow
    {
        private readonly CalcButtonsCollection calcButtonsCollectionPortrait = new CalcButtonsCollection(4, 6, new List<string>
        {
            CalcSymbols.Percent,        CalcSymbols.SquareRoot,  CalcSymbols.X2,         CalcSymbols.Fraction,
            CalcSymbols.Ce,             CalcSymbols.C,           CalcSymbols.BackSpace,  CalcSymbols.Divide,
            CalcSymbols.D7,             CalcSymbols.D8,          CalcSymbols.D9,         CalcSymbols.Multiply,
            CalcSymbols.D4,             CalcSymbols.D5,          CalcSymbols.D6,         CalcSymbols.Minus,
            CalcSymbols.D1,             CalcSymbols.D2,          CalcSymbols.D3,         CalcSymbols.Plus,
            CalcSymbols.PlusMinus,      CalcSymbols.D0,          CalcSymbols.Decimal,    CalcSymbols.Eqls
        });

        private readonly CalcButtonsCollection calcButtonsCollectionLandscape = new CalcButtonsCollection(5, 5, new List<string>
        {
            CalcSymbols.Percent,        CalcSymbols.Ce,          CalcSymbols.C,          CalcSymbols.BackSpace,      CalcSymbols.Divide,
            CalcSymbols.SquareRoot,     CalcSymbols.D7,          CalcSymbols.D8,         CalcSymbols.D9,             CalcSymbols.Multiply,
            CalcSymbols.X2,             CalcSymbols.D4,          CalcSymbols.D5,         CalcSymbols.D6,             CalcSymbols.Minus,
            CalcSymbols.X3,             CalcSymbols.D1,          CalcSymbols.D2,         CalcSymbols.D3,             CalcSymbols.Plus,
            CalcSymbols.Fraction,       CalcSymbols.PlusMinus,   CalcSymbols.D0,         CalcSymbols.Decimal,        CalcSymbols.Eqls
        });


        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                // blur based on https://gist.github.com/riverar/fd6525579d6bbafc6e48
                var windowHelper = new WindowInteropHelper(this);
                var accent = new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND };
                var accentStructSize = Marshal.SizeOf(accent);
                var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                Marshal.StructureToPtr(accent, accentPtr, false);
                var data = new WindowCompositionAttributeData { Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY, SizeOfData = accentStructSize, Data = accentPtr };
                SetWindowCompositionAttribute(windowHelper.Handle, ref data);
                Marshal.FreeHGlobal(accentPtr);
            };

            // window drag&drop
            MouseDown += (s, args) => { if(args.LeftButton == MouseButtonState.Pressed) DragMove(); };

            // maximizefix
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }


        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void CommandBinding_Close(object sender, ExecutedRoutedEventArgs e) => SystemCommands.CloseWindow(this);

        private void CommandBinding_Minimize(object sender, ExecutedRoutedEventArgs e) => SystemCommands.MinimizeWindow(this);

        private void CommandBinding_Maximize(object sender, ExecutedRoutedEventArgs e) => MaximizeNormalize();

        private void TitleBarTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2) MaximizeNormalize();
        }

        private void MaximizeNormalize()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(ActualWidth > 700 && ActualHeight > 650) Landscape();
            else Portrait();
        }

        
        private bool isPortrait;

        private void Portrait()
        {
            try
            {
                if(isPortrait) return;
                isPortrait = true;
                calcButtonsCollectionPortrait.SetCalcButtons(GridButtons);
            }
            catch(Exception)
            {
                return;
            }
        }

        private void Landscape()
        {
            try
            {
                if(!isPortrait) return;
                isPortrait = false;
                calcButtonsCollectionLandscape.SetCalcButtons(GridButtons);
            }
            catch(Exception)
            {
                return;
            }
        }

        


        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private enum AccentState
        {
            //ACCENT_DISABLED = 1,
            //ACCENT_ENABLE_GRADIENT = 0,
            //ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            //ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public AccentState AccentState;
            private readonly int AccentFlags;
            private readonly int GradientColor;
            private readonly int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }
    }
}
