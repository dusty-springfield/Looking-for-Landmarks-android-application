using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.DSInterfaces
{
    public interface IDatabase
    {

        SQLiteConnection CreateConnection();

    }
}
