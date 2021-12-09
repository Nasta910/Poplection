using Poplection.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Poplection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAccounts : ContentPage
    {
        public ViewAccounts()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<User>();
                var Users = sQLiteConnection.Table<User>().ToList();
                UsersListView.ItemsSource = Users;
            }
        }

        private void UserListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedUser = UsersListView.SelectedItem as User;
            if (selectedUser != null)
            {
                Navigation.PushAsync(new UserDetailedPage(selectedUser));
            }
        }
    }
}