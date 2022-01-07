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
    public partial class AllPops : ContentPage
    {
        public AllPops()
        {
            InitializeComponent();      
        }

        protected override void OnAppearing()
        {
            //Write the code of your page here
            base.OnAppearing();
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Pop>();
                var Pops = sQLiteConnection.Table<Pop>().ToList();
                AllPopsListView.ItemsSource = Pops;
            }
        }

        private void AddNewPopButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewPop("AllPops"));
        }
    }
}