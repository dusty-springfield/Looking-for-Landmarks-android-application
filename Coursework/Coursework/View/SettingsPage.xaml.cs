
using Coursework.Model;
using Coursework.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
       
        private SettingsViewModel vm ;
        private bool isAppearing;
        public SettingsPage()
        {
            isAppearing = true;
            vm = new SettingsViewModel();
            BindingContext = vm;
            InitializeComponent();
            languagePicker.ItemsSource = new ObservableCollection<string>()
            {
                 "English", "Русский"
            };
                        
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(() =>
            {
                itemsOnPagePicker.SelectedIndex = ConfigurationManager.Configuration.ItemsOnPageIndex;
                maxSearchResultPicker.SelectedIndex = ConfigurationManager.Configuration.MaxSearchResultIndex;
                showVisitedSwitchCell.On = ConfigurationManager.Configuration.ShowVisited;
                languagePicker.SelectedIndex = ConfigurationManager.Configuration.LanguageIndex;
                isAppearing = false;
            });
        }


        private async void ShowVisitedSwitchCell_OnChanged(object sender, EventArgs e) {
            if (!isAppearing)
            {
                ConfigurationManager.Configuration.ShowVisited = ((SwitchCell)sender).On;
                await ConfigurationManager.SaveConfiguration();
            }
            }
        private async void MaxSearchResultPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isAppearing)
            {
                CustomPicker picker = (CustomPicker)sender;
                if (picker.SelectedIndex > 1)
                {
                    DependencyService.Get<DSInterfaces.IMessage>().LongAlert(AppResources.BigValueAlarm);
                }

                ConfigurationManager.Configuration.MaxSearchResultItem = int.Parse(picker.SelectedItem.ToString());
                ConfigurationManager.Configuration.MaxSearchResultIndex = picker.SelectedIndex;
                await ConfigurationManager.SaveConfiguration();
            }

        }

        private async void ItemsOnPagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isAppearing)
            {
                CustomPicker picker = (CustomPicker)sender;
                ConfigurationManager.Configuration.ItemsOnPageItem = int.Parse(picker.SelectedItem.ToString());
                ConfigurationManager.Configuration.ItemsOnPageIndex = picker.SelectedIndex;
                await ConfigurationManager.SaveConfiguration();
            }
        }

        private async void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isAppearing)
            {
                CustomPicker picker = (CustomPicker)sender;
                vm.SelectedLanguage = picker.SelectedItem.ToString();
                ConfigurationManager.Configuration.LanguageIndex = picker.SelectedIndex;
                await ConfigurationManager.SaveConfiguration();

            }
        }

        private async void OnClearFavorite(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert(AppResources.Alert, AppResources.AreYouSure, AppResources.Yes, AppResources.No);
            if (answer)
            {
                await vm.ClearFavoriteAsync();
                DependencyService.Get<DSInterfaces.IMessage>().ShortAlert(AppResources.FavoriteCleared);

            }
        }

        private async void OnClearVisited(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert(AppResources.Alert, AppResources.AreYouSure, AppResources.Yes, AppResources.No);
            if (answer)
            {
                await vm.ClearVisitedAsync();
                DependencyService.Get<DSInterfaces.IMessage>().ShortAlert(AppResources.VisitedCleared);
            }
        }
    }
}