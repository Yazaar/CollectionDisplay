using CollectionDisplay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_CollectionDisplay.Models;
using WPF_CollectionDisplay.ViewModels.Commands.ImagesViewCommands;

namespace WPF_CollectionDisplay.ViewModels
{
    public class ImagesViewModel : BaseViewModel
    {
        private readonly string[] _images = new string[]
        {
            "pack://application:,,,/Images/imageA.png",
            "pack://application:,,,/Images/imageB.png",
            "pack://application:,,,/Images/imageC.png"
        };

        public ImageContainer? SelectedContainer { get; set; }
        public ImageContainer[] Containers { get; }

        public ICommand NextImageCommand { get; }
        public ICommand PreviousImageCommand { get; }

        public ImagesViewModel()
        {
            NextImageCommand = new ShiftSelectedImageCommand(this, 1);
            PreviousImageCommand = new ShiftSelectedImageCommand(this, -1);

            Containers = new ImageContainer[]
            {
                new ImageContainer()
                {
                    Name = "container 1",
                    Images = new CollectionDisplay<Image>()
                    {
                        Items = new ObservableCollection<Image>()
                        {
                            new Image() { Name = "img 1", Source = _images[0] },
                            new Image() { Name = "img 2", Source = _images[1] },
                            new Image() { Name = "img 3", Source = _images[2] }
                        }
                    }
                },
                new ImageContainer()
                {
                    Name = "container 2",
                    Images = new CollectionDisplay<Image>()
                    {
                        Items = new ObservableCollection<Image>()
                        {
                            new Image() { Name = "img 1", Source = _images[0] },
                            new Image() { Name = "img 2", Source = _images[1] },
                            new Image() { Name = "img 3", Source = _images[2] }
                        }
                    }
                },
                new ImageContainer()
                {
                    Name = "container 3",
                    Images = new CollectionDisplay<Image>()
                    {
                        Items = new ObservableCollection<Image>()
                        {
                            new Image() { Name = "img 1", Source = _images[0] },
                            new Image() { Name = "img 2", Source = _images[1] },
                            new Image() { Name = "img 3", Source = _images[2] }
                        }
                    }
                }
            };
        }
    }
}
