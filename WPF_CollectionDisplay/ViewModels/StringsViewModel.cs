using CollectionDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_CollectionDisplay.Models;
using WPF_CollectionDisplay.ViewModels.StringsViewModelCommands.Commands;

namespace WPF_CollectionDisplay.ViewModels
{
    public class StringsViewModel : BaseViewModel
    {
        private string _index = string.Empty;

        public string Item { get => CollectionDisplay.Item == null ? "" : CollectionDisplay.Item ;
            set
            {
                CollectionDisplay.Items[CollectionDisplay.Index] = value;
                OnPropertyChanged();
            }
        }

        public string Index { get => _index;
            set
            {
                _index = value;
                OnPropertyChanged();
                if (int.TryParse(value, out int intValue) && intValue >= 0 && intValue < CollectionDisplay.Items.Count)
                {
                    CollectionDisplay.Items.Move(CollectionDisplay.Index, intValue);
                    CollectionDisplay.Index = intValue;
                }
            }
        }

        public CollectionDisplay<string> CollectionDisplay { get; } = new();

        public ICommand NextItemCommand { get; }
        public ICommand PreviousItemCommand { get; }

        public StringsViewModel()
        {
            NextItemCommand = new ShiftSelectedStringCommand(this, 1);
            PreviousItemCommand = new ShiftSelectedStringCommand(this, -1);

            CollectionDisplay.Items.Add("A");
            CollectionDisplay.Items.Add("B");
            CollectionDisplay.Items.Add("C");
            CollectionDisplay.Items.Add("D");
            CollectionDisplay.Items.Add("E");
            CollectionDisplay.Items.Add("F");

            Index = CollectionDisplay.Index.ToString();
        }
    }
}
