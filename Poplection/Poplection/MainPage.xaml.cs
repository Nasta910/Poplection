using Poplection.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Poplection
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(UsernameInput.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PasswordInput.Text);
            bool CorrectLoginDetails = false;
            
            int ValueLoggedInUserID = 999;

            if (isUsernameEmpty || isPasswordEmpty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
                {
                    sQLiteConnection.CreateTable<User>();
                    var Users = sQLiteConnection.Table<User>().ToList();
                    
                    foreach (User user in Users)
                    {
                        if(user.UserName == UsernameInput.Text && user.Password == PasswordInput.Text){
                            CorrectLoginDetails = true;
                            ValueLoggedInUserID = user.UserID;
                        }
                    }
                }
                if (CorrectLoginDetails)
                {
                    GlobalVariables.LoggedInUser = GetUser(ValueLoggedInUserID);
                    Navigation.PushAsync(new HomePage());
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Incorrect Info", "Your Username or Password is incorrect!", "Accept");
                }
            }
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
            
        }

        public User GetUser(int LoggedInUserID)
        {
            User LoggedInUser = null;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<User>();
                var Users = sQLiteConnection.Table<User>().ToList();
                foreach (User user in Users)
                {
                    if (LoggedInUserID == user.UserID)
                    {
                        LoggedInUser = user;
                    }
                }

            }
            return LoggedInUser;
        }

    }
}
