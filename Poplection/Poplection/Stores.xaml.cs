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
    public partial class Stores : ContentPage
    {
        public Stores()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //Write the code of your page here
            base.OnAppearing();
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Store>();
                var Stores = sQLiteConnection.Table<Store>().ToList();
                StoresListView.ItemsSource = Stores;
            }
        }

        private void AddNewStoreButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewStore());
        }

        private void StoresListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedStore = StoresListView.SelectedItem as Store;
            if (selectedStore != null)
            {
                Navigation.PushAsync(new StoreDetailedPage(selectedStore));
            }
        }
    }
}