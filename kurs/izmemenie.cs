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

    public partial class izmemenie : Form
    {
        public izmemenie()
        {
            InitializeComponent();
        }
        private void izmemenie_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            GetListUsers();
        }
        string id_selected_rows = "0";
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[1].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[1].Value.ToString();
            //Указываем ID выделенной строки в метке

        }
        string connStr = "server=caseum.ru;port=33333;user=st_2_19_19;database=st_2_19_19;password=38138013";

        MySqlConnection conn;    
        private DataTable table = new DataTable();
        public void GetListUsers()
        {

            //Запрос для вывода строк в БД
            string sql = "SELECT n_poezda AS 'Номер поезда' ,vrema AS 'Время', mesto_otpravki AS 'Место отправления', mesto_naznachena AS 'место назначение' FROM raspisanie";
            try
            {
                conn.Open();
                MySqlDataAdapter IDataAdapter = new MySqlDataAdapter(sql, conn);
                DataSet dataset = new DataSet();
                IDataAdapter.Fill(dataset);
                dataGridView1.DataSource = dataset.Tables[0];

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
            finally
            {
                conn.Close();

            }



        }

      
       
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListUsers();
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            reload_list();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
          izmemenie form = new izmemenie();
            this.Hide();
        }

        private void izmemenie_Load_1(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            GetListUsers();
        }
    }
}
