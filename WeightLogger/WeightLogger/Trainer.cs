using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WeightLogger
{
    public partial class Trainer : Form
    {
        public Form1 Form1 { get; set; } = new Form1();

        public Trainer()
        {
            InitializeComponent();
        }

        private void Trainer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personalProjectDataSet.Trainer' table. You can move, or remove it, as needed.
            this.trainerTableAdapter.Fill(this.personalProjectDataSet.Trainer);

            DataGridLoader();
            DataAccendingSort();
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Inject();
            DataGridLoader();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Form1.IsDisposed)
                Form1 = new Form1();
            Form1.Show();

        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "No")
            {
                textBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox1.SelectedItem = "No";
                comboBox2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
        }

        //Pre-defined methods below
        public void Inject()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=MYCOMPUTER\SQLEXPRESS;Initial Catalog=PersonalProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Trainer (Date, Gym, Time, Sett, RestDay) VALUES (@Date, @Gym, @Time, @Sett, @RestDay)", con)) 
                {
                    if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox2.Text != "(Select)" && comboBox1.Text == "Yes")
                    {
                        cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Gym", comboBox1.SelectedItem);
                        cmd.Parameters.AddWithValue("@Time", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Sett", comboBox2.SelectedItem);
                        cmd.Parameters.AddWithValue("@RestDay", "NO");

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            MessageBox.Show("Saved");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                        con.Close();
                    }
                    if (checkBox1.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Gym", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Time", 0);
                        cmd.Parameters.AddWithValue("@Sett", "-NA-");
                        cmd.Parameters.AddWithValue("@RestDay", "YES");
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            MessageBox.Show("Saved");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                        con.Close();
                    }
                    else if (comboBox1.Text == "No" && checkBox1.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Gym", comboBox1.SelectedItem);
                        cmd.Parameters.AddWithValue("@Time", 0);
                        cmd.Parameters.AddWithValue("@Sett", "-NA-");
                        cmd.Parameters.AddWithValue("@RestDay", "NO");
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            MessageBox.Show("Saved");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                        con.Close();
                    }
                    else if (comboBox2.Text == "(Select)")
                        MessageBox.Show("Please select the Set you did today.");
                    else if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        throw new ArgumentNullException("Time field cannot be NULL!");
                    }
                }

                    
                    

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void DataGridLoader()
        {
            string command = "SELECT * FROM Trainer";
            SqlConnection con = new SqlConnection(@"Data Source=MYCOMPUTER\SQLEXPRESS;Initial Catalog=PersonalProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using (SqlCommand cmd = new SqlCommand(command, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                    
            }
        }

        public void DataAccendingSort()
        {
            string command = "SELECT * FROM Trainer ORDER BY Date ASC";
            SqlConnection con = new SqlConnection(@"Data Source=MYCOMPUTER\SQLEXPRESS;Initial Catalog=PersonalProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using (SqlCommand cmd = new SqlCommand(command, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        

        private void Label6_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
