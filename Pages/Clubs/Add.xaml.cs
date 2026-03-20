using praktika26_Shein.Classes;
using praktika26_Shein.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace praktika26_Shein.Pages.Clubs
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        /// <summary>
        /// Главная страница клубов
        /// </summary>
        private Main Main;

        /// <summary>
        /// Данные клуба
        /// </summary>
        private Models.Clubs Club;

        public Add(Main Main, Models.Clubs Club = null)
        {
            InitializeComponent();
            if (UserSession.CurrentRole != "Admin")
            {
                MessageBox.Show("У вас нет прав на редактирование.");
                MainWindow.init.OpenPages(new Pages.Clubs.Main());
                return;
            }

            // Запоминаем в переменную
            this.Main = Main;

            // Если пришёл клуб, отображаем данные
            if (Club != null)
            {
                // Запоминаем клуб в переменную
                this.Club = Club;
                // Указываем наименование
                this.Name.Text = Club.Name;
                // Указываем адрес
                this.Address.Text = Club.Address;
                // Указываем время работы
                this.WorkTime.Text = Club.WorkTime;
                // Изменяем текст кнопки
                BtnAdd.Content = "Изменить";
            }
        }

        /// <summary>
        /// Метод добавления или изменения
        /// </summary>
        private void AddClub(object sender, System.Windows.RoutedEventArgs e)
        {
            // Если клуб пустой
            if (this.Club == null)
            {
                // Создаём новый объект
                Club = new Models.Clubs();
                // Задаём данные
                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;
                // Добавляем объект в контекст
                this.Main.AllClub.Clubs.Add(this.Club);
                // Сохраняем изменения
                this.Main.AllClub.SaveChanges();
            }
            else
            {
                // Если изменение
                // Изменяем данные
                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;
                // Сохраняем изменения
                this.Main.AllClub.SaveChanges();
            }

            // Открываем страницу клубов
            MainWindow.init.OpenPages(new Pages.Clubs.Main());
        }
    }
}