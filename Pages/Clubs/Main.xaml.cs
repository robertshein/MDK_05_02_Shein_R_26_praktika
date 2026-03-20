using System.Linq;
using System.Windows;
using System.Windows.Controls;
using praktika26_Shein.Classes;
using praktika26_Shein.Models;

namespace praktika26_Shein.Pages.Clubs
{
    public partial class Main : Page
    {
        public ClubsContext AllClub = new ClubsContext();

        public Main()
        {
            InitializeComponent();

            // Скрываем кнопку добавления, если роль не Admin
            if (UserSession.CurrentRole != "Admin")
                BthAdd.Visibility = Visibility.Collapsed;

            LoadClubs();
        }

        public void LoadClubs()
        {
            var query = AllClub.Clubs.AsQueryable();

            // Фильтрация
            string filter = txtFilter.Text.Trim();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(c => c.Name.Contains(filter) || c.Address.Contains(filter));
            }

            // Сортировка
            string sortBy = (cmbSortBy.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (sortBy == "Название")
                query = query.OrderBy(c => c.Name);
            else if (sortBy == "Адрес")
                query = query.OrderBy(c => c.Address);

            var clubs = query.ToList();

            Parent.Children.Clear();
            foreach (var club in clubs)
            {
                var item = new Elements.Item(club, this);
                // Устанавливаем режим только для чтения, если роль не Admin
                item.IsReadOnly = (UserSession.CurrentRole != "Admin");
                Parent.Children.Add(item);
            }
        }

        private void ApplyFilterSort_Click(object sender, RoutedEventArgs e)
        {
            LoadClubs();
        }

        private void AddClub(object sender, RoutedEventArgs e)
        {
            // Проверка прав доступа
            if (UserSession.CurrentRole != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление записей.", "Доступ запрещён");
                return;
            }
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this));
        }
    }
}