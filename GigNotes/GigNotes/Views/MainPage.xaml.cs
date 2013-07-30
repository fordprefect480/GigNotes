using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GigNotes.Resources;
using GigNotes.ViewModels;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using GigNotes.Model;
using System.Collections.ObjectModel;

namespace GigNotes.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
                       
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPivot.SelectedIndex == 0)
            {                
                AppBar.IsVisible = true;
                AppBar.IsMenuEnabled = true;
            }
            else
            {
                AppBar.IsVisible = false;
                AppBar.IsMenuEnabled = false;
            }
        }
    }
}