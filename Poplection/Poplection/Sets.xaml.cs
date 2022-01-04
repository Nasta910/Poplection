﻿using Poplection.Models;
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
    public partial class Sets : ContentPage
    {
        public Sets()
        {
            InitializeComponent();

            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Set>();
                var Sets = sQLiteConnection.Table<Set>().ToList();
                SetsListView.ItemsSource = Sets;
            }
        }

        private void AddNewSetButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewSet());
        }
    }
}