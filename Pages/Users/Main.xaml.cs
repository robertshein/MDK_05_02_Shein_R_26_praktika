using System.Linq;
using System.Windows;
using System.Windows.Controls;
using praktika26_Shein.Classes;
using praktika26_Shein.Models;

namespace praktika26_Shein.Pages.Users
{
    public partial class Main : Page
    {
        public UserContext AllUsers = new UserContext();
        private ClubsContext AllClub = new ClubsContext();

        public Main()
        {
            InitializeComponent();

            if (UserSession.CurrentRole != "Admin")
                BthAdd.Visibility = Visibility.Collapsed;

            LoadUsers();
        }

        public void LoadUsers()
        {
            // Загружаем пользователей с подгрузкой клубов (для фильтрации можно использовать Join)
            var users = AllUsers.Users.ToList();

            // Фильтрация по ФИО
            string filter = txtFilter.Text.Trim();
            if (!string.IsNullOrEmpty(filter))
            {
                users = users.Where(u => u.FIO.Contains(filter)).ToList();
            }

            // Сортировка
            string sortBy = (cmbSortBy.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (sortBy == "ФИО")
                users = users.OrderBy(u => u.FIO).ToList();
            else if (sortBy == "Дата аренды")
                users = users.OrderBy(u => u.RentStart).ToList();
            else if (sortBy == "Продолжительность")
                users = users.OrderBy(u => u.Duration).ToList();

            Parent.Children.Clear();
            foreach (var user in users)
            {
                var item = new Elements.Item(user, this);
                item.IsReadOnly = (UserSession.CurrentRole != "Admin");
                Parent.Children.Add(item);
            }
        }

        private void ApplyFilterSort_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentRole != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление записей.", "Доступ запрещён");
                return;
            }
            MainWindow.init.OpenPages(new Pages.Users.Add(this));
        }
    }
}