using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using GigNotes.Model;
using Microsoft.Phone.Controls;

namespace GigNotes.ViewModels
{
    public class MainPageSetlistsPivotViewModel : Screen
    {
        private SetlistDataContext _db;
        private INavigationService _navigationService;

        public string DisplayName
        {
            get { return "setlists"; }
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
                if (value)
                {
                    _isInMultiSelectMode = true;

                    AddSetlistIsVisible = false;
                    DeleteSetlistsIsVisible = true;
                }
                else
                {
                    _isInMultiSelectMode = false;

                    AddSetlistIsVisible = true;
                    DeleteSetlistsIsVisible = false;
                }

                NotifyOfPropertyChange(() => IsInMultiSelectMode);
            }
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

        private string _deleteSetlistsText;
        public string DeleteSetlistsText
        {
            get { return _deleteSetlistsText; }
            set
            {
                if (_deleteSetlistsText == value) return;
                _deleteSetlistsText = value;
                NotifyOfPropertyChange(() => DeleteSetlistsText);
            }
        }

        private Uri _deleteSetlistsIcon;
        public Uri DeleteSetlistsIcon
        {
            get { return _deleteSetlistsIcon; }
            set
            {
                if (_deleteSetlistsIcon == value) return;
                _deleteSetlistsIcon = value;
                NotifyOfPropertyChange(() => DeleteSetlistsIcon);
            }
        }

        private bool _deleteSetlistsIsVisible;
        public bool DeleteSetlistsIsVisible
        {
            get { return _deleteSetlistsIsVisible; }
            set
            {
                if (_deleteSetlistsIsVisible == value) return;
                _deleteSetlistsIsVisible = value;
                NotifyOfPropertyChange(() => DeleteSetlistsIsVisible);
            }
        }

        #endregion 

        public MainPageSetlistsPivotViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _db = new SetlistDataContext();
            _setlists = new BindableCollection<Setlist>();

            SetUpAppBar();
            LoadCollectionsFromDatabase();   
        }
                
        public void ToggleSelectionMode()
        {
            IsInMultiSelectMode = !IsInMultiSelectMode;
        }
        
        public void AddSetlist()
        {
            GoToNewSetlistPage();
        }

        public void DeleteSetlists()
        {
            var setlistsToDeleteFrom = Setlists.ToList<Setlist>();
            foreach (Setlist item in setlistsToDeleteFrom)
            {
                if (item.IsSelected)
                {
                    DeleteSetlist(item);
                }
            }

            SaveToDB();
            ToggleSelectionMode();
        }
        
        public void DeleteSetlist(Setlist setlistForDelete)
        {
            // Remove the setlist from the observable collection.
            Setlists.Remove(setlistForDelete);

            // Remove the setlist from the data context.
            _db.Setlists.DeleteOnSubmit(setlistForDelete);
        }
        
        public void OpenSetlist(Setlist selectedSetlist)
        {
            // If selected item is null (no selection) do nothing
            if (selectedSetlist == null)
                return;

            // Navigate to the new page
            _navigationService.UriFor<SetlistPageViewModel>().WithParam<int>(x => x.SetlistId, selectedSetlist.SetlistId).Navigate();
        }

        public void SelectionChanged(LongListMultiSelector llms)
        {
            Setlist[] listOfSelectedSetlist = new Setlist[llms.SelectedItems.Count];
            llms.SelectedItems.CopyTo(listOfSelectedSetlist, 0);

            foreach (var item in Setlists)
            {
                if (listOfSelectedSetlist.Any(s => s.SetlistId == item.SetlistId))
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }
        
        #region Private Methods

        private void SetUpAppBar()
        {
            AddSetlistText = "add setlist";
            AddSetlistIcon = new Uri("/Assets/Images/add.png", UriKind.Relative);
            AddSetlistIsVisible = true;
            SelectionModeText = "select";
            SelectionModeIcon = new Uri("/Assets/Images/select.png", UriKind.Relative);
            SelectionModeIsVisible = true;
            DeleteSetlistsText = "delete";
            DeleteSetlistsIcon = new Uri("/Assets/Images/delete.png", UriKind.Relative);
            DeleteSetlistsIsVisible = false;
        }

        private void LoadCollectionsFromDatabase()
        {
            // Specify the query for all setlists in the database.
            var setlistsInDB = from Setlist sl in _db.Setlists
                               orderby sl.GigDate
                               select sl;

            // Query the database and load all setlists.
            _setlists.AddRange(setlistsInDB);

            // Specify the query for all songs in the database.
            //var songsInDB = from Song s in _db.Repertoire
            //                orderby s.Title
            //                select s;

            //// Query the database and load all songs.
            //_songs.AddRange(songsInDB);
        }

        private void SaveToDB()
        {
            _db.SubmitChanges();
        }

        private void GoToNewSetlistPage()
        {
            _navigationService.UriFor<NewSetlistPageViewModel>().Navigate();
        }
        
        #endregion
    }
}
