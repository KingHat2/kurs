using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
      
    }
    static class Auth
    {
        //Статичное поле, которое хранит значение статуса авторизации
        public static bool auth = false;
        //Статичное поле, которое хранит значения pass пользователя
        public static string auth_pass = null;
        //Статичное поле, которое хранит значения id пользователя
        public static string auth_id = null;
        //Статичное поле, которое хранит значения ФИО пользователя
        public static string auth_login = null;
        //статик поле для данных сотрудника
        public static string auth_sotrud = null;
        //Статичное поле, которое хранит количество привелегий пользователя
        public static int auth_role = 0;
    }
}

