using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    internal interface ITodoRepository
    {
        DataTable SelectAll();
        DataTable SelectRow(int todoId);
        DataTable Search(string parameter);
        bool Add(string name, string time, string from, string to,string session, string details);
        bool Edit(int todoId, string name, string time, string from, string to,string session, string details);
        bool Delete(int todoId);
    }
}
