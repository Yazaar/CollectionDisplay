using CollectionDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CollectionDisplay.Models
{
    public class ImageContainer
    {
        public string Name { get; set; } = string.Empty;
        public CollectionDisplay<Image>? Images { get; set; }
    }
}
