using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

using System.Diagnostics;

namespace ToDoList.Services
{
    internal class TodoRepository : ITodoRepository
    {
        private string connectionString = @"Data Source=AMIR\SQL2019;Initial Catalog=ToDoDB;Integrated Security=true";
        public bool Add(string name, string time, string from, string to, string session, string details)
        {
                SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "Insert Into ToDo_table(Name,Time,[From],[To],Session,Details) values (@Name,@Time,@From,@To,@Session,@Details)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Time", time);
                command.Parameters.AddWithValue("@From", from);
                command.Parameters.AddWithValue("@To", to);
                command.Parameters.AddWithValue("@Session", session);
                command.Parameters.AddWithValue("@Details", details);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Insert Error");
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public bool Delete(int todoId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "Delete From ToDo_table Where TodoID=@ID";
                SqlCommand command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@ID", todoId);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Delete Error");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        

        public bool Edit(int todoId, string name, string time, string from, string to, string session, string details)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "Update ToDo_table Set Name=@Name,Time=@Time,[From]=@From,[To]=@To,Session=@Session,Details=@Details Where TodoID=@TodoID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ToDoID", todoId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Time", time);
                command.Parameters.AddWithValue("@From", from);
                command.Parameters.AddWithValue("@To", to);
                command.Parameters.AddWithValue("@Session", session);
                command.Parameters.AddWithValue("@Details", details);
                connection.Open();
                command.ExecuteNonQuery();
                
                return true;
            }
            catch
            {
                MessageBox.Show("Edit Error");
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public DataTable Search(string parameter)
        {
            string query = "Select * From ToDo_table Where Name Like @parameter";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter", "%" + parameter + "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectAll()
        {
            string query = "Select * From ToDo_table";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectRow(int todoId)
        {
            string query = "Select * From ToDo_table Where TodoID="+todoId;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
    }
}
