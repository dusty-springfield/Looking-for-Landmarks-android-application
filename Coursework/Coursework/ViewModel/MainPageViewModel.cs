
using Coursework.Exceptions;
using Coursework.Model;
using Plugin.Geolocator.Abstractions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Coursework.ViewModel
{
    /// <summary>
    /// Класс для управления данными на странице со списком Строений
    /// </summary>
    public class MainPageViewModel : ConstructionListViewModel
    {
        public MainPageViewModel() : base() { }

        /// <summary>
        /// Метод формирует отсортированную коллекцию из объектов для дальнейшего отображения
        /// </summary>
        /// <returns>Коллекция для отображения</returns>
        public ObservableCollection<Construction> GetNearestConstrucions()
        {
            GeoLocation locator = new GeoLocation();
            if (!locator.IsLocationAvailable())
            {
                throw new GeoLocationIsNotAvaible();
            }

            Task<Position> task = locator.GetCurrentPosition();
            task.Wait();
            Position pos = task.Result;
            if (pos == null)
            {
                throw new GeoLocationIsNotEnabled();
            }

            ConfigurationManager.Configuration.UserLtd = pos.Latitude;
            ConfigurationManager.Configuration.UserLnt = pos.Longitude;

            ObservableCollection<Construction> res = new ObservableCollection<Construction>();
            for (int i = 0; i < Constructions.Count; i++)
            {
                Construction c = Constructions[i];
                c.Distance = 
                c.Distance = DistanceCalculator.Distance(ConfigurationManager.Configuration.UserLnt,
                     ConfigurationManager.Configuration.UserLtd = pos.Latitude,
                                            c.Long, c.Lat);

            }
            Constructions.Sort();

            for (int i = 0; i < Constructions.Count; i++)
            {
                Construction c = Constructions[i];
                if (!(!ConfigurationManager.Configuration.ShowVisited && c.isVisited == 1))
                {
                    res.Add(c);
                }
            }

            return res;
        }

        /// <summary>
        /// Метод возвращает список избранных Строений
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Construction> GetFavoriteConstrucions()
        {
            ObservableCollection<Construction> res = new ObservableCollection<Construction>();

            for (int i = 0; i < Constructions.Count; i++)
            {
                Construction c = Constructions[i];
                if (c.isFavorite != 0) res.Add(c);
            }

            return res;
        }

        /// <summary>
        /// Метод возвращает список посещенных Строений
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Construction> GetVisitedConstrucions()
        {

            ObservableCollection<Construction> res = new ObservableCollection<Construction>();

            for (int i = 0; i < Constructions.Count; i++)
            {
                Construction c = Constructions[i];
                if (c.isVisited != 0) res.Add(c);
            }
            return res;
        }
    }
}
