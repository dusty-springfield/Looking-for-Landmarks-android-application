using Coursework.Exceptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Coursework.ViewModel
{
    internal class SearchPageViewModel : ConstructionListViewModel
    {
        public SearchPageViewModel() : base()
        {
           
        }

        public ObservableCollection<Construction> GetConstructions()
        {
            return new ObservableCollection<Construction>(Constructions);
        }

        public ObservableCollection<Construction> GetSearchResult(string name, string address)
        {

            name = name.Trim();
            address = address.Trim();
            address = Regex.Replace(address, @"\s+", " ");
            address = Regex.Replace(address, ",", "");
            name = Regex.Replace(name, @"\s+", " ");
            name = Regex.Replace(name, ",", "");

            List<Construction> searchResult = new List<Construction>();

            if (name != "" && address == "")
            {
                searchResult = SearchByName(name, Constructions);
            }
            else if (name == "" && address != "")
            {
                searchResult = SearchByAddres(address, Constructions);
            }
            else if(name != "" && address != "")
            {
                List<Construction> temp = new List<Construction>();
                temp = SearchByName(name, Constructions);
                if (temp.Count > 0)
                {
                    searchResult = SearchByAddres(address, temp);
                }
            }
            return new ObservableCollection<Construction>(searchResult);
# warning "Memory exception"
            
        }

        private List<Construction> SearchByAddres(string address, List<Construction> collection)
        {
            List<Construction> searchResult = new List<Construction>();
            string[] addressPatterns = address.Split(' ');

            foreach (Construction i in collection)
            {
                bool matches = addressPatterns.All(p => isMatches(p, i.Address));
                if (matches) searchResult.Add(i);
            }

            return searchResult;
        }
        private List<Construction> SearchByName(string name, List<Construction> collcetion)
        {
            List<Construction> searchResult = new List<Construction>();
            string[] namePatterns = name.Split(' ');
           
                foreach (Construction i in collcetion)
                {
                    bool matches = namePatterns.All((p) => isMatches(p, i.Name));
                    if (matches) searchResult.Add(i);
                }
            return searchResult;
        }
        private bool isMatches(string pattern, string source)
        {
            //^ ba |\bba
            pattern = string.Format(@"^{0}|\b{0}", pattern);
            return Regex.Match(source, pattern, RegexOptions.IgnoreCase).Success;
        }
    }
}
