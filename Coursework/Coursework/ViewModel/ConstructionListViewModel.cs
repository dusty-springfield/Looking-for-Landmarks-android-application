using Coursework.Exceptions;
using Coursework.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.ViewModel
{
    public abstract class ConstructionListViewModel : BaseViewModel
    {
        /// <summary>
        /// Список всех объектов 
        /// </summary>
        public List<Construction> Constructions { get; set; }

        internal DatabaseManager dbManager;
        
        public ConstructionListViewModel()
        {
            dbManager = new DatabaseManager();
            Constructions = dbManager.GetAllConstructions();
        }
    }
}
