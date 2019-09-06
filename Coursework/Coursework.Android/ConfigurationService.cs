using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Coursework.DSInterfaces;
using Coursework.Model;

using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Coursework.Droid.ConfigurationService))]
namespace Coursework.Droid
{
    class ConfigurationService : IConfiguration
    {
        public Configuration Load()
        {
            var fileName = "config.json";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, fileName);

            if (!File.Exists(path))
            {
                Configuration conf = new Configuration() {
                    ItemsOnPageItem = 4,
                    MaxSearchResultItem = 10,
                    ItemsOnPageIndex = 0,
                    MaxSearchResultIndex = 0,
                    LanguageIndex = 0,
                    ShowVisited = false
                };

                return conf;
            }
            else
            {
                using(StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    string js = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<Configuration>(js);
                }

            }
            
        }

        public async Task Save(Configuration conf)
        {
            var fileName = "config.json";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, fileName);

            if (conf != null)
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create)))
                {
                    string js = JsonConvert.SerializeObject(conf);
                    await sw.WriteAsync(js);
                }
            }

        }
    }
}