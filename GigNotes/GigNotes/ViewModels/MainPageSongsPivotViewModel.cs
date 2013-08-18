using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using GigNotes.Model;
using Microsoft.Phone.Controls;

namespace GigNotes.ViewModels
{
    public class MainPageSongsPivotViewModel : Screen
    {
        private SetlistDataContext _db;
        private INavigationService _navigationService;

        public string DisplayName
        {
            get { return "songbook"; }
        }

        public Uri DisplayIcon
        {
            get { return new Uri("/Assets/Images/songbook.png", UriKind.Relative); }
        }

        private BindableCollection<Song> _songs;
        public BindableCollection<Song> Songs
        {
            get { return _songs; }
            set
            {
                _songs = value;
                NotifyOfPropertyChange(() => Songs);
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

                    AddSongIsVisible = false;
                    DeleteSongsIsVisible = true;
                }
                else
                {
                    _isInMultiSelectMode = false;

                    AddSongIsVisible = true;
                    DeleteSongsIsVisible = false;
                }

                NotifyOfPropertyChange(() => IsInMultiSelectMode);
            }
        }

        #region App Bar

        private string _addSongText;
        public string AddSongText
        {
            get { return _addSongText; }
            set
            {
                if (_addSongText == value) return;
                _addSongText = value;
                NotifyOfPropertyChange(() => AddSongText);
            }
        }

        private Uri _addSongIcon;
        public Uri AddSongIcon
        {
            get { return _addSongIcon; }
            set
            {
                if (_addSongIcon == value) return;
                _addSongIcon = value;
                NotifyOfPropertyChange(() => AddSongIcon);
            }
        }

        private bool _addSongIsVisible;
        public bool AddSongIsVisible
        {
            get { return _addSongIsVisible; }
            set
            {
                if (_addSongIsVisible == value) return;
                _addSongIsVisible = value;
                NotifyOfPropertyChange(() => AddSongIsVisible);
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

        private string _deleteSongsText;
        public string DeleteSongsText
        {
            get { return _deleteSongsText; }
            set
            {
                if (_deleteSongsText == value) return;
                _deleteSongsText = value;
                NotifyOfPropertyChange(() => DeleteSongsText);
            }
        }

        private Uri _deleteSongsIcon;
        public Uri DeleteSongsIcon
        {
            get { return _deleteSongsIcon; }
            set
            {
                if (_deleteSongsIcon == value) return;
                _deleteSongsIcon = value;
                NotifyOfPropertyChange(() => DeleteSongsIcon);
            }
        }

        private bool _deleteSongsIsVisible;
        public bool DeleteSongsIsVisible
        {
            get { return _deleteSongsIsVisible; }
            set
            {
                if (_deleteSongsIsVisible == value) return;
                _deleteSongsIsVisible = value;
                NotifyOfPropertyChange(() => DeleteSongsIsVisible);
            }
        }

        #endregion 

        public MainPageSongsPivotViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _db = new SetlistDataContext();
            _songs = new BindableCollection<Song>();

            LoadCollectionsFromDatabase();  
            SetUpAppBar();             
        }

        public void ToggleSelectionMode()
        {
            IsInMultiSelectMode = !IsInMultiSelectMode;
        }

        public void SelectionChanged(LongListMultiSelector llms)
        {
            Song[] listOfSelectedSongs = new Song[llms.SelectedItems.Count];
            llms.SelectedItems.CopyTo(listOfSelectedSongs, 0);

            foreach (var item in Songs)
            {
                if (listOfSelectedSongs.Any(s => s.SongId == item.SongId))
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }

        public void AddSong()
        {
            _navigationService.UriFor<NewSongPageViewModel>().Navigate();
        }

        public void DeleteSongs()
        {
            var songsToDeleteFrom = Songs.ToList<Song>();
            foreach (Song item in songsToDeleteFrom)
            {
                if (item.IsSelected)
                {
                    DeleteSong(item);
                }
            }

            SaveToDB();
            CheckIfSelectionModeShouldBeHidden();
            //ToggleSelectionMode();
        }

        public void DeleteSong(Song songForDelete)
        {
            // Remove the setlist from the observable collection.
            Songs.Remove(songForDelete);

            // Remove the setlist from the data context.
            _db.Repertoire.DeleteOnSubmit(songForDelete);

            CheckIfSelectionModeShouldBeHidden();
        }

        public void OpenSong(Song selectedSong)
        {
            // If selected item is null (no selection) do nothing
            if (selectedSong == null)
                return;

            // Navigate to the new page
            //_navigationService.UriFor<SetlistPageViewModel>().WithParam<int>(x => x.SetlistId, selectedSetlist.SetlistId).Navigate();
        }

        #region Private Methods

        private void CheckIfSelectionModeShouldBeHidden()
        {
            if (Songs.Count == 0)
            {
                SelectionModeIsVisible = false;
            }
            else
            {
                SelectionModeIsVisible = true;
            }
        }

        private void SetUpAppBar()
        {
            AddSongText = "add song";
            AddSongIcon = new Uri("/Assets/Images/add.png", UriKind.Relative);
            AddSongIsVisible = true;
            SelectionModeText = "select";
            SelectionModeIcon = new Uri("/Assets/Images/select.png", UriKind.Relative);
            CheckIfSelectionModeShouldBeHidden();
            DeleteSongsText = "delete";
            DeleteSongsIcon = new Uri("/Assets/Images/delete.png", UriKind.Relative);
            DeleteSongsIsVisible = false;
        }

        private void LoadCollectionsFromDatabase()
        {
            // Specify the query for all songs in the database.
            var songsInDB = from Song s in _db.Repertoire
                            orderby s.Title
                            select s;

            // Query the database and load all songs.
            _songs.AddRange(songsInDB);
        }

        private void SaveToDB()
        {
            _db.SubmitChanges();
        }
              
        #endregion
    }
}
