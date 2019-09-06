
using Coursework.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Coursework.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get {
                return selectedLanguage;
            }
            set
            {
                if (value != null)
                {
                    if (value == "English") value = "en";
                    if (value == "Русский") value = "ru";
                    selectedLanguage = value;
                    SetLanguage();
                }
            }
        }

        
        public static event EventHandler LanguageChanged;
       

        public SettingsViewModel()
        {
            selectedLanguage = App.CurrentLanguage;
            
        }

       

        private void SetLanguage()
        {
            App.CurrentLanguage = SelectedLanguage;
            AppResources.Culture = new CultureInfo(SelectedLanguage);
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
            LanguageChanged?.Invoke(this, new EventArgs());
        }

        public async Task ClearVisitedAsync()
        {
            await Task.Run( () =>
            {
                DatabaseManager db = new DatabaseManager();
                db.ClearVisited();
            });
        }

        internal async Task ClearFavoriteAsync()
        {
            await Task.Run(() =>
            {
                DatabaseManager db = new DatabaseManager();
                db.ClearFavorite();
            });
        }
    }
}
