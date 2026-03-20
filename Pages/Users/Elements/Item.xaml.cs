using System.Linq;
using System.Windows;
using System.Windows.Controls;
using praktika26_Shein.Classes;

namespace praktika26_Shein.Pages.Users.Elements
{
    public partial class Item : UserControl
    {
        private ClubsContext AllClub = new ClubsContext();
        private Main Main;
        private Models.Users User;

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(Item), new PropertyMetadata(false, OnIsReadOnlyChanged));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Item;
            if (control != null)
            {
                bool isReadOnly = (bool)e.NewValue;
                control.BtnEdit.Visibility = isReadOnly ? Visibility.Collapsed : Visibility.Visible;
                control.BtnDelete.Visibility = isReadOnly ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Item(Models.Users User, Main Main)
        {
            InitializeComponent();

            this.FIO.Text = User.FIO;
            this.RentStart.Text = User.RentStart.ToString("yyyy-MM-dd");
            this.RentTime.Text = User.RentStart.ToString("HH:mm");
            this.Duration.Text = User.Duration.ToString();

            var club = AllClub.Clubs.FirstOrDefault(x => x.Id == User.IdClub);
            this.Club.Text = club?.Name ?? "Клуб не найден";

            this.Main = Main;
            this.User = User;
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentRole != "Admin") return;
            MainWindow.init.OpenPages(new Pages.Users.Add(this.Main, this.User));
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentRole != "Admin") return;
            Main.AllUsers.Users.Remove(User);
            Main.AllUsers.SaveChanges();
            Main.LoadUsers(); // обновляем список
        }
    }
}