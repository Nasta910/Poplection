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
    public partial class AddNewSet : ContentPage
    {
        public AddNewSet()
        {
            InitializeComponent();
        }

        private void AddSetButton_Clicked(object sender, EventArgs e)
        {
            Set set = new Set();
            set.SetName = SetNameInput.Text;
            set.SetImage = SetImageURLInput.Text;
            

            SQLiteConnection sQLiteconnection = new SQLiteConnection(App.DatabaseLocation);
            sQLiteconnection.CreateTable<Set>();
            int insertedRows = sQLiteconnection.Insert(set);
            sQLiteconnection.Close();

            if (insertedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your set has been Added", "Accept");
                Navigation.PushAsync(new Sets());
            }
        }
    }
}