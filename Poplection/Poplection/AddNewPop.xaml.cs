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
    public partial class AddNewPop : ContentPage
    {
        public AddNewPop()
        {
            InitializeComponent();
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Set>();
                var Sets = sQLiteConnection.Table<Set>().ToList();

                foreach (Set set in Sets)
                {
                    SetNamePicker.Items.Add(set.SetName);
                }
            }

        }

        private void AddNewPopButton_Clicked(object sender, EventArgs e)
        {
            Pop pop = new Pop();
            pop.PopName = PopNameInput.Text;
            pop.PopNumber = Int32.Parse(PopNumberInput.Text);
            pop.PopImage = PopImageURLInput.Text;
            string pickedSetName = SetNamePicker.Items[SetNamePicker.SelectedIndex];
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                sQLiteConnection.CreateTable<Set>();
                var Sets = sQLiteConnection.Table<Set>().ToList();

                foreach (Set set in Sets)
                {
                    if (set.SetName == pickedSetName)
                    {
                        pop.SetID = set.SetID;
                    }
                }
            }


            SQLiteConnection sQLiteconnection = new SQLiteConnection(App.DatabaseLocation);
            sQLiteconnection.CreateTable<Pop>();
            int insertedRows = sQLiteconnection.Insert(pop);
            sQLiteconnection.Close();

            if (insertedRows < 0)
            {
                Application.Current.MainPage.DisplayAlert("Missing Info", "You have to fill in all input fields", "Accept");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Done", "Your Pop has been Added", "Accept");
                Navigation.PushAsync(new AllPops());
            }
        }

        private void AllpopsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllPops());
        }
    }

    public class NumericValidationBehavior : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }


    }
}