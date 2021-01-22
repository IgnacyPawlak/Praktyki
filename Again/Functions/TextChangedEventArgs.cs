using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Again.Functions
{
    public class TextChangedEventArgs : RoutedEventArgs
    {
        public Size Offset { get; set; }

        public TextChangedEventArgs()
        {
        }

        public TextChangedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public TextChangedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
