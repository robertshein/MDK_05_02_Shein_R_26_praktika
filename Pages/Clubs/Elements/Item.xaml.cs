using praktika26_Shein.Models;
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

namespace praktika26_Shein.Pages.Clubs.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        /// <summary>
        /// Главная страница клубов
        /// </summary>
        private Main Main;

        /// <summary>
        /// Данные клуба
        /// </summary>
        private Models.Clubs Club;

        public Item(Models.Clubs Club, Main Main)
        {
            InitializeComponent();

            // Присваиваем наименование
            this.Name.Text = Club.Name;
            // Присваиваем адрес
            this.Address.Text = Club.Address;
            // Присваиваем время работы
            this.WorkTime.Text = Club.WorkTime;

            // Запоминаем в переменные
            this.Main = Main;
            this.Club = Club;
        }

        private void EditClub(object sender, System.Windows.RoutedEventArgs e)
        {
            // Открываем страницу добавления, пересылая данные
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this.Main, this.Club));
        }

        /// <summary>
        /// Метод удаления
        /// </summary>
        private void DeleteClub(object sender, System.Windows.RoutedEventArgs e)
        {
            // Удаляем клуб из контекста
            Main.AllClub.Clubs.Remove(Club);
            // Сохраняем изменения
            Main.AllClub.SaveChanges();
            // Удаляем элемент со страницы Main
            (Main.Parent as Panel)?.Children.Remove(this);
        }
    }
}