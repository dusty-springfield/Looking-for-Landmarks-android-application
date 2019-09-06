using Coursework.DSInterfaces;
using Coursework.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private ObservableCollection<Construction> items;
        private SearchPageViewModel vm;

        public SearchPage()
        {
            InitializeComponent();
            if (App.CheckConnection())
            {
                
                vm = new SearchPageViewModel();
                BindingContext = vm;
                items = vm.GetConstructions();
                constView.ItemsSource = items;
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {

            constView.ItemsSource = items;
            entryName.Text = "";
            entryAddress.Text = "";
            emptyLabel.HeightRequest = 0;
            emptyLabel.IsVisible = false;

        }

        private void Search_Clicked(object sender, EventArgs e)
        {
            if (App.CheckConnection())
            {
                if (string.Compare(entryName.Text, null) != 0 && string.Compare(entryName.Text.Trim(), "") == 0 &&
                    string.Compare(entryAddress.Text, null) != 0 &&
                    string.Compare(entryAddress.Text.Trim(), "") == 0)
                {
                    DependencyService.Get<IMessage>().LongAlert(AppResources.EmptySearch);
                    return;
                }


                string name = "";
                string address = "";
                if (entryName.Text != null) name = entryName.Text;
                if (entryAddress.Text != null) address = entryAddress.Text;

                var match1 = Regex.Match(name, @"[^a-z0-9а-я \-/]+", RegexOptions.IgnoreCase);
                var match2 = Regex.Match(address, @"[^a-z0-9а-я \-/]+", RegexOptions.IgnoreCase);

                if (match1.Success || match2.Success)
                {
                    DependencyService.Get<IMessage>().LongAlert(AppResources.OnlyLettersAndNum);
                    return;
                }

                ObservableCollection<Construction> searchResult = vm.GetSearchResult(name, address);
                constView.ItemsSource = searchResult;

                if (searchResult.Count == 0)
                {
                    emptyLabel.Text = AppResources.Empty;
                    emptyLabel.HeightRequest = 150;
                    emptyLabel.IsVisible = true;
                }
                else
                {
                    emptyLabel.HeightRequest = 0;
                    emptyLabel.IsVisible = false;
                }
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        private void ConstListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var item = e.SelectedItem as Construction;
            constView.SelectedItem = null;
            ConstPage constPage = new ConstPage(item, PageMode.Nearest);

            Navigation.PushAsync(constPage);
        }
    }
}