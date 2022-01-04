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
    public partial class MyCollection : ContentPage
    {
        public MyCollection()
        {
            InitializeComponent();
            List<int> AllOwnedPopsID = new List<int>();
            List<Pop> PopsInCollection = new List<Pop>();
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<PopsToUser>();
                var Pops = sQLiteConnection.Table<PopsToUser>().ToList();
                foreach(PopsToUser pop in Pops)
                {
                    if(pop.UserID == GlobalVariables.LoggedInUser.UserID)
                    {
                        AllOwnedPopsID.Add(pop.PopID);
                    }
                }
            }
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                
                sQLiteConnection.CreateTable<Pop>();
                var Pops = sQLiteConnection.Table<Pop>().ToList();
                foreach(Pop pop in Pops)
                {
                    if (AllOwnedPopsID.Contains(pop.PopID))
                    {
                        PopsInCollection.Add(pop);
                    }
                }
                PopsCollectionListView.ItemsSource = PopsInCollection;
            }
        }

        private void AddNewPopButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewPop());
        }

        private void AddPopToCollectionButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPopToCollection());
        }

        private void PopsCollectionListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPop = PopsCollectionListView.SelectedItem as Pop;
            if (selectedPop != null)
            {
                //To do
            }
        }
    }
}