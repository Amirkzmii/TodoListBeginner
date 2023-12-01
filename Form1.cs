using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoList.Services;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        ITodoRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new TodoRepository();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddOrEdit frm = new frmAddOrEdit();
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {

            }
        }
        private void BindGrid()
        {
            dgTodo.DataSource = repository.SelectAll();
            dgTodo.Columns[0].Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(dgTodo.CurrentRow != null)
            {
                string taskName = dgTodo.CurrentRow.Cells[1].Value.ToString();
                

                if (MessageBox.Show($"Are You Sure To Delete {taskName} Task?","Attention",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    int contactId = int.Parse(dgTodo.CurrentRow.Cells[0].Value.ToString());
                    repository.Delete(contactId);
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("Select A Task!");

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgTodo.CurrentRow != null)
            {
                int todoId = int.Parse(dgTodo.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEdit frm = new frmAddOrEdit();
                frm.todoId = todoId;   
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            dgTodo.DataSource = repository.Search(txtSearch.Text);
        }
    }
}
