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
    public partial class StoreDetailedPage : ContentPage
    {
        Store store;
        public StoreDetailedPage(Store selectedStore)
        {
            InitializeComponent();
            store = selectedStore;

            StoreImage.Source = store.StoreImage;
            StoreNameInput.Text = store.StoreName;
            StoreURLInput.Text = store.StoreURL;
            StoreImageURLInput.Text = store.StoreImage;
        }

        private void UpdateStoreButton_Clicked(object sender, EventArgs e)
        {
            int updatedRows;
            store.StoreName = StoreNameInput.Text;
            store.StoreURL = StoreURLInput.Text;
            store.StoreImage = StoreImageURLInput.Text;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Store>();
                updatedRows = sQLiteConnection.Update(store);
            }

            if (updatedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "The store has been Updated", "Accept");
                Navigation.PushAsync(new Stores());
            }
        }

        private void DeleteStoreButton_Clicked(object sender, EventArgs e)
        {
            int deletedRows;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Store>();
                deletedRows = sQLiteConnection.Delete(store);
            }

            if (deletedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Oh no!..", "Something went wrong!", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "The store has been deleted", "Accept");
                Navigation.PushAsync(new Stores());
            }
        }
    }
}