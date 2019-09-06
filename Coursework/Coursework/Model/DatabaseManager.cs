using Coursework.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;

using Coursework.DSInterfaces;

namespace Coursework.Model
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    public class DatabaseManager 
    {

        private SQLiteConnection dbConnection;

        public DatabaseManager(){ }

        /// <summary>
        /// Метод возвращает список всех объектов из базы данных
        /// </summary>
        /// <returns></returns>
        public List<Construction> GetAllConstructions()
        {
            dbConnection = GetConnection();
            string table = App.CurrentLanguage == "en" ? "constructions_description_en" : "constructions_description_ru";
            List<Construction> constructions = dbConnection.Query<Construction>(string.Format("Select * From [{0}] inner join [constructions_data] ON {0}.Id=[constructions_data].Id", table));
            dbConnection.Close();
            return constructions;

        }

        /// <summary>
        /// Метод добавляет или изменяет уже сущетсвующий объект в базе данных
        /// </summary>
        /// <param name="constructions">Объект для добавления/изменения</param>
        /// <returns></returns>
        public void UpdateConstructionInfo(Construction construction)
        {
            dbConnection = GetConnection();
            string query = string.Format(
                "UPDATE [constructions_data] " +
                "SET IsVisited = {0}, isFavorite = {1} " +
                "WHERE Id = {2};", construction.isVisited, construction.isFavorite, construction.Id);
           var cc = dbConnection.Query<Construction>(query);
            dbConnection.Close();
        }

        public SQLiteConnection GetConnection()
        {
            return Xamarin.Forms.DependencyService.Get<IDatabase>().CreateConnection();
        }

        public void ClearVisited()
        {
            dbConnection = GetConnection();
            dbConnection.Query<Construction>(string.Format(
                "UPDATE [constructions_data] " +
                "SET isVisited = {0};", 0));
            dbConnection.Close();
        }

        public void ClearFavorite()
        {
            dbConnection = GetConnection();
            dbConnection.Query<Construction>(string.Format(
                "UPDATE [constructions_data] " +
                "SET isFavorite = {0} ;", 0));
            dbConnection.Close();
        }

    }
}
