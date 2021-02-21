using ProfileBook.Models;
using SQLite;
using System.Collections.Generic;

namespace ProfileBook.Services.Repository
{
    public class Repository
    {
        SQLiteConnection database;
        public Repository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            //database.CreateTable<Registrate>();
            database.CreateTable<User>();
        }
        public IEnumerable<User> GetItems()
        {
            return database.Table<User>().ToList();
        }
        public User GetItem(int id)
        {
            return database.Get<User>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<User>(id);
        }
        public int SaveItem(User item)
        {
            return database.Insert(item);
        }
    }
}
