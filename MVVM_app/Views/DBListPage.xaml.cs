using MVVM_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBListPage : ContentPage
    {
        public DBListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            friendsList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }
        private async void friendsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Friend selectedfriend=(Friend)e.SelectedItem;
            DBFriendPage friendPage = new DBFriendPage();
            friendPage.BindingContext = selectedfriend;
            await Navigation.PushAsync(friendPage);
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            Friend friend=new Friend();
            DBFriendPage friendPage=new DBFriendPage();
            friendPage.BindingContext = friend;
            await Navigation.PushAsync(friendPage);

        }
    }
}