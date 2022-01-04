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
    public partial class AddPopToCollection : ContentPage
    {
        public AddPopToCollection()
        {
            InitializeComponent();
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Pop>();
                var Pops = sQLiteConnection.Table<Pop>().ToList();

                foreach (Pop pop in Pops)
                {
                    PopNamePicker.Items.Add(pop.PopName);
                }
            }

            PopDeliveredPicker.Items.Add("Yes");
            PopDeliveredPicker.Items.Add("No");
            PopProtectedPicker.Items.Add("Yes");
            PopProtectedPicker.Items.Add("No");
        }

        private void AddPopToCollectionButton_Clicked(object sender, EventArgs e)
        {
            PopsToUser PopToUser = new PopsToUser();
            bool bPickedDelivered = false;
            bool bPickedProtected = false;
            string PickedDelivered = PopDeliveredPicker.Items[PopDeliveredPicker.SelectedIndex];
            if(PickedDelivered == "Yes")
            {
                bPickedDelivered = true;
            }
            string PickedProtected = PopProtectedPicker.Items[PopProtectedPicker.SelectedIndex];
            if (PickedProtected == "Yes")
            {
                bPickedProtected = true;
            }
            string pickedPopName = PopNamePicker.Items[PopNamePicker.SelectedIndex];
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Pop>();
                var Pops = sQLiteConnection.Table<Pop>().ToList();

                foreach (Pop pop in Pops)
                {
                    if (pop.PopName == pickedPopName)
                    {
                        PopToUser.PopID = pop.PopID;
                    }
                }
            }

            PopToUser.UserID = GlobalVariables.LoggedInUser.UserID;
            PopToUser.PopPrice = float.Parse(PopPriceInput.Text);
            PopToUser.ShippingPrice = float.Parse(ShippingPriceInput.Text);
            PopToUser.OrderDate = OrderDateInput.Date;
            PopToUser.PopDelivered = bPickedDelivered;
            PopToUser.PopProtected = bPickedProtected;

            SQLiteConnection sQLiteconnection = new SQLiteConnection(App.DatabaseLocation);
            sQLiteconnection.CreateTable<PopsToUser>();
            int insertedRows = sQLiteconnection.Insert(PopToUser);
            sQLiteconnection.Close();

            if (insertedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your Pop has been added to your collection", "Accept");
                Navigation.PushAsync(new MyCollection());
            }
        }
    }
}