using praktika26_Shein.Classes;
using praktika26_Shein.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace praktika26_Shein.Pages.Users.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        /// <summary>
        /// Контекст клубов
        /// </summary>
        private ClubsContext AllClub = new ClubsContext();

        /// <summary>
        /// Страница Main
        /// </summary>
        private Main Main;

        /// <summary>
        /// Данные о пользователе
        /// </summary>
        private Models.Users User;

        public Item(Models.Users User, Main Main)
        {
            InitializeComponent();

            // Указываем фамилию в поле
            this.FIO.Text = User.FIO;

            // Указываем дату аренды
            this.RentStart.Text = User.RentStart.ToString("yyyy-MM-dd");

            // Указываем время аренды
            this.RentTime.Text = User.RentStart.ToString("HH:mm");

            // Указываем продолжительность аренды
            this.Duration.Text = User.Duration.ToString();

            // Получаем клуб по ID и указываем наименование
            var club = AllClub.Clubs.FirstOrDefault(x => x.Id == User.IdClub);
            this.Club.Text = club?.Name ?? "Клуб не найден";

            // Запоминаем страницу Main
            this.Main = Main;

            // Запоминаем пользователя
            this.User = User;
        }

        /// <summary>
        /// Метод изменения
        /// </summary>
        private void EditUser(object sender, System.Windows.RoutedEventArgs e)
        {
            // Открываем страницу добавления, передавая данные пользователя
            MainWindow.init.OpenPages(new Pages.Users.Add(this.Main, this.User));
        }

        /// <summary>
        /// Метод удаления
        /// </summary>
        private void DeleteUser(object sender, System.Windows.RoutedEventArgs e)
        {
            // Удаляем пользователя из контекста
            Main.AllUsers.Users.Remove(User);
            // Сохраняем изменения
            Main.AllUsers.SaveChanges();

            // Удаляем элемент со страницы Main
            Main.UsersPanel.Children.Remove(this);
        }
    }
}