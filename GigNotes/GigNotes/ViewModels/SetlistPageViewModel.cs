using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.Extras;
using GigNotes.Model;

namespace GigNotes.ViewModels
{
    public class SetlistPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region Properties

        private SetlistPivotViewModel item1;
        private SetlistPivotViewModel item2;
        private SetlistPivotViewModel item3;
        private SetlistPivotViewModel item4;

        private readonly INavigationService _navigationService;
        private SetlistDataContext _db;

        private int _setlistId;
        public int SetlistId
        {
            get { return _setlistId; }
            set
            {
                _setlistId = value;
                NotifyOfPropertyChange(() => SetlistId);
            }
        }

        private Setlist _setlist;
        public Setlist Setlist
        {
            get 
            {
                if (_setlist == null)
                {
                    LoadSelectedSetlist();
                }
                return _setlist;
            }
            set
            {
                _setlist = value;
                NotifyOfPropertyChange(() => Setlist);
            }
        }

        //public BindableCollection<Set> Sets
        //{
        //    get
        //    {
        //        new DebugLogger(typeof(SetlistPageViewModel)).Info("Getting Setlist.Sets. Count = {0}", Setlist.Sets.Count);
        //        return Setlist.Sets;
        //    }
        //}

        public string SetlistName
        {
            get 
            {
                if (_setlist == null)
                {
                    LoadSelectedSetlist();
                }
                return _setlist.Name; 
            }
            set
            {
                _setlist.Name = value;
                NotifyOfPropertyChange(() => SetlistName);
            }
        }

        public string SetlistInfo
        {
            get { return _setlist.AdditionalInfo; }
            set
            {
                _setlist.AdditionalInfo = value;
                NotifyOfPropertyChange(() => SetlistInfo);
            }
        }

        #endregion

        public SetlistPageViewModel(INavigationService navigationService, SetlistPivotViewModel item1, SetlistPivotViewModel item2,
            SetlistPivotViewModel item3, SetlistPivotViewModel item4)
        {
            this._navigationService = navigationService;
            _db = new SetlistDataContext();

            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.item1.Set = Setlist.Sets[0];
            this.item2.Set = Setlist.Sets[1];
            this.item3.Set = Setlist.Sets[2];
            this.item4.Set = Setlist.Sets[3];
            Items.Add(item1);
            Items.Add(item2);
            Items.Add(item3);
            Items.Add(item4);

            ActivateItem(item1);
        }

        #region Private Methods

        private void LoadSelectedSetlist()
        {
            try
            {
                var loadedSetlist = _db.Setlists.Single(s => s.SetlistId == SetlistId); 
                Setlist = loadedSetlist;
                return;
            }
            catch
            {
                MessageBox.Show("The setlist could not be loaded. Gutted.\nTry making a new one.");
                _navigationService.GoBack();
            }
        }

        #endregion
    }
}
