using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Caliburn.Micro;

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

        public Table<Song> Repertoire;
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

        private BindableCollection<Set> _sets;
        public BindableCollection<Set> Sets
        {
            get
            {
                if (_sets == null)
                {
                    _sets = new BindableCollection<Set>();
                    /* TEST DATA */
                    var newSet = new Set();
                    newSet.Order = 1;
                    _sets.Add(newSet);
                    newSet = new Set();
                    newSet.Order = 2;
                    _sets.Add(newSet); 
                    newSet = new Set();
                    newSet.Order = 3;
                    _sets.Add(newSet);
                    newSet = new Set();
                    newSet.Order = 4;
                    _sets.Add(newSet);
                }

                return _sets;
            }
            private set { }
        }

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

    [Table]
    public class Set
    {
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // Define ID: private field, public property, and database column.
        private int _setId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int SetId
        {
            get { return _setId; }
            set
            {
                if (_setId != value)
                {
                    NotifyPropertyChanging("SetId");
                    _setId = value;
                    NotifyPropertyChanged("SetId");
                }
            }
        }

        private int _order;

        [Column]
        public int Order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    NotifyPropertyChanging("Order");
                    _order = value;
                    NotifyPropertyChanged("Order");
                }
            }
        }

        public string SetName
        {
            get { return "set " + Order; }
        }

        private BindableCollection<Song> _songs;
        public BindableCollection<Song> Songs
        {
            get
            {
                if (_songs == null)
                {
                    _songs = new BindableCollection<Song>();
                    /* TEST DATA */
                    var s = new Song() { SongId = 1, Title = "April Sun in Cuba" };
                    var s2 = new Song() { SongId = 2, Title = "Thriller" };
                    _songs.Add(s);
                    _songs.Add(s2);

                }
                return _songs;
            }
            private set { }
        }

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

    [Table]
    public class Song
    {
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // Define ID: private field, public property, and database column.
        private int _songId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int SongId
        {
            get { return _songId; }
            set
            {
                if (_songId != value)
                {
                    NotifyPropertyChanging("SongId");
                    _songId = value;
                    NotifyPropertyChanged("SongId");
                }
            }
        }

        private string _title;

        [Column]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    NotifyPropertyChanging("Title");
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

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
