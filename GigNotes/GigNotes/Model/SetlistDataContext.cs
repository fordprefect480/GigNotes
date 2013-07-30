using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace GigNotes.Model
{
    public interface ISetlistDataContext { }

    public class SetlistDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public SetlistDataContext()
            : base(App.DbConnection)
        { }
        
        public Table<Setlist> Setlists;
    }

    [Table]
    public class Setlist
    {
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // Define ID: private field, public property, and database column.
        private int _setlistId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int SetlistId
        {
            get { return _setlistId; }
            set
            {
                if (_setlistId != value)
                {
                    NotifyPropertyChanging("SetlistId");
                    _setlistId = value;
                    NotifyPropertyChanged("SetlistId");
                }
            }
        }

        private string _name;

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private DateTime? _gigDate;
        
        [Column(CanBeNull = true)]
        public DateTime? GigDate
        {
            get { return _gigDate; }
            set
            {
                if (_gigDate != value)
                {
                    NotifyPropertyChanging("GigDate");
                    _gigDate = value;
                    NotifyPropertyChanged("GigDate");
                }
            }
        }

        public string GigDateFormatted
        {
            get { return GigDate.Value.ToString("dd/MM/yyyy"); }
        }

        private string _additionalInfo;

        [Column(CanBeNull = true)]
        public string AdditionalInfo
        {
            get { return _additionalInfo; }
            set
            {
                if (_additionalInfo != value)
                {
                    NotifyPropertyChanging("AdditionalInfo");
                    _additionalInfo = value;
                    NotifyPropertyChanged("AdditionalInfo");
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    NotifyPropertyChanging("IsSelected");
                    _isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        //private List<Set> _sets;

        //public List<Set> Sets
        //{
        //    get 
        //    {
        //        if (_sets == null)
        //            _sets = new List<Set>();

        //        return _sets; 
        //    }
        //    private set {}
        //}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }


}
