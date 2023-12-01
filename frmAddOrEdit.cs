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
    public partial class frmAddOrEdit : Form
    {
        ITodoRepository repository;
        public int todoId = 0;
        public frmAddOrEdit()
        {
            InitializeComponent();
            repository = new TodoRepository();
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            if(todoId == 0)
            {
                this.Text = "Add New";
            } 
            else
            {
                this.Text = "Edit Task";
                DataTable dt = repository.SelectRow(todoId);
                txtName.Text = dt.Rows[0][1].ToString();
                txtTime.Text = dt.Rows[0][2].ToString();
                txtFrom.Text = dt.Rows[0][3].ToString();
                txtTo.Text = dt.Rows[0][4].ToString();
                txtSession.Text = dt.Rows[0][5].ToString();
                txtDetails.Text = dt.Rows[0][6].ToString();

            }
        }
        bool InputValidation()
        {
            

            if(txtName.Text == "")
            {
                MessageBox.Show("Enter Task Name!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            if (txtTime.Text == "")
            {
                MessageBox.Show("Enter Time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFrom.Text == "")
            {
                MessageBox.Show("Enter Start Time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtTo.Text == "")
            {
                MessageBox.Show("Enter Finishing Time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            

            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (InputValidation())
            {
                bool isSuccess;
                if(todoId == 0)
                {
                    
                    isSuccess = repository.Add(txtName.Text, txtTime.Text, txtFrom.Text, txtTo.Text, txtSession.Text, txtDetails.Text);
                    
                }
                else
                {
                    isSuccess = repository.Edit(todoId,txtName.Text, txtTime.Text, txtFrom.Text, txtTo.Text, txtSession.Text, txtDetails.Text);
                }
                if (isSuccess == true)
                {
                    MessageBox.Show("Operation Was Successful :)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Operation Failed!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
