using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using GigNotes.Model;

namespace GigNotes.ViewModels
{
    public class NewSetlistPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private SetlistDataContext _db;

        public NewSetlistPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            _db = new SetlistDataContext();

            GigDate = DateTime.Now;
            SetUpAppBar();            
        }

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

        #region View Controls

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private DateTime _gigDate;
        public DateTime GigDate
        {
            get { return _gigDate; }
            set
            {
                _gigDate = value;
                NotifyOfPropertyChange(() => GigDate);
            }
        }

        private string _additionalInfo;
        public string AdditionalInfo
        {
            get { return _additionalInfo; }
            set
            {
                _additionalInfo = value;
                NotifyOfPropertyChange(() => AdditionalInfo);
            }
        }

        #endregion

        // Add a setlist to the database and collections.
        public void AddSetlist()
        {
            if (Name.Length > 0)
            {
                // Create a new setlist.
                Setlist newSetlist = new Setlist
                {
                    Name = Name,
                    GigDate = GigDate,
                    AdditionalInfo = AdditionalInfo
                };

                // Add a setlist to the data context.
                _db.Setlists.InsertOnSubmit(newSetlist);

                // Save changes to the database.
                _db.SubmitChanges();
            }
            else
            {
                throw new ArgumentNullException("Gig must be given a name.");
            }

            // Add a setlist to the "all" observable collection.
            //_db.Add(newSetlist);

            _navigationService.UriFor<MainPageViewModel>().Navigate();
        }

        public void Cancel()
        {
            // Return to the main page.
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
        }
    }
}
