using Coursework.DSInterfaces;
using Coursework.Model;
using Coursework.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        /// <summary>
        /// Список выбранныйх из базы данных Строений
        /// </summary>
        private ObservableCollection<Construction> items;
        /// <summary>
        /// Список отображаемых Строений
        /// </summary>
        private ObservableCollection<Construction> itemsCurr;
        private MainPageViewModel vm;
        /// <summary>
        /// Отображает состояние в котором находится страница
        /// </summary>
        public PageMode Mode { get; private set; }


        public MainPage()
        {
            vm = new MainPageViewModel();
            BindingContext = vm;
            InitializeComponent();
            Mode = PageMode.Nearest;
            ShowNearest(this, new EventArgs());
        }

        /// <summary>
        /// Метод открывает страницу объекта при его выборе в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConstListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var item = e.SelectedItem as Construction;
            constView.SelectedItem = null;
            ConstPage constPage = new ConstPage(item, Mode);
            constPage.OnDeletingItemFromCategory += ((i, arg) =>
            {
                Construction c = (Construction)i;
                itemsCurr.Remove(c);
            });
            constPage.OnVisitedChangedWhileNotShowVisited += (i, arg) =>
             {

                 Construction c = (Construction)i;
                 if (c.isVisited == 1)
                 {
                     if (Mode == PageMode.Nearest) itemsCurr.Remove(c);
                     items.Remove(c);
                 }
                 else
                 {
                     if (!items.Contains(c))
                     {
                         items.Add(c);
                         items.OrderBy((j) => j);
                     }
                     if(Mode == PageMode.Nearest && !itemsCurr.Contains(c))
                     {
                         itemsCurr.Add(c);
                         itemsCurr.OrderBy((j) => j);
                     }

                 }
             };
            Navigation.PushAsync(constPage);
        }

        /// <summary>
        /// Метод добавляет фиксированное число Строений к отображаемым
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreBtn_Clicked(object sender, EventArgs e)
        {
            Task<bool> connTask = Task.Run(() => App.CheckConnection());
            connTask.Wait();
            if (connTask.Result)
            {
                for (int i = 0; i < ConfigurationManager.Configuration.ItemsOnPageItem; i++)
                {
                    try
                    {
                        Construction c = items.ElementAt(itemsCurr.Count);
                        if (!(!ConfigurationManager.Configuration.ShowVisited && c.isVisited == 1))
                        {
                            itemsCurr.Add(c);
                        }
                    }
                    catch (ArgumentOutOfRangeException) { }
                    catch (Exception) { }
                }
                if (itemsCurr.Count == items.Count) btnMore.IsEnabled = false;
                ConfigurationManager.Configuration.DisplayedConstructions= itemsCurr.ToList();
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        /// <summary>
        /// Обновление страницы со списком ближайщих Строений
        /// </summary>
        public void Refresh()
        {
            Task<bool> connTask = Task.Run(() => App.CheckConnection());
                itemsCurr = new ObservableCollection<Construction>();
            connTask.Wait();
            if (connTask.Result)
            {
                btnMore.IsEnabled = true;
                items = vm.GetNearestConstrucions();
                MoreBtn_Clicked(this, new EventArgs());
                constView.ItemsSource = itemsCurr;
                constView.EndRefresh();
                ConfigurationManager.Configuration.DisplayedConstructions= itemsCurr.ToList();
            }
            else
            {
                constView.EndRefresh();
                constView.ItemsSource = null;
                btnMore.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        /// <summary>
        /// Метод открывает Android activity для показа отображаемых строений на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOnMap_Clicked(object sender, EventArgs e)
        {
            Task<bool> connTask = Task.Run(() => App.CheckConnection());
            connTask.Wait();
            if (connTask.Result)
            {
                DependencyService.Register<IMap>();
                DependencyService.Get<IMap>().DisplaySearchResultOnMap();
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        /// <summary>
        /// Метод отображает список Посещенных строений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowVisited(object sender, EventArgs e)
        {
            Task<bool> connTask = Task.Run(() => App.CheckConnection());
            constView.IsPullToRefreshEnabled = false;
            itemsCurr = new ObservableCollection<Construction>();
            Title = AppResources.Visited;
            Mode = PageMode.Visited;
            btnMore.IsVisible = false;
            connTask.Wait();
            if (connTask.Result)
            {
                items = vm.GetVisitedConstrucions();
                itemsCurr = new ObservableCollection<Construction>(items);
                constView.ItemsSource = itemsCurr;
                ConfigurationManager.Configuration.DisplayedConstructions= itemsCurr.ToList();
            }
            else
            {
                constView.ItemsSource = null;
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        /// <summary>
        /// Метод отображает список избранных Строений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowFavorite(object sender, EventArgs e)
        {
            Task<bool> connTask = Task.Run(() => App.CheckConnection());
            constView.IsPullToRefreshEnabled = false;
            itemsCurr = new ObservableCollection<Construction>();
            Title = AppResources.Favorite;
            Mode = PageMode.Favorite;
            btnMore.IsVisible = false;
            connTask.Wait();
            if (connTask.Result)
            {
                items = vm.GetFavoriteConstrucions();
                itemsCurr = new ObservableCollection<Construction>(items);
                constView.ItemsSource = itemsCurr;
                ConfigurationManager.Configuration.DisplayedConstructions= itemsCurr.ToList();
            }
            else
            {
                constView.ItemsSource = null;
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        /// <summary>
        /// Метод отображает список ближащийх Строений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNearest(object sender, EventArgs e)
        {
            Task<bool> connTask = Task.Run(() => App.CheckConnection());
            Title = AppResources.Nearest;
            itemsCurr = new ObservableCollection<Construction>();
            Mode = PageMode.Nearest;
            constView.IsPullToRefreshEnabled = true;
            connTask.Wait();
            if (connTask.Result)
            {
                try
                {
                    items = vm.GetNearestConstrucions();
                    if (items == null || items.Count == 0) btnMore.IsVisible = false;
                    MoreBtn_Clicked(this, new EventArgs());
                    constView.ItemsSource = itemsCurr;
                    constView.RefreshCommand = new Command(Refresh);

                    ConfigurationManager.Configuration.DisplayedConstructions= itemsCurr.ToList();
                    btnMore.IsVisible = true;
                    btnMore.IsEnabled = true;
                }
                catch (Exceptions.GeoLocationIsNotEnabled)
                {
                    constView.ItemsSource = null;
                    DisplayAlert("Error", "No geodata", "Ok");
                }
                catch (Exception)
                {
                    constView.ItemsSource = null;
                }
            }
            else
            {
                constView.ItemsSource = null;
                btnMore.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

    }
}