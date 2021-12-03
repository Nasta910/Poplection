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
            bool isUsernameEmpty = string.IsNullOrEmpty(UsernameInput.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PasswordInput.Text);

            if (isUsernameEmpty || isPasswordEmpty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "you have to fill in all forms", "Accept");
            }
            else
            {
                Navigation.PushAsync(new MainPage());
            }
        }
    }
}