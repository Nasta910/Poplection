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
    public partial class MyOrders : ContentPage
    {
        public MyOrders()
        {
            InitializeComponent();
            

        }

        protected override void OnAppearing()
        {
            //Write the code of your page here
            base.OnAppearing();
            List<int> AllOwnedPopsID = new List<int>();
            List<Pop> PopsInCollection = new List<Pop>();
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<PopsToUser>();
                var Pops = sQLiteConnection.Table<PopsToUser>().ToList();
                foreach (PopsToUser pop in Pops)
                {
                    if (pop.UserID == GlobalVariables.LoggedInUser.UserID && pop.PopDelivered == false)
                    {
                        AllOwnedPopsID.Add(pop.PopID);
                    }
                }
            }
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {

                sQLiteConnection.CreateTable<Pop>();
                var Pops = sQLiteConnection.Table<Pop>().ToList();
                foreach (Pop pop in Pops)
                {
                    if (AllOwnedPopsID.Contains(pop.PopID))
                    {
                        PopsInCollection.Add(pop);
                    }
                }
                PopsOrdersListView.ItemsSource = PopsInCollection;
            }
        }

        private void AddPopToCollectionButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPopToCollection("MyOrders"));
        }

        private void AddNewPopButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewPop("MyOrders"));
        }

        private void PopsOrdersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPop = PopsOrdersListView.SelectedItem as Pop;
            if (selectedPop != null)
            {
                Navigation.PushAsync(new PopFromCollectionDetailedPage(selectedPop, "MyOrders"));
            }
        }
    }
}