using praktika26_Shein.Classes;
using System.Windows;
using System.Windows.Controls;

namespace praktika26_Shein.Pages.Clubs.Elements
{
    public partial class Item : UserControl
    {
        private Main Main;
        private Models.Clubs Club;

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

        public Item(Models.Clubs Club, Main Main)
        {
            InitializeComponent();

            this.Name.Text = Club.Name;
            this.Address.Text = Club.Address;
            this.WorkTime.Text = Club.WorkTime;

            this.Main = Main;
            this.Club = Club;
        }

        private void EditClub(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentRole != "Admin") return;
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this.Main, this.Club));
        }

        private void DeleteClub(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentRole != "Admin") return;
            Main.AllClub.Clubs.Remove(Club);
            Main.AllClub.SaveChanges();
            // Обновляем список (перезагрузка с фильтром)
            Main.LoadClubs();
        }
    }
}