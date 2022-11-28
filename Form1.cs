using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson12WINFROMS
{
    public partial class Form1 : Form
    {
        DataBaseEntities db = new DataBaseEntities();
        Users users = new Users();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("error 11  " + ex.ToString());
            }
           

        }

        private void LoadData()
        {
            dataGridView1.DataSource = db.Users.ToList();
        }

        private void AddName_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(txtAge.Text, out int age);
                users.FirstName = txtName.Text;
                users.LastName = txtFamily.Text;
                users.Age = age;

                db.Users.Add(users);
                if(db.SaveChanges()> 0)
                {
                    MessageBox.Show("success!");
                    LoadData();
                    
                }


            }
            catch (Exception ex )
            {
                MessageBox.Show("error 234 " + ex.ToString());
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if(dataGridView1.CurrentCell.RowIndex != -1)
            {
                int userId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Id"].Value);
                users = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                if(users != null)
                {
                    txtName.Text = users.FirstName;
                    txtFamily.Text = users.LastName;
                    txtAge.Text = users.Age.ToString();
                }
            }
        }

        private void btnDlt_Click(object sender, EventArgs e)
        {
            try
            {
                if (users != null)
                {
                    db.Users.Remove(users);
                    db.SaveChanges();
                    MessageBox.Show("delete success!");
                    LoadData();
                }
            }
            catch (Exception ex )
            {

                MessageBox.Show("error delete! " + ex.ToString());
            }
           
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(users != null)
                {
                    users.FirstName = txtName.Text;
                    users.LastName = txtFamily.Text;
                    int.TryParse(txtAge.Text, out int age);
                    users.Age =  age;
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("update success!");
                    LoadData();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("error update!");
               
            }
        }
    }
}
