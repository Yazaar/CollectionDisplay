using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_CollectionDisplay.ViewModels.Commands.MainViewCommands;

namespace WPF_CollectionDisplay.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentUC = new StringsViewModel();
        public object CurrentUC { get => _currentUC; set { _currentUC = value; OnPropertyChanged(); } }

        public ICommand OpenStringsViewCommand { get; }
        public ICommand OpenImagesViewCommand { get; }

        public MainViewModel()
        {
            OpenStringsViewCommand = new OpenStringsViewCommand(this);
            OpenImagesViewCommand = new OpenImagesViewCommand(this);
        }
    }
}
