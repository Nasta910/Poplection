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
    public partial class MyAccount : ContentPage
    {
        User user;
        public MyAccount()
        {
            InitializeComponent();
            user = GlobalVariables.LoggedInUser;
            UsernameInput.Text = user.UserName;
            PasswordInput.Text = user.Password;
            UserProfileImageURLInput.Text = user.ProfileImage;
            ProfilePicImage.Source = user.ProfileImage;
        }

        public void SaveButton_Clicked(object sender, EventArgs e)
        {
            int updatedRows;
            user.UserName = UsernameInput.Text;
            user.Password = PasswordInput.Text;
            user.ProfileImage = UserProfileImageURLInput.Text;
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
                GlobalVariables.LoggedInUser = user;
                Navigation.PushAsync(new HomePage());
            }
        }

        private void AllAccountsButton_Clicked(object sender, EventArgs e)
        {    
            Navigation.PushAsync(new ViewAccounts());   
        }
    }
}