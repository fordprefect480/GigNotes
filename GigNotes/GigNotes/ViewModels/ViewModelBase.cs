using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigNotes.ViewModels
{
    public class ViewModelBase : Screen
    {
        private static bool? isInDesignMode;
        public static bool IsInDesignMode
        {
            get
            {
                if (!isInDesignMode.HasValue)
                {
                    isInDesignMode = Execute.InDesignMode;
                }
                return isInDesignMode.Value;
            }
        }

    }
}
