using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_CollectionDisplay.ViewModels.Commands.ImagesViewCommands
{
    public class ShiftSelectedImageCommand : ICommand
    {
        private readonly ImagesViewModel _imagesViewModel;
        private readonly int _indexes;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ShiftSelectedImageCommand(ImagesViewModel imagesViewModel, int indexes)
        {
            _imagesViewModel = imagesViewModel;
            _indexes = indexes;
        }

        public bool CanExecute(object? parameter)
        {
            return _imagesViewModel.SelectedContainer != null;
        }

        public void Execute(object? parameter)
        {
            _imagesViewModel.SelectedContainer?.Images?.ShiftIndex(_indexes);
        }
    }
}
