using Calc.Helpers;
using Calc.Model;

namespace Calc.ViewModel
{
    public class CalcViewModel : ViewModelBase
    {
        private readonly CalcModel calcModel = new CalcModel();

        public string Display
        {
            get => calcModel.Display;
            set
            {
                calcModel.Display = value;
                OnPropertyChanged();
            }
        }

        private Command inputCommand;
        public Command InputCommand => inputCommand ?? (inputCommand = new Command(o => Display = calcModel.Calculate(o.ToString())));
    }
}