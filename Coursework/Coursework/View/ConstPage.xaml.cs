using Coursework.DSInterfaces;
using Coursework.Model;
using Coursework.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConstPage : ContentPage
    {
        /// <summary>
        /// Отображаемое Строение
        /// </summary>
        public Construction Item { get; set; }
        /// <summary>
        /// Путь к иконке, отображающей статус "Избранное"
        /// </summary>
        private string favorite;
        /// <summary>
        /// Путь к иконке, отображающей статус "Посещенное"
        /// </summary>
        private string visited;
        private DatabaseManager db;
        /// <summary>
        /// Состояние, из которого открыта старница Строения
        /// </summary>
        private readonly PageMode pageMode;

        public event EventHandler OnDeletingItemFromCategory;
        public event EventHandler OnVisitedChangedWhileNotShowVisited;


        public ConstPage(Construction item, PageMode pageMode)
        {

            InitializeComponent();
            Item = item;
            Title = Item.Name;
            db = new DatabaseManager();
            ConfigurationManager.Configuration.SelectedConstruction = item;
            this.pageMode = pageMode;
            constImage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));

            favorite = item.isFavorite == 0 ? ConfigurationManager.Configuration.IconNotFavorite : ConfigurationManager.Configuration.IconFavorite;
            visited = item.isVisited == 0 ? ConfigurationManager.Configuration.IconNotVisited : ConfigurationManager.Configuration.IconVisited;

            BindingContext = new { Image = item.Image, Description = item.Description, OnMap = AppResources.OnMapButton };

            AddToolbarItems();
        }

        private void OnTap(Xamarin.Forms.View arg1, object arg2)
        {
            Navigation.PushAsync(new ImageViewer(Item));
        }

        /// <summary>
        /// Добавляет на верхнюю панель элементы, оторбражающие статус "Избранное", "Посещенное"
        /// </summary>
        private void AddToolbarItems()
        {
            int t = ToolbarItems.Count;

            ToolbarItem tbVisited = new ToolbarItem()
            {
                Icon = visited,
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

            ToolbarItems.Add(tbVisited);
            tbVisited.Clicked += isVisitedChanged;
            ToolbarItem tbFavorite = new ToolbarItem()
            {
                Icon = favorite,
                Order = ToolbarItemOrder.Primary,
                Priority = 1
            };

            ToolbarItems.Add(tbFavorite);
            tbFavorite.Clicked += isFavoriteChanged;
            t = ToolbarItems.Count;
        }

        /// <summary>
        /// Открывает Adroid activity, для отображения Строения на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowOnMapClicked(object sender, EventArgs e)
        {
            if (App.CheckConnection())
            {
                DependencyService.Register<IMap>();
                DependencyService.Get<IMap>().DisplaySelectedConstructionOnMap();
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(AppResources.NoInternet);
            }
        }

        /// <summary>
        /// Меняет иконку и статус "Избранное" Строения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isFavoriteChanged(object sender, EventArgs e)
        {
            if (Item.isFavorite == 0)
            {
                Item.isFavorite = 1;
                favorite = ConfigurationManager.Configuration.IconFavorite;
                DependencyService.Get<IMessage>().ShortAlert(AppResources.AddedToFavorite);
            }
            else
            {
                Item.isFavorite = 0;
                favorite = ConfigurationManager.Configuration.IconNotFavorite;
                DependencyService.Get<IMessage>().ShortAlert(AppResources.DeletedFromFavorite);

                if (pageMode == PageMode.Favorite)
                {
                    OnDeletingItemFromCategory?.Invoke(Item, new EventArgs());
                }

            }
            db.UpdateConstructionInfo(Item);
            ToolbarItems.Clear();
            AddToolbarItems();



        }

        /// <summary>
        /// Меняет иконку и статус "Посещенное" Строения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isVisitedChanged(object sender, EventArgs e)
        {

           
            if (Item.isVisited == 0)
            {
                Item.isVisited = 1;
                visited = ConfigurationManager.Configuration.IconVisited;
                DependencyService.Get<IMessage>().ShortAlert(AppResources.AddedToVisited);

            }
            else
            {
                Item.isVisited = 0;
                visited = ConfigurationManager.Configuration.IconNotVisited;
                DependencyService.Get<IMessage>().ShortAlert(AppResources.DeletedFromVisited);
                if (pageMode == PageMode.Visited)
                {
                    OnDeletingItemFromCategory?.Invoke(Item, new EventArgs());
                }
            }
            db.UpdateConstructionInfo(Item);
            ToolbarItems.Clear();
            AddToolbarItems();
            if (!ConfigurationManager.Configuration.ShowVisited)
            {
                OnVisitedChangedWhileNotShowVisited?.Invoke(Item, new EventArgs());
            }

        }
    }
}