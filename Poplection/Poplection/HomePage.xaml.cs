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
    public partial class HomePage : TabbedPage
    {
        
        public HomePage()
        {
            InitializeComponent();
            AccountToolbarItem.Text = GlobalVariables.LoggedInUser.UserName;
        }

        private void AccountToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyAccount());

        }
    }
}