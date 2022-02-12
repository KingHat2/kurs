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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void ManagerRole(int role)
        {
            switch (role)
            {
                case 1://кассир
                    metroButton1.Enabled = true;
                    metroButton2.Enabled = true;
                    metroButton3.Enabled = false;
                    metroButton4.Enabled = false;
                    break;
                case 2://Администратор
                    metroButton1.Enabled = true;
                    metroButton2.Enabled = true;
                    metroButton3.Enabled = true;
                    metroButton4.Enabled = true;
                    break;
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            Form2 = new Form2();
            Form2.ShowDialog();
            if (Auth.auth)
            {
                //Отображаем рабочую форму
                this.Show();
                //Вытаскиваем из класса поля в label'ы
                metroLabel1.Text = Auth.auth_id;
               
            }
            else
            {
                this.Close();
            }
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}