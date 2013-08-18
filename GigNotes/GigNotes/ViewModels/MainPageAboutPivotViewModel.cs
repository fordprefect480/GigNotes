using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace GigNotes.ViewModels
{
    public class MainPageAboutPivotViewModel : Screen
    {
        public string DisplayName
        {
            get { return "about"; }
        }

        public Uri DisplayIcon
        {
            get { return new Uri("/Assets/Images/about.png", UriKind.Relative); }
        }
    }
}
