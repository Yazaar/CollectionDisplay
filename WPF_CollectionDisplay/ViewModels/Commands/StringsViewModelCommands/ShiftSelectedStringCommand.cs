using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_CollectionDisplay.ViewModels.StringsViewModelCommands.Commands
{
    public class ShiftSelectedStringCommand : ICommand
    {
        private readonly StringsViewModel _mainViewModel;
        private readonly int _indexes;

        public event EventHandler? CanExecuteChanged { add { } remove { } }

        public ShiftSelectedStringCommand(StringsViewModel mainViewModel, int indexes)
        {
            _mainViewModel = mainViewModel;
            _indexes = indexes;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _mainViewModel.CollectionDisplay.ShiftIndex(_indexes);
            _mainViewModel.Index = _mainViewModel.CollectionDisplay.Index.ToString();
            _mainViewModel.Item = _mainViewModel.CollectionDisplay.Item == null ? "" : _mainViewModel.CollectionDisplay.Item;
        }
    }
}
