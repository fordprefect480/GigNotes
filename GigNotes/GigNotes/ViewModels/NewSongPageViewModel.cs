using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using GigNotes.Model;

namespace GigNotes.ViewModels
{
    public class NewSongPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private SetlistDataContext _db;

        public NewSongPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _db = new SetlistDataContext();
            
            SetUpInitialValues();
            SetUpAppBar();            
        }

        private void SetUpInitialValues()
        {
            BPM = 120;
            Keys = new BindableCollection<string>() {
                "N/A",
                "A", "A♭", "Am", "A♯m", "A♭m",
                "B", "B♭", "Bm", "B♭m",
                "C", "C♯", "C♭", "Cm", "C♯m", 
                "D",  "D♭", "Dm", "D♯m", 
                "E", "E♭", "Em",  "E♭m",
                "F", "F♯",  "Fm", "F♯m", 
                "G", "G♭", "Gm", "G♯m"
            };
        }
      
        #region Bound Controls
        
        #region App Bar

        public string ConfirmText { get; set; }

        public string CancelText { get; set; }

        public Uri ConfirmIcon { get; set; }

        public Uri CancelIcon { get; set; }

        public bool ConfirmIsVisible { get; set; }

        public bool CancelIsVisible { get; set; }

        private void SetUpAppBar()
        {
            ConfirmText = "ok";
            ConfirmIcon = new Uri("/Assets/Images/check.png", UriKind.Relative);
            ConfirmIsVisible = true;
            CancelText = "cancel";
            CancelIcon = new Uri("/Assets/Images/cancel.png", UriKind.Relative);
            CancelIsVisible = true;
        }

        #endregion

        private double _bpm;
        public double BPM
        {
            get { return _bpm; }
            set
            {
                _bpm = value;
                NotifyOfPropertyChange(() => BPM);
                NotifyOfPropertyChange(() => BPMText);
            }
        }

        private BindableCollection<string> _keys;
        public BindableCollection<string> Keys
        {
            get 
            {
                return _keys;
            }
            set 
            {
                _keys = value;
                NotifyOfPropertyChange(() => Keys);
            }
        }

        public string _selectedKey;
        public string SelectedKey
        {
            get { return _selectedKey; }
            set
            {
                _selectedKey = value;
                NotifyOfPropertyChange(() => SelectedKey);
            }
        }
        
        public string BPMText
        {
            get { return "♩=" + (int)_bpm; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        #endregion

        public void AddSong()
        {
            if (Title.Length > 0)
            {
                // Create a new song
                Song newSong = new Song
                {
                    Title = Title,
                    BPM = (int)BPM,
                    Key = SelectedKey
                };

                // Add a song to the data context
                _db.Repertoire.InsertOnSubmit(newSong);

                // Save changes to the database
                _db.SubmitChanges();
            }
            else
            {
                throw new ArgumentNullException("Song must be given a title.");
            }

            NavigateToMainPage(); 
        }

        public void Cancel()
        {
            NavigateToMainPage();            
        }

        public void NavigateToMainPage()
        {
            _navigationService.UriFor<MainPageViewModel>().Navigate();
        }
    }
}
