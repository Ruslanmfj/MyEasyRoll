using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public bool chat_passed = false;
        public bool chat_resizeNS = false;
        public bool chat_resize_NSWE = false;
        public double[] chat_old_coordinates = new double[2];
        public double[] chat_old_size = new double[2];

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

        private void Window_close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_minimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void rp_user_licence_Loaded(object sender, RoutedEventArgs e)
        {
            TextRange doc = new TextRange(rp_user_licence.Document.ContentStart, rp_user_licence.Document.ContentEnd);
            using (FileStream fs = new FileStream("license.rtf", FileMode.Open))
            {
                if (System.IO.Path.GetExtension("license.rtf").ToLower() == ".rtf")
                    doc.Load(fs, DataFormats.Rtf);
                else if (System.IO.Path.GetExtension("license.rtf").ToLower() == ".txt")
                    doc.Load(fs, DataFormats.Text);
                else
                    doc.Load(fs, DataFormats.Xaml);
            }
        }

        private void wp_toregister_Click(object sender, RoutedEventArgs e)
        {
            general_tabs.SelectedItem = register_page;
        }

        private void rp_register_button_Click(object sender, RoutedEventArgs e)
        {
            string emailcheck = rp_email.Text;
            int errorcode = 0;
            if (emailcheck == "") errorcode = 5;
            else if (emailcheck.Contains('@') == false) errorcode = 1;
            else if (emailcheck.IndexOf('@') == 0) errorcode = 6;
            else
            {
                emailcheck = emailcheck.Remove(0, emailcheck.IndexOf('@') + 1);
                if (emailcheck.Contains('.') == false) errorcode = 2;
                else
                {
                    string userlog = rp_login.Text;
                    string usermail = rp_email.Text;
                    database DB = new database();
                    DataTable dt = new DataTable();

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand commanda = new MySqlCommand("SELECT * FROM users WHERE email = @uM", DB.getConnection());
                    commanda.Parameters.Add("@uM", MySqlDbType.VarChar).Value = rp_email.Text;

                    adapter.SelectCommand = commanda;
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0) errorcode = 3;
                }
            }
            if (rp_name.Text == "") errorcode = 7;
            if (rp_surname.Text == "") errorcode = 8;
            if (rp_login.Text == "") errorcode = 9;
            else
            {
                string userlog1 = rp_login.Text;
                string usermail1 = rp_email.Text;
                database DB1 = new database();
                DataTable dt1 = new DataTable();
                MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                MySqlCommand commanda1 = new MySqlCommand("SELECT * FROM users WHERE login = @uL", DB1.getConnection());
                commanda1.Parameters.Add("@uL", MySqlDbType.VarChar).Value = rp_login.Text;

                adapter1.SelectCommand = commanda1;
                adapter1.Fill(dt1);
                if (dt1.Rows.Count > 0) errorcode = 4;
            }
            if (rp_password.Password.ToString() == "") errorcode = 10;

            if (errorcode == 1)
            {
                rp_email.Text = "Введите корректный адрес";
                rp_email.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 2)
            {
                rp_email.Text = "Введите корректный адрес";
                rp_email.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 3)
            {
                rp_email.Text = "Данный адрес электронынй почты уже зарегестрирован";
                rp_email.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 4)
            {
                rp_login.Text = "Данный логин уже занят";
                rp_login.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 5)
            {
                rp_email.Text = "Поле не может оставаться пустым";
                rp_email.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 6)
            {
                rp_email.Text = "Введите корректный адрес";
                rp_email.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 7)
            {
                rp_name.Text = "не может быть пустым";
                rp_name.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 8)
            {
                rp_surname.Text = "не может быть пустым";
                rp_surname.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 9)
            {
                rp_login.Text = "не может быть пустым";
                rp_login.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else if (errorcode == 10)
            {
                rp_pass_label.Content = "Поле пароля не может быть пустым";
                rp_pass_label.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            else
        {
            database datab = new database();
                MySqlCommand SQLcommand = new MySqlCommand("INSERT INTO users (id,login,name,surname,password,email,email_verify) VALUES (NULL,@uL,@uN,@uS,@uP,@uE,'no');", datab.getConnection());
            SQLcommand.Parameters.Add("@uL", MySqlDbType.VarChar).Value = rp_login.Text;
            SQLcommand.Parameters.Add("@uN", MySqlDbType.VarChar).Value = rp_name.Text;
            SQLcommand.Parameters.Add("@uS", MySqlDbType.VarChar).Value = rp_surname.Text;
            SQLcommand.Parameters.Add("@uP", MySqlDbType.VarChar).Value = rp_password.Password.ToString();
            SQLcommand.Parameters.Add("@uE", MySqlDbType.VarChar).Value = rp_email.Text;

            datab.openConnection();
            if (SQLcommand.ExecuteNonQuery() == 1)
            {
                general_tabs.SelectedItem = map_0;
                    logined_user.Content = rp_surname.Text + " " + rp_name.Text + "(" + rp_login.Text + ")";
                logined_user.Visibility = Visibility.Visible;
            }
                
                else MessageBox.Show("Ошибка записи. Возможно сервис на данный момент недоступен.");
            datab.closeConnection();
        }
        }

        private void rp_email_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
            (sender as TextBox).Foreground = new SolidColorBrush(Color.FromRgb(245, 124, 1));
        }

        private void rp_password_GotFocus(object sender, RoutedEventArgs e)
        {
            rp_pass_label.Content = "Пароль";
            rp_pass_label.Foreground = new SolidColorBrush(Color.FromRgb(245, 124, 1));
        }

        private void wp_login_button_Click(object sender, RoutedEventArgs e)
        {
            int errorcode = 0;
            string userlogin = "";
            string UserName = "";
            string UserSurname = "";
            if (wp_userlog.Text == "") errorcode = 1;
            else
            {
                string userlog1 = wp_userlog.Text;
                database DB = new database();
                DataTable dt = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand commanda = new MySqlCommand("SELECT * FROM users WHERE login = @uL", DB.getConnection());
                commanda.Parameters.Add("@uL", MySqlDbType.VarChar).Value = wp_userlog.Text;

                adapter.SelectCommand = commanda;
                adapter.Fill(dt);
                if (dt.Rows.Count == 0) errorcode = 2;
                if (wp_userpass.Password.ToString() == "") errorcode = 3;
                else
                {
                    string userlog = wp_userlog.Text;
                    database DB1 = new database();
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                    MySqlCommand commanda1 = new MySqlCommand("SELECT * FROM users WHERE login = @uL AND password = @uP", DB1.getConnection());
                    commanda1.Parameters.Add("@uL", MySqlDbType.VarChar).Value = wp_userlog.Text;
                    commanda1.Parameters.Add("@uP", MySqlDbType.VarChar).Value = wp_userpass.Password.ToString();

                    adapter1.SelectCommand = commanda1;
                    adapter1.Fill(dt1);
                    userlogin = dt1.Rows[0][1].ToString();
                    UserName = dt1.Rows[0][2].ToString();
                    UserSurname = dt1.Rows[0][3].ToString();

                    if (dt1.Rows.Count == 0) errorcode = 4;
                }
            }
            if (errorcode == 1)
            {
                wp_userlog.Text = "Поле не может быть пустым";
                wp_userlog.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            if (errorcode == 2)
            {
                wp_pass_label.Content = "Не верное имя пользователя или пароль";
                wp_pass_label.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            if (errorcode == 3)
            {
                wp_userlog.Text = "Пароль не может быть пустым";
                wp_userlog.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            if (errorcode == 4)
            {
                wp_pass_label.Content = "Не верное имя пользователя или пароль";
                wp_pass_label.Foreground = new SolidColorBrush(Color.FromRgb(214, 11, 11));
            }
            if (errorcode  == 0)
            {
                general_tabs.SelectedItem = map_0;
                logined_user.Content = UserSurname + " " + UserName + " (" + userlogin + ")";
                logined_user.Visibility = Visibility.Visible;
            }
        }

        private void wp_userpass_GotFocus(object sender, RoutedEventArgs e)
        {
            wp_pass_label.Content = "Пароль";
            wp_pass_label.Foreground = new SolidColorBrush(Color.FromRgb(245, 124, 1));
        }

        private void mp_more_menus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mp_menus.Width == 45)
            {
                mp_tools_dock.Margin = new Thickness(300, 0, 0, 0);
                mp_menus.Width = 300;
            }
            else
            {
                mp_tools_dock.Margin = new Thickness(45, 0, 0, 0);
                mp_menus.Width = 45;
            }
        }

        private void mp_chat_header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chat_passed = true;
            chat_old_coordinates[0] = Mouse.GetPosition(mp_chat_header).X;
            chat_old_coordinates[1] = Mouse.GetPosition(mp_chat_header).Y;
        }

        private void mp_chat_header_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chat_passed = false;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (chat_passed == true)
            {
                Thickness s = new Thickness(Mouse.GetPosition(General_window).X - chat_old_coordinates[0], Mouse.GetPosition(General_window).Y -33, 0, 0);
                mp_chat.Margin = s;
            }
            if (chat_resizeNS == true)
            {
                double he = 0;
                he = Mouse.GetPosition(mp_grid1).Y - mp_chat.Margin.Top;
                if (he > 0) mp_chat.Height = he;

            }
            if (chat_resize_NSWE == true)
            {
                double he = 0;
                double wi = 0;
                he = Mouse.GetPosition(mp_grid1).Y - mp_chat.Margin.Top;
                wi = Mouse.GetPosition(mp_grid1).X - mp_chat.Margin.Left;
                if (he > 0) mp_chat.Height = he;
                if (wi > 0) mp_chat.Width = wi;
            }
        }

        private void mp_chat_hide_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mp_chat.Height == 25)
            {
                mp_chat.Height = 400;
                mp_chat_hide_button.Content = "--";
            }
            else
            {
                mp_chat.Height = 25;
                mp_chat_hide_button.Content = "O";
            }
        }

        private void mp_chat_close_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mp_chat.Visibility = Visibility.Collapsed;
        }

        private void mp_char_sizeNS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chat_resizeNS = true;
            chat_old_size[0] = mp_chat.Height;
            chat_old_size[1] = mp_chat.Width;
        }

        private void mp_char_sizeNS_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chat_resizeNS = false;
        }

        private void mp_grid1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chat_passed = false;
            chat_resizeNS = false;
            chat_resize_NSWE = false;
        }

        private void mp_char_sizeEN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chat_resize_NSWE = true;
            chat_old_size[0] = mp_chat.Height;
            chat_old_size[1] = mp_chat.Width;
        }

        private void mp_char_sizeEN_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chat_resize_NSWE = false;
        }

        private void mp_char_sizeEN_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void mp_map_viewbox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                mp_map_viewbox.Height = mp_map_viewbox.Height + 50;
                mp_map_viewbox.Width = mp_map_viewbox.Width + 50;
            }
            else
        {
                mp_map_viewbox.Height = mp_map_viewbox.Height - 50;
                mp_map_viewbox.Width = mp_map_viewbox.Width - 50;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testimg.Height = mp_map_grid.Height;
            testimg.Width = mp_map_grid.Width;

            for (int i = 0; i != 50;i++)
            {
                ColumnDefinition cw = new ColumnDefinition();
                cw.Width = new GridLength(25);
                mp_map_grid.ColumnDefinitions.Add(cw);
                for (int o=0; o != 50; o++)
        {
                    RowDefinition rw = new RowDefinition();
                    rw.Height = new GridLength(25);
                    mp_map_grid.RowDefinitions.Add(rw);
                    Border br = new Border();
                    br.Name = "Cell" + i.ToString() + o.ToString();
                    br.MouseEnter += CellEnter;
                    br.MouseLeave += CellLeave;
                    br.BorderThickness = new Thickness(1, 1, 0, 0);
                    br.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    br.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    Grid.SetRow(br, o);
                    Grid.SetColumn(br, i);
                    mp_map_grid.Children.Add(br);
                    if (o == 49)
                        if (i==49) br.BorderThickness = new Thickness(1, 1, 1, 1);
                    else br.BorderThickness = new Thickness(1, 1, 1, 0);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _111.Content = "EbsTooDay";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _111.Content = "aAa";
        }

        private void CellEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 85));
        }

        private void CellLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
    }
}
