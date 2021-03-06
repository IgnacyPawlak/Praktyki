﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Again.Functions;

namespace Again.Views
{
    /// <summary>
    /// Interaction logic for View1.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        public ResultsView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, Functions.TextChangedEventArgs e)
        {
            scrollviewer.ScrollToVerticalOffset(e.Offset.Height-this.ActualHeight/2);
        }
    }
}
