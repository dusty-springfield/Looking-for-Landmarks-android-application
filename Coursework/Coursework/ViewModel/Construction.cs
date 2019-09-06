using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Coursework.ViewModel
{
    public class Construction : IComparable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [Unique]
        public string  Name { get; set; }
        public string  Address { get; set; }
        public string  Image { get; set; }
        public string  Description { get; set; }
        public string Area { get; set; }
        public double Long { get; set; }
        public double Lat{ get; set; }
        public double Distance { get; set; }
        public int isVisited { get; set; }
        public int isFavorite { get; set; }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Construction otherConstruction = obj as Construction;
            if (otherConstruction != null)
                return this.Distance.CompareTo(otherConstruction.Distance);
            else
                throw new ArgumentException("Object is not a Construction");
        }
    }
}
