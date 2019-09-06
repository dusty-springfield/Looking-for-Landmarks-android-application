using Coursework.DSInterfaces;
using Coursework.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDMaster : ContentPage
    {
        public ListView ListView;

        public MDMaster()
        {
            InitializeComponent();
            exitLabel.Text = AppResources.Exit;
            exitLabel.GestureRecognizers.Add(new TapGestureRecognizer(async (view) => {
                bool answer = await DisplayAlert(AppResources.Alert, AppResources.AreYouSure, AppResources.Yes, AppResources.No);
                if (answer)
                {
                    DependencyService.Get<IExitApplication>().ExitApplication();
                }
            }));
            
            BindingContext = new MasterDetailMainPageMasterViewModel();
            ListView = MenuItemsListView;

            SettingsViewModel.LanguageChanged += SettingsViewModel_LanguageChanged;
        }
#warning "Poor code"
        private void SettingsViewModel_LanguageChanged(object sender, EventArgs e)
        {
            exitLabel.Text = AppResources.Exit;
            BindingContext = new MasterDetailMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MDMenuItem> MenuItems { get; set; }
            
            public MasterDetailMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MDMenuItem>(new[]
                {
                    new MDMenuItem { Id = 0, Icon ="baseline_home_black_24.png", Title = AppResources.MainPage, TargetType=typeof(MainPage)},
                    new MDMenuItem{ Id = 1,Icon="baseline_search_black_24.png", Title = AppResources.Search, TargetType=typeof(SearchPage)},
                    new MDMenuItem{ Id = 2, Icon="baseline_settings_black_24.png", Title = AppResources.Settings, TargetType=typeof(SettingsPage)}, 
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}