using System.Windows;
using praktika26_Shein.Classes;

namespace praktika26_Shein
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            // Простая проверка: admin/admin или user/user
            if (login == "admin" && password == "admin")
            {
                UserSession.CurrentRole = "Admin";
                OpenMainWindow();
            }
            else if (login == "user" && password == "user")
            {
                UserSession.CurrentRole = "User";
                OpenMainWindow();
            }
            else
            {
                lblError.Content = "Неверный логин или пароль";
            }
        }

        private void OpenMainWindow()
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}