using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Model
{
    /// <summary>
    /// Класс для рассчет расстояния между двумя точками на основе их широты и долготы
    /// </summary>
    public static class DistanceCalculator
    {
        /// <summary>
        /// Возращает расстояние между двумя объектами, заданными широтой и долготой
        /// </summary>
        /// <param name="objLong1">Долгота первого объекта</param>
        /// <param name="objLat1">Широта первого объекта</param>
        /// <param name="objLong2">Долгота второго объекта</param>
        /// <param name="objLat2">Широта второго объекта</param>
        /// <returns></returns>
        public static double Distance(double objLong1, double objLat1, double objLong2, double objLat2)
        {
            double R = 6371;
            double phi1 = objLat1 * (Math.PI / 180);
            double phi2 = objLat2 * (Math.PI / 180);
            double dphi = Math.Abs((objLat1-objLat2)) * (Math.PI / 180);
            double dtheta = Math.Abs(objLong1-objLong2) * (Math.PI / 180);

            double a = Math.Pow(Math.Sin(dphi / 2), 2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Pow(Math.Sin(dtheta / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
                
        }
    }
}
