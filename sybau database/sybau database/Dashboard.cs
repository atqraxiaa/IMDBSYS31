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
        private string connectionString = "Data Source=REIREII;Initial Catalog=SybauDB;Integrated Security=True;TrustServerCertificate=True;";

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Name, Email) VALUES (@Name, @Email)", conn);
                cmd.Parameters.AddWithValue("@Name", txtBoxName.Text);
                cmd.Parameters.AddWithValue("@Email", txtBoxEmail.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                tableData.DataSource = dt;
            }
        }
    }
}
