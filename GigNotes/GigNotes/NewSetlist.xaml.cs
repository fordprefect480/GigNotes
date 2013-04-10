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
using System.Windows.Input;

namespace GigNotes
{
    public partial class NewSetlist : PhoneApplicationPage
    {
        public NewSetlist()
        {
            InitializeComponent();           
            
            // Set the page DataContext property to the ViewModel.
            this.DataContext = App.ViewModel;
        }

        private void NewSetlist_Loaded(object sender, EventArgs e)
        {
            NameTextBox.Focus();
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            // Confirm there is some text in the text box.
            if (NameTextBox.Text.Length > 0)
            {
                // Create a new to-do item.
                Setlist newSetlist = new Setlist
                {
                    Name = NameTextBox.Text,
                    GigDate = GigDatePicker.Value,
                    AdditionalInfo = AdditionalTextBox.Text
                };

                // Add the item to the ViewModel.
                App.ViewModel.AddSetlist(newSetlist);

                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        //private void appBarNextButton_Click(object sender, EventArgs e)
        //{
            //List<Control> textboxesOrDatePickers = ContentPanel.Children.Where(c => c is TextBox || c is DatePicker).Cast<Control>().ToList();             
            //for (int i = 0; i < textboxesOrDatePickers.Count(); i++)
            //{
            //    if (FocusManager.GetFocusedElement().Equals(ContentPanel.Children[i]))
            //    {
            //        if (i + 1 >= textboxesOrDatePickers.Count())
            //        {
            //            i = 0;
            //        }
            //        else
            //        {
            //            i++;
            //        }
            //        Control nextChild = (Control)textboxesOrDatePickers[i];
            //        nextChild.Focus();                   
            //    }
            //}
        //}        
    }
}