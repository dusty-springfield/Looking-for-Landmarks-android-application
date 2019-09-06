using Coursework.Model;
using Coursework.View;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Coursework
{
    public partial class App : Application
    {
        
        public static string CurrentLanguage;

        public static int ScreenHeight;
        public static int ScreenWidth;

        public App()
        {
            
            
            Page loadingPage = new LoadingPage();
            MainPage = loadingPage;
            Task startupWork = new Task(() => { StartLoading(); });
            startupWork.Start();
        }

       
    

    private async void StartLoading()
        {
            if (ConfigurationManager.Configuration.LanguageIndex == 0)
            {
                CurrentLanguage = "en";
            }
            else
            {
                CurrentLanguage = "ru";
            }
            AppResources.Culture = new CultureInfo(CurrentLanguage);
            InitializeComponent();
            ConfigurationManager.Configuration.IconNotFavorite = "baseline_star_border_white_24.png";
            ConfigurationManager.Configuration.IconFavorite = "baseline_star_white_24.png";
            ConfigurationManager.Configuration.IconNotVisited = "baseline_done_outline_white_24.png";
            ConfigurationManager.Configuration.IconVisited = "baseline_done_all_white_24.png";
            
          Page main = new NavigationPage(new MDMain());
           
            MainPage = main;
           
        }

       

        public static bool CheckConnection()
        {
            bool res = Connectivity.NetworkAccess == NetworkAccess.Internet;
            return res;
        }
    }
}
