using Coursework.DSInterfaces;
using Coursework.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coursework.Model
{
    /// <summary>
    /// Класс, содержащий информацию об конфигурации (настройках) приложения
    /// </summary>
    public static  class ConfigurationManager
    {
       
        public static Configuration Configuration { get; set; }

        static ConfigurationManager()
        {
            Configuration = Xamarin.Forms.DependencyService.Get<IConfiguration>().Load();
        }

        public static async Task SaveConfiguration()
        {
            await Xamarin.Forms.DependencyService.Get<IConfiguration>().Save(Configuration);
        }

    }

    public class Configuration
    {
        [JsonProperty]
        public int MaxSearchResultItem { get; set; }
        [JsonProperty]
        public int ItemsOnPageItem { get; set; }
        [JsonProperty]
        public int MaxSearchResultIndex { get; set; }
        [JsonProperty]
        public int ItemsOnPageIndex { get; set; }
        [JsonProperty]
        public int LanguageIndex { get; set; }
        [JsonProperty]
        public bool ShowVisited { get; set; }
        [JsonIgnore]
        public double UserLtd { get; set; }
        [JsonIgnore]
        public double UserLnt { get; set; }
        [JsonIgnore]
        public Construction SelectedConstruction { get; set; }
        [JsonIgnore]
        public List<Construction> DisplayedConstructions { get; set; }
        [JsonIgnore]
        public string IconNotFavorite  {get;set;}
        [JsonIgnore]
        public string IconFavorite { get; set; }
        [JsonIgnore]
        public string IconNotVisited { get; set; }
        [JsonIgnore]
        public string IconVisited { get; set; }
       
    }
}
