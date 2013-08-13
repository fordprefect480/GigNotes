using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using GigNotes.Model;

namespace GigNotes.ViewModels
{
    public class SetlistPivotViewModel : Screen
    {
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

        public BindableCollection<Song> Songs
        {
            get { return _set.Songs; }
            private set { }
        }

        //private string _setName;
        public string SetName
        {
            get { return _set.SetName; }
            private set{}
        }
    }
}
