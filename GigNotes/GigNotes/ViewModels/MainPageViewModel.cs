using Caliburn.Micro;
using GigNotes.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

namespace GigNotes.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private SetlistDataContext _db;

        public string ApplicationTitle
        {
            get
            {
                return "GIG NOTES";
            }
        }

        private BindableCollection<Setlist> _setlists;
        public BindableCollection<Setlist> Setlists
        {
            get { return _setlists; }
            set
            {
                _setlists = value;
                NotifyOfPropertyChange(() => Setlists);
            }
        }

        private bool _isInMultiSelectMode;
        public bool IsInMultiSelectMode
        {
            get { return _isInMultiSelectMode; }
            set
            {
                _isInMultiSelectMode = value;
                NotifyOfPropertyChange(() => IsInMultiSelectMode);
            }
        }

        public MainPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            _db = new SetlistDataContext();
            _setlists = new BindableCollection<Setlist>();

            AddSetlistText = "add setlist";
            AddSetlistIcon = new Uri("/Assets/Images/add.png", UriKind.Relative);
            AddSetlistIsVisible = true;
            SelectionModeText = "select";
            SelectionModeIcon = new Uri("/Assets/Images/select.png", UriKind.Relative);
            SelectionModeIsVisible = true;
             
            LoadCollectionsFromDatabase();            
        }

        private void LoadDesignData()
        {
            var testSetlist1 = new Setlist() { SetlistId = 1, Name = "Kierra's Wedding", GigDate = DateTime.Now.Date.AddDays(7), AdditionalInfo = "Park behind restaurant" };
            var testSetlist2 = new Setlist() { SetlistId = 2, Name = "Swing Dancing", GigDate = DateTime.Now.Date.AddDays(12), AdditionalInfo = "Set up 7pm" };
            _setlists.Add(testSetlist1);
            _setlists.Add(testSetlist2);
        }

        private void LoadCollectionsFromDatabase()
        {
            // Specify the query for all to-do items in the database.
            var setlistsInDB = from Setlist sl in _db.Setlists
                               select sl;

            // Query the database and load all to-do items.
            _setlists.AddRange(setlistsInDB);
        }

        #region App Bar

        private string _addSetlistText;
        public string AddSetlistText
        {
            get { return _addSetlistText; }
            set
            {
                if (_addSetlistText == value) return;
                _addSetlistText = value;
                NotifyOfPropertyChange(() => AddSetlistText);
            }
        }

        private Uri _addSetlistIcon;
        public Uri AddSetlistIcon
        {
            get { return _addSetlistIcon; }
            set
            {
                if (_addSetlistIcon == value) return;
                _addSetlistIcon = value;
                NotifyOfPropertyChange(() => AddSetlistIcon);
            }
        }

        private bool _addSetlistIsVisible;
        public bool AddSetlistIsVisible
        {
            get { return _addSetlistIsVisible; }
            set
            {
                if (_addSetlistIsVisible == value) return;
                _addSetlistIsVisible = value;
                NotifyOfPropertyChange(() => AddSetlistIsVisible);
            }
        }

        private string _selectionModeText;
        public string SelectionModeText
        {
            get { return _selectionModeText; }
            set
            {
                if (_selectionModeText == value) return;
                _selectionModeText = value;
                NotifyOfPropertyChange(() => SelectionModeText);
            }
        }

        private Uri _selectionModeIcon;
        public Uri SelectionModeIcon
        {
            get { return _selectionModeIcon; }
            set
            {
                if (_selectionModeIcon == value) return;
                _selectionModeIcon = value;
                NotifyOfPropertyChange(() => SelectionModeIcon);
            }
        }

        private bool _selectionModeIsVisible;
        public bool SelectionModeIsVisible
        {
            get { return _selectionModeIsVisible; }
            set
            {
                if (_selectionModeIsVisible == value) return;
                _selectionModeIsVisible = value;
                NotifyOfPropertyChange(() => SelectionModeIsVisible);
            }
        }

        #endregion 

        private void OpenSetlist()
        {
            /*
            // If selected item is null (no selection) do nothing
            if (SetlistLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/SetlistPage.xaml?setlistId=" + (SetlistLongListSelector.SelectedItem as Setlist).SetlistId, UriKind.Relative));

            // Reset selected item to null (no selection)
            SetlistLongListSelector.SelectedItem = null;
             * */
        }

        public void AddSetlist()
        {
            GoToNewSetlistPage();
        }

        private void GoToNewSetlistPage()
        {
            _navigationService.UriFor<NewSetlistPageViewModel>().Navigate();
        }

        public void ToggleSelectionMode()
        {
            if (IsInMultiSelectMode)
            {
                IsInMultiSelectMode = false;
            }
            else
            {
                IsInMultiSelectMode = true;
            }
        }

        // Remove a to-do task item from the database and collections.
        public void DeleteSetlist(Setlist setlistForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            //Setlists.Remove(setlistForDelete);

            // Remove the to-do item from the data context.
            //setlistDB.Setlists.DeleteOnSubmit(setlistForDelete);

            // Save changes to the database.
            //setlistDB.SubmitChanges();
        }
    }
}