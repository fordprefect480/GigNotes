using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GigNotes.Model;

namespace GigNotes
{
    public partial class SetlistPage : PhoneApplicationPage
    {
        public SetlistPage()
        {
            InitializeComponent();            
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedSetlist = "";
                if (NavigationContext.QueryString.TryGetValue("setlistId", out selectedSetlist))
                {
                    int id = int.Parse(selectedSetlist);
                    DataContext = App.ViewModel.Setlists.Single(s => s.SetlistId == id);
                    LoadSetlist();
                }
                else
                {
                    MessageBox.Show("The setlist could not be loaded. Gutted.\n Maybe try making a new one.");
                }
            }
        }

        private void LoadSetlist()
        {
            var setlist = (DataContext as Setlist);
            PanoramaPage.Title = setlist.Name;
            GigInfoTextBlock.Text = setlist.AdditionalInfo;
            //foreach (var set in setlist.Sets)
            //{
            //    //add new panoramaitem
            //}

        }

    }
}