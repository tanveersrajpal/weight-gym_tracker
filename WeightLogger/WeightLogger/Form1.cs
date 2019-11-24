using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WeightLogger
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            DataGridLoader(@"Data Source=MYCOMPUTER\SQLEXPRESS;Initial Catalog=PersonalProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "SELECT * FROM WeightLogger");
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            SQLInjector();
            ClearRows();
        }
        public void SQLInjector()
        {
            try
            {


                string conn = @"Data Source = MYCOMPUTER\SQLEXPRESS; Initial Catalog = PersonalProject; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                string command = "INSERT INTO WeightLogger (Date, Weight, Fat, Muscle) VALUES (@Date, @Weight, @Fat, @Muscle)";
                SqlConnection con = new SqlConnection(conn);
                using (SqlCommand cmd = new SqlCommand(command, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Weight", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Fat", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Muscle", textBox3.Text);

                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Saved");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                
                DataGridLoader(@"Data Source=MYCOMPUTER\SQLEXPRESS;Initial Catalog=PersonalProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "SELECT * FROM WeightLogger");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void DataGridLoader(string connectionstring, string command)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand(command, con);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            

        }
         public void ClearRows()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trainer trainer = new Trainer();
            if (trainer.IsDisposed)
            {
                trainer = new Trainer();
            }                
            trainer.Show();
            
        }

        private void WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked it! Yahh");
        }

        private void AllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }


}
