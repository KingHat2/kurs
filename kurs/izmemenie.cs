using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        private SqlDataAdapter sqlDataAdapter = null;
        private bool neRowAdding = false;
        private DataSet dataSet = null;
     
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //блок  обработки исключения
            try
            {   //проверка нажатия на 5 ячейку
                if (e.ColumnIndex == 4)

                {
                    //получение текста из linkLabel 
                    string task = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    //проверка какую команды хотел выполнить пользователь
                    if (task == "Delete")
                    {
                        //вывод MessageBox с вопросом удаление строки
                        if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            //создание переменной
                            int rowIndex = e.RowIndex;
                            //вызов метода RemoveAt
                            dataGridView1.Rows.RemoveAt(rowIndex);
                            //удаление этой строки из dataSet
                            dataSet.Tables["Inventorizacia"].Rows[rowIndex].Delete();
                            //обновление данных в бд
                            sqlDataAdapter.Update(dataSet, "Inventorizacia");
                        }
                    }
                    //проверка какую команды хотел выполнить пользователь
                    else if (task == "Insert")
                    {
                        //созданние int переменой с индексом стрроки
                        int rowIndex = dataGridView1.Rows.Count - 2;
                        //создание переменой куда запишим ссылку на новую строку которую мы создадим в DataSet в таблице Inventorizacia
                        DataRow row = dataSet.Tables["Inventorizacia"].NewRow();
                        //занесение в новые строки данные из dataGridView 
                        row["Invent_nomer"] = dataGridView1.Rows[rowIndex].Cells["Invent_nomer"].Value;
                        row["Tip"] = dataGridView1.Rows[rowIndex].Cells["Tip"].Value;
                        row["Nazvanie"] = dataGridView1.Rows[rowIndex].Cells["Nazvanie"].Value;
                        row["Nomer_Kabineta"] = dataGridView1.Rows[rowIndex].Cells["Nomer_Kabineta"].Value;
                        //добавление новой строки в dataSet
                        dataSet.Tables["Inventorizacia"].Rows.Add(row);
                        dataSet.Tables["Inventorizacia"].Rows.RemoveAt(dataSet.Tables["Inventorizacia"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        //установка для 6 ячейки текс Delete
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = "Delete";
                        //обновление данных в бд и занесение строки из dataGridView и dataSet
                        sqlDataAdapter.Update(dataSet, "Inventorizacia");
                        //установка neRowAdding в значение false
                        neRowAdding = false;

                    }

                    //проверка какую команды хотел выполнить пользователь
                    else if (task == "Update")
                    {
                        //полученние индекса выделенной строки
                        int r = e.RowIndex;
                        //обновление всех данных в dataSet
                        dataSet.Tables["Inventorizacia"].Rows[r]["Invent_nomer"] = dataGridView1.Rows[r].Cells["Invent_nomer"].Value;
                        dataSet.Tables["Inventorizacia"].Rows[r]["Tip"] = dataGridView1.Rows[r].Cells["Tip"].Value;
                        dataSet.Tables["Inventorizacia"].Rows[r]["Nazvanie"] = dataGridView1.Rows[r].Cells["Nazvanie"].Value;
                        dataSet.Tables["Inventorizacia"].Rows[r]["Nomer_Kabineta"] = dataGridView1.Rows[r].Cells["Nomer_Kabineta"].Value;
                        //замена текста на 5 ячейки на Delete
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = "Delete";
                        //убрает необходимость нажимать на enter для Update после вводы новых данных
                        this.Validate();
                        this.dataGridView1.EndEdit();
                        //обновление данных в бд
                        sqlDataAdapter.Update(dataSet, "Inventorizacia");
                    }
                    //обновление Бд
                    reload_list();
                }
            }
            catch
            {

            }
        }

    

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //neRowAdding == false проверка на редактирование данных
                if (neRowAdding == false)
                {
                    //получение индекса выделенной строки
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    //созданние экземпляра класса dataGridViewRow с присвоеннием строки по индексу по индексу которой мы присвоили переменную
                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];
                    //создание linkCell
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    //устновка LinkCell в 6 ячейку
                    dataGridView1[4, rowIndex] = linkCell;
                    //переименование 6 ячейки Delete в Update
                    editingRow.Cells["Delete"].Value = "Update";
                }
            }
            //вывод сообщения о ошибки
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                // провенрка neRowAdding == false что бы не путались переменные при Insert и Update
                if (neRowAdding == false)
                {
                    //добавление новой строки
                    neRowAdding = true;
                    //добавление строки в последнию строку
                    int lastRow = dataGridView1.Rows.Count - 2;
                    //используя индекс последний строки в которой добавляем новую ячейку для создания класса DataGridViewRow
                    DataGridViewRow row = dataGridView1.Rows[lastRow];
                    //создание linkCell
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    //устновка LinkCell в 4 ячейку
                    dataGridView1[5, lastRow] = linkCell;
                    //переменование строки Delete в Insert
                    row.Cells["Delete"].Value = "Insert";
                }
            }
            //вывод сообщения о ошибки
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}   
