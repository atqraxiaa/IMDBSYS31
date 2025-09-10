using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace sybau_database
{
    public partial class Dashboard : Form
    {
        private string connectionString = "Data Source=REIREII;Initial Catalog=StudentDB;Integrated Security=True;TrustServerCertificate=True;";

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBfname.Text) ||
                string.IsNullOrWhiteSpace(txtBlname.Text) ||
                string.IsNullOrWhiteSpace(txtBage.Text) ||
                string.IsNullOrWhiteSpace(txtBcourse.Text))
            {
                MessageBox.Show("Please input fields before adding a student's details!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Students (FirstName, LastName, Age, Course) VALUES (@FirstName, @LastName, @Age, @Course)", conn);
                cmd.Parameters.AddWithValue("@FirstName", txtBfname.Text);
                cmd.Parameters.AddWithValue("@LastName", txtBlname.Text);
                cmd.Parameters.AddWithValue("@Age", txtBage.Text);
                cmd.Parameters.AddWithValue("@Course", txtBcourse.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBselectedID.Text))
            {
                MessageBox.Show("Please enter a Student ID to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE StudentID = @SelectedID", conn);
                cmd.Parameters.AddWithValue("@SelectedID", txtBselectedID.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtBfname.Text = reader["FirstName"].ToString();
                    txtBlname.Text = reader["LastName"].ToString();
                    txtBage.Text = reader["Age"].ToString();
                    txtBcourse.Text = reader["Course"].ToString();

                    txtBselectedID.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No student found with that ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Course = @Course WHERE StudentID = @SelectedID", conn);

                cmd.Parameters.AddWithValue("@FirstName", txtBfname.Text);
                cmd.Parameters.AddWithValue("@LastName", txtBlname.Text);
                cmd.Parameters.AddWithValue("@Age", txtBage.Text);
                cmd.Parameters.AddWithValue("@Course", txtBcourse.Text);
                cmd.Parameters.AddWithValue("@SelectedID", txtBselectedID.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                    txtBselectedID.Enabled = true;

                    txtBfname.Clear();
                    txtBlname.Clear();
                    txtBage.Clear();
                    txtBcourse.Clear();
                    txtBselectedID.Clear();
                }
                else
                {
                    MessageBox.Show("Student ID not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBselectedID.Text))
            {
                MessageBox.Show("Please enter a Student ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE StudentID = @SelectedID", conn);
                    cmd.Parameters.AddWithValue("@SelectedID", txtBselectedID.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("No student found with that ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                tableData.DataSource = dt;
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
