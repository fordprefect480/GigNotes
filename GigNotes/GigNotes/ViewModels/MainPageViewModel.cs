using Caliburn.Micro;
using GigNotes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using Microsoft.Phone.Controls;
using Caliburn.Micro.BindableAppBar;

namespace GigNotes.ViewModels
{
    public class MainPageViewModel : Conductor<IScreen>.Collection.OneActive 
    {
        private MainPageSetlistsPivotViewModel _mpSetlistsPivotVM;
        private MainPageSongsPivotViewModel _mpSongsPivotVM;
        private MainPageAboutPivotViewModel _mpAboutPivotVM;

        public string ApplicationTitle
        {
            get
            {
                return "GIG NOTES";
            }
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
                
        public MainPageViewModel(MainPageSetlistsPivotViewModel mpSetlistsPivotVM,
                                 MainPageSongsPivotViewModel mpSongsPivotVM, 
                                 MainPageAboutPivotViewModel mpAboutPivotVM)
        {
            _songs = new BindableCollection<Song>();
            _mpSetlistsPivotVM = mpSetlistsPivotVM;
            _mpSongsPivotVM = mpSongsPivotVM;
            _mpAboutPivotVM = mpAboutPivotVM;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(_mpSetlistsPivotVM);
            Items.Add(_mpSongsPivotVM);
            Items.Add(_mpAboutPivotVM);

            ActivateItem(_mpSetlistsPivotVM);

            AppBarConductor.Mixin(this);
        }
    }
}