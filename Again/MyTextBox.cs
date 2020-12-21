using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Again
{
    public class MyTextBox:TextBox
    {

        public static readonly DependencyProperty MytextProperty = DependencyProperty.Register("MyText", typeof(string), typeof(MyTextBox));


        public string Mytext
        {
            get => (string)GetValue(MytextProperty);
            set => SetValue(MytextProperty, value);
        }

        string _test = "ten tekst też znajdź";
        public string Test { get => _test; set => _test = value; }
    }
}
