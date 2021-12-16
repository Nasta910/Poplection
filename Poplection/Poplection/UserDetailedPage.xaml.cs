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
    public partial class UserDetailedPage : ContentPage
    {
        User user;
        public UserDetailedPage(User selectedUser)
        {
            InitializeComponent();
            user = selectedUser;

            UserIDLabel.Text = user.UserID.ToString();
            UserNameInput.Text = user.UserName;
            UserPasswordInput.Text = user.Password;
            UserProfileImageNameLabel.Text = user.ProfileImage;
        }

        private void UpdateUserButton_Clicked(object sender, EventArgs e)
        {
            int updatedRows;
            user.UserName = UserNameInput.Text;
            user.Password = UserPasswordInput.Text;

            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<User>();
                updatedRows = sQLiteConnection.Update(user);
            }

            if (updatedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your account has been Updated", "Accept");
                Navigation.PushAsync(new ViewAccounts());
            }
        }

        private void DeleteUserButton_Clicked(object sender, EventArgs e)
        {
            int deletedRows;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<User>();
                deletedRows = sQLiteConnection.Delete(user);
            }

            if (deletedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Oh no!..", "Something went wrong!", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your account has been deleted", "Accept");
                Navigation.PushAsync(new ViewAccounts());
            }
        }
    }
}