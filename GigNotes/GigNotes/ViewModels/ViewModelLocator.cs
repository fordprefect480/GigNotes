using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;

namespace GigNotes.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static MainViewModel _main;
        private static SetlistViewModel _setlist;
        
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SetlistViewModel Setlist
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SetlistViewModel>();
            }
        }

        public static void Cleanup()
        {
            _main.Cleanup();
            _setlist.Cleanup();
            _main = null;
            _setlist = null;
        }       
  
        public event PropertyChangedEventHandler PropertyChanged = delegate { };    
    }
}