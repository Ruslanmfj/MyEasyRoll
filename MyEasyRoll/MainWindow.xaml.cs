using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyEasyRoll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void General_window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = 0;
            this.Top = 0;
        }

        private void wp_register_button_Click(object sender, RoutedEventArgs e)
        {
            general_tabs.SelectedItem = register_page;
        }

        private void rp_submit_Click(object sender, RoutedEventArgs e)
        {
            database datab = new database();
            MySqlCommand SQLcommand = new MySqlCommand("INSERT INTO users (id,login,name,surname,password,email) VALUES (NULL,@uL,@uN,@uS,@uP,@uE);", datab.getConnection());
            SQLcommand.Parameters.Add("@uL", MySqlDbType.VarChar).Value = rp_login.Text;
            SQLcommand.Parameters.Add("@uN", MySqlDbType.VarChar).Value = rp_name.Text;
            SQLcommand.Parameters.Add("@uS", MySqlDbType.VarChar).Value = rp_surname.Text;
            SQLcommand.Parameters.Add("@uP", MySqlDbType.VarChar).Value = rp_password.Password.ToString();
            SQLcommand.Parameters.Add("@uE", MySqlDbType.VarChar).Value = rp_email.Text;

            datab.openConnection();
            if (SQLcommand.ExecuteNonQuery() == 1)
            {
                general_tabs.SelectedItem = map_0;
                logined_user.Content = rp_surname.Text+" "+rp_name.Text+"("+rp_login.Text+")";
                logined_user.Visibility = Visibility.Visible;
            }
                
            else MessageBox.Show("Ошибка записи");
            datab.closeConnection();
        }

        private void Window_close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_minimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
