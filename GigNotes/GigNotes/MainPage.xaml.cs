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

namespace GigNotes
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (SetlistLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/SetlistPage.xaml?setlistId=" + (SetlistLongListSelector.SelectedItem as Setlist).SetlistId, UriKind.Relative));

            // Reset selected item to null (no selection)
            SetlistLongListSelector.SelectedItem = null;
        }

        private void newSetlistAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewSetlist.xaml", UriKind.Relative));
        }
    
        private void deleteSetlistButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                Setlist setlistForDelete = button.DataContext as Setlist;

                App.ViewModel.DeleteSetlist(setlistForDelete);
            }

            // Put the focus back to the main page.
            this.Focus();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();
        }        
        
        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.ViewModel.LoadCollectionsFromDatabase();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPivot.SelectedIndex == 0)
            {
                ApplicationBar.IsVisible = true;
                ApplicationBar.IsMenuEnabled = true;
            }
            else
            {
                ApplicationBar.IsVisible = false;
                ApplicationBar.IsMenuEnabled = false;
            }
        }
    }
}