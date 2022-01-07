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
    public partial class SetDetailedPage : ContentPage
    {
        Set set;
        public SetDetailedPage(Set SelectedSet)
        {
            InitializeComponent();
            set = SelectedSet;

            SetImage.Source = set.SetImage;
            SetNameInput.Text = set.SetName;
            SetImageURLInput.Text = set.SetImage;
        }

        private void UpdateSetButton_Clicked(object sender, EventArgs e)
        {
            int updatedRows;
            set.SetName = SetNameInput.Text;
            set.SetImage = SetImageURLInput.Text;
            
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Set>();
                updatedRows = sQLiteConnection.Update(set);
            }

            if (updatedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "The set has been Updated", "Accept");
                Navigation.PushAsync(new Sets());
            }
        }

        private void DeleteSetButton_Clicked(object sender, EventArgs e)
        {
            int deletedRows;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Set>();
                deletedRows = sQLiteConnection.Delete(set);
            }

            if (deletedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Oh no!..", "Something went wrong!", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "The set has been deleted", "Accept");
                Navigation.PushAsync(new Stores());
            }
        }
    }
}