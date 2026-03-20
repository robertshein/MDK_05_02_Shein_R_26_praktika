using praktika26_Shein.Classes;
using praktika26_Shein.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace praktika26_Shein.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        /// <summary>
        /// Контекст клубов
        /// </summary>
        private ClubsContext AllClub = new ClubsContext();

        private Models.Users User;
        private Main Main;

        public Add(Main Main, Models.Users User = null)
        {
            InitializeComponent();
            if (UserSession.CurrentRole != "Admin")
            {
                MessageBox.Show("У вас нет прав на редактирование.");
                MainWindow.init.OpenPages(new Pages.Users.Main());
                return;
            }

            this.Main = Main;
            // Запоминаем страницу
            this.Main = Main;

            // Перебираем клубы и добавляем в выпадающий список
            foreach (Models.Clubs club in AllClub.Clubs)
            {
                Clubs.Items.Add(club.Name);
            }

            // Если пользователь для изменения
            if (User != null)
            {
                // Запоминаем пользователя
                this.User = User;

                // Указываем данные пользователя
                FIO.Text = User.FIO;
                RentStart.Text = User.RentStart.ToString("yyyy-MM-dd");
                RentTime.Text = User.RentStart.ToString("HH:mm");
                Duration.Text = User.Duration.ToString();

                // Выбираем соответствующий клуб в ComboBox
                var selectedClub = AllClub.Clubs.FirstOrDefault(x => x.Id == User.IdClub);
                if (selectedClub != null)
                {
                    Clubs.SelectedItem = selectedClub.Name;
                }

                // Изменяем текст кнопки
                BtnAdd.Content = "Изменить";
            }
        }

        /// <summary>
        /// Метод добавления или изменения
        /// </summary>
        private void AddUser(object sender, RoutedEventArgs e)
        {
            // Создаём дату аренды
            DateTime rentStartDate;

            // Конвертируем дату
            if (!DateTime.TryParse(RentStart.Text, out rentStartDate))
            {
                MessageBox.Show("Некорректная дата аренды");
                return;
            }

            // Добавляем время с поля
            TimeSpan rentTime;
            if (!TimeSpan.TryParse(RentTime.Text, out rentTime))
            {
                MessageBox.Show("Некорректное время аренды");
                return;
            }
            rentStartDate = rentStartDate.Add(rentTime);

            // Получаем выбранный клуб
            if (Clubs.SelectedItem == null)
            {
                MessageBox.Show("Выберите клуб");
                return;
            }

            var selectedClub = AllClub.Clubs.FirstOrDefault(x => x.Name == Clubs.SelectedItem.ToString());
            if (selectedClub == null)
            {
                MessageBox.Show("Выбранный клуб не найден");
                return;
            }

            // Если пользователь для добавления
            if (this.User == null)
            {
                // Создаём новый объект
                this.User = new Models.Users();

                // Указываем данные
                this.User.FIO = FIO.Text;
                this.User.RentStart = rentStartDate;
                this.User.Duration = Convert.ToInt32(Duration.Text);
                this.User.IdClub = selectedClub.Id;

                // Добавляем пользователя в контекст
                Main.AllUsers.Users.Add(this.User);
            }
            else
            {
                // Изменяем данные объекта
                this.User.FIO = FIO.Text;
                this.User.RentStart = rentStartDate;
                this.User.Duration = Convert.ToInt32(Duration.Text);
                this.User.IdClub = selectedClub.Id;
            }

            // Сохраняем все изменения
            Main.AllUsers.SaveChanges();

            // Открываем страницу с пользователями
            MainWindow.init.OpenPages(new Pages.Users.Main());
        }
    }
}