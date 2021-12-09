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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            User user = new User();
            user.UserName = UsernameInput.Text;
            user.Password = PasswordInput.Text;
            user.ProfileImage = "PlaceholderProfilePic.png";

            SQLiteConnection sQLiteconnection = new SQLiteConnection(App.DatabaseLocation);
            sQLiteconnection.CreateTable<User>();
            int insertedRows = sQLiteconnection.Insert(user);
            sQLiteconnection.Close();

            if (insertedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your account has been created", "Accept");
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}