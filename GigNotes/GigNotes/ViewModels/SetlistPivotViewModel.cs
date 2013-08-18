using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using GigNotes.Model;

namespace GigNotes.ViewModels
{
    public class SetlistPivotViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private SetlistDataContext _db;

        private Set _set;
        public Set Set
        {
            get { return _set; }
            set
            {
                _set = value;
                NotifyOfPropertyChange(() => Set);
            }
        }

        private int _setId;
        public int SetId
        {
            get { return _setId; }
            set
            {
                _setId = value;
                NotifyOfPropertyChange(() => SetId);
                NotifyOfPropertyChange(() => Set);
                NotifyOfPropertyChange(() => SetName);
                NotifyOfPropertyChange(() => Songs);
            }
        }

        public BindableCollection<Song> Songs
        {
            get { return Set.Songs; }
            private set { }
        }

        public string SetName
        {
            get { return Set.SetName; }
            private set{}
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
                
        public SetlistPivotViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _db = new SetlistDataContext();

        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            LoadSet();
            SetUpAppBar();
        }
        
        private void LoadSet()
        {
            try
            {
                var loadedSet = _db.Sets.Single(s => s.SetId == SetId);
                Set = loadedSet;
                return;
            }
            catch
            {
                MessageBox.Show("The set could not be loaded. Soz mate.\nTry making a new one.");
                _navigationService.GoBack();
            }
        }

        private void SetUpAppBar()
        {
            AddSongText = "pick song";
            AddSongIcon = new Uri("/Assets/Images/add.png", UriKind.Relative);
            AddSongIsVisible = true;
            SelectionModeText = "select";
            SelectionModeIcon = new Uri("/Assets/Images/select.png", UriKind.Relative);
            CheckIfSelectionModeShouldBeHidden();
            DeleteSongsText = "delete";
            DeleteSongsIcon = new Uri("/Assets/Images/delete.png", UriKind.Relative);
            DeleteSongsIsVisible = false;
        }

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
    }
}
