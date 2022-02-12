using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kurs
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
      
     
            string connStr = "server=caseum.ru;port=33333;user=st_2_19_19;database=st_2_19_19;password=18138013";
            MySqlConnection conn;
            static string sha256(string randomString)
            {
              
                var crypt = new System.Security.Cryptography.SHA256Managed();
                var hash = new System.Text.StringBuilder();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                return hash.ToString();
            }
            public void GetUserInfo(string login)
            {
                conn.Open();
                string sql = $"SELECT * FROM login WHERE login='{login}'";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Auth.auth_id = reader[0].ToString();
                    Auth.auth_login = reader[1].ToString();
                    Auth.auth_role = Convert.ToInt32(reader[3].ToString());
                    Auth.auth_sotrud = reader[4].ToString();
                }
                reader.Close();
                conn.Close();
                {
                   
                }
            }
            

            private void Form2_Load(object sender, EventArgs e)
            {
                conn = new MySqlConnection(connStr);
            }
        



        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}

