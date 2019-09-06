using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Coursework.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public LocalizedResources Resources
        {
            get;
            private set;
        }

        public BaseViewModel()
        {
            Resources = new LocalizedResources(typeof(AppResources), App.CurrentLanguage);
        }

        public void OnPropertyChanged([CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}


