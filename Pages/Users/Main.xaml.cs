using praktika26_Shein.Classes;
using praktika26_Shein.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace praktika26_Shein.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        /// <summary>
        /// Контекст пользователей
        /// </summary>
        public UserContext AllUsers = new UserContext();

        public Main()
        {
            InitializeComponent();

            // Перебираем пользователей
            foreach (Models.Users User in AllUsers.Users)
            {
                // Добавляем на экран (предполагается, что в XAML есть элемент с именем UsersPanel, например StackPanel)
                UsersPanel.Children.Add(new Elements.Item(User, this));
            }
        }

        /// <summary>
        /// Метод добавления пользователей
        /// </summary>
        private void AddUser(object sender, RoutedEventArgs e) =>
            // Открываем страницу добавления пользователей
            MainWindow.init.OpenPages(new Pages.Users.Add(this));
    }
}