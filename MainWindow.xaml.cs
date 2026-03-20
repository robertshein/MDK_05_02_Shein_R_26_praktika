using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace praktika26_Shein
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Ссылка на окно MainWindow
        /// </summary>
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();

            // Запоминаем ссылку
            init = this;

            // Открываем страницу клубов
            OpenPages(new Pages.Clubs.Main());
        }

        /// <summary>
        /// Метод открытия страниц
        /// </summary>
        public void OpenPages(Page Page)
        {
            // Обращаемся к фрейму и открываем страницу
            frame.Navigate(Page);
        }

        /// <summary>
        /// Клубы
        /// </summary>
        private void Clubs(object sender, RoutedEventArgs e) =>
            OpenPages(new Pages.Clubs.Main());

        /// <summary>
        /// Пользователи
        /// </summary>
        private void Users(object sender, RoutedEventArgs e) =>
            OpenPages(new Pages.Users.Main());
    }
}