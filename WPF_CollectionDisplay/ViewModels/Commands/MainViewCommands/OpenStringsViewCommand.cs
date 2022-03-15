using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_CollectionDisplay.ViewModels.Commands.MainViewCommands
{
    public class OpenStringsViewCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public OpenStringsViewCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return !(_mainViewModel.CurrentUC is StringsViewModel);
        }

        public void Execute(object? parameter)
        {
            _mainViewModel.CurrentUC = new StringsViewModel();
        }
    }
}
