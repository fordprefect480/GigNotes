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

namespace GigNotes.Views
{
    public partial class NewSetlistPage : PhoneApplicationPage
    {
        public NewSetlistPage()
        {
            InitializeComponent();
        }

        private void NewSetlist_Loaded(object sender, EventArgs e)
        {
            Name.Focus();
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