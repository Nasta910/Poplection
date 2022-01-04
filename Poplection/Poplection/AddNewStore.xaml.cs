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
    public partial class AddNewStore : ContentPage
    {
        public AddNewStore()
        {
            InitializeComponent();
        }

        private void AddStoreButton_Clicked(object sender, EventArgs e)
        {
            Store store = new Store();
            store.StoreName = StoreNameInput.Text;
            store.StoreURL = StoreURLInput.Text;
            store.StoreImage = StoreImageURLInput.Text;


            SQLiteConnection sQLiteconnection = new SQLiteConnection(App.DatabaseLocation);
            sQLiteconnection.CreateTable<Store>();
            int insertedRows = sQLiteconnection.Insert(store);
            sQLiteconnection.Close();

            if (insertedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your Store has been Added", "Accept");
                Navigation.PushAsync(new Stores());
            }
        }
    }

    
}