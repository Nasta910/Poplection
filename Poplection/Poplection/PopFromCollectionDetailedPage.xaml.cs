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
    public partial class PopFromCollectionDetailedPage : ContentPage
    {
        string FromWhichPage;

        Pop popToEdit;
        PopsToUser PopEdited;
        int SelectedPopID;
        int SelectedPopStoreID;
        float SelectedPopPrice;
        float SelectedPopShippingCosts;
        DateTime SelectedPopDate;
        bool SelectedPopDelivered;
        bool SelectedPopProtected;

        string sSelectedPopDelivered;
        string sSelectedPopProtected;

        string SelectedPopName;
        int SelectedPopNumber;
        int SelectedPopSetID;
        string SelectedPopImageURL;

        string SelectedSetName;
        string SelectedStoreName;

        int UserID = GlobalVariables.LoggedInUser.UserID;
        public PopFromCollectionDetailedPage(Pop SelectedPop, string startPage)
        {
            InitializeComponent();

            FromWhichPage = startPage;
            popToEdit = SelectedPop;


            SelectedPopName = popToEdit.PopName;
            SelectedPopNumber = popToEdit.PopNumber;
            SelectedPopSetID = popToEdit.SetID;
            SelectedPopImageURL = popToEdit.PopImage;

            SelectedPopID = popToEdit.PopID;
            

            PopDeliveredInput.Items.Add("Yes");
            PopDeliveredInput.Items.Add("No");
            PopProtectedInput.Items.Add("Yes");
            PopProtectedInput.Items.Add("No");
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<PopsToUser>();
                var Pops = sQLiteConnection.Table<PopsToUser>().ToList();
                foreach (PopsToUser pop in Pops)
                {
                    if (pop.PopID == SelectedPopID && pop.UserID == UserID)
                    {
                        PopEdited = pop;
                        SelectedPopStoreID = pop.StoreID;
                        SelectedPopDate = pop.OrderDate;
                        SelectedPopDelivered = pop.PopDelivered;
                        SelectedPopProtected = pop.PopProtected;
                        SelectedPopPrice = pop.PopPrice;
                        SelectedPopShippingCosts = pop.ShippingPrice;
                    }
                }

                sQLiteConnection.CreateTable<Set>();
                var Sets = sQLiteConnection.Table<Set>().ToList();
                foreach (Set set in Sets)
                {
                    if (set.SetID == SelectedPopSetID)
                    {
                        SelectedSetName = set.SetName;
                    }
                }

                sQLiteConnection.CreateTable<Store>();
                var Stores = sQLiteConnection.Table<Store>().ToList();
                foreach (Store store in Stores)
                {
                    PopStoreNameInput.Items.Add(store.StoreName);
                    if (store.StoreID == SelectedPopStoreID)
                    {
                        SelectedStoreName = store.StoreName;
                    }
                }
                if (SelectedPopDelivered)
                {
                    sSelectedPopDelivered = "Yes";
                }
                else
                {
                    sSelectedPopDelivered = "No";
                }

                if (SelectedPopProtected)
                {
                    sSelectedPopProtected = "Yes";
                }
                else
                {
                    sSelectedPopProtected = "No";
                }
                PopImageLabel.Source = SelectedPopImageURL;
                PopNameLabel.Text = SelectedPopName;
                PopNumberLabel.Text = "#" + SelectedPopNumber.ToString();
                PopSetNameLabel.Text = SelectedSetName;
                PopStoreNameInput.SelectedItem = SelectedStoreName;
                PopOrderDateInput.Date = SelectedPopDate;
                PopPriceInput.Text = SelectedPopPrice.ToString();
                PopShippingCostsInput.Text = SelectedPopShippingCosts.ToString();
                PopDeliveredInput.SelectedItem = sSelectedPopDelivered;
                PopProtectedInput.SelectedItem = sSelectedPopProtected;
            }
        }

        public void SaveButton_Clicked(object sender, EventArgs e)
        {
            string pickedStoreName;
            int updatedRows;
            if(PopStoreNameInput.SelectedIndex == -1)
            {
                pickedStoreName = SelectedStoreName;
            }
            else
            {
                pickedStoreName = PopStoreNameInput.Items[PopStoreNameInput.SelectedIndex];
            }
            
            
            bool bPickedDelivered = false;
            bool bPickedProtected = false;
            string PickedDelivered = PopDeliveredInput.Items[PopDeliveredInput.SelectedIndex];
            if (PickedDelivered == "Yes")
            {
                bPickedDelivered = true;
            }
            string PickedProtected = PopProtectedInput.Items[PopProtectedInput.SelectedIndex];
            if (PickedProtected == "Yes")
            {
                bPickedProtected = true;
            }

            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Store>();
                var Stores = sQLiteConnection.Table<Store>().ToList();

                foreach (Store store in Stores)
                {
                    if (store.StoreName == pickedStoreName)
                    {
                        PopEdited.StoreID = store.StoreID;
                    }
                }

                sQLiteConnection.CreateTable<PopsToUser>();
                updatedRows = sQLiteConnection.Update(PopEdited);
            }
            PopEdited.OrderDate = PopOrderDateInput.Date;
            PopEdited.PopPrice = float.Parse(PopPriceInput.Text);
            PopEdited.ShippingPrice = float.Parse(PopShippingCostsInput.Text);
            PopEdited.PopDelivered = bPickedDelivered;
            PopEdited.PopProtected = bPickedProtected;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<PopsToUser>();
                updatedRows = sQLiteConnection.Update(PopEdited);
            }

            if (updatedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your pop has been Updated", "Accept");
                if (FromWhichPage == "MyOrders")
                {
                    Navigation.PushAsync(new MyOrders());
                }
                else
                {
                    Navigation.PushAsync(new MyCollection());
                }
            }
        }

        private void DeletePopFromCollectionButton_Clicked(object sender, EventArgs e)
        {
            int deletedRows;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<PopsToUser>();
                deletedRows = sQLiteConnection.Delete(PopEdited);
            }

            if (deletedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Oh no!..", "Something went wrong!", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your Pop has been deleted", "Accept");
                Navigation.PushAsync(new MyCollection());
            }
        }
    }
}