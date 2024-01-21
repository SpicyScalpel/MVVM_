using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MVVM_app
{
    public partial class MessagesPage : ContentPage
    {
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            // Возвращаемся обратно на страницу заполнения
            await Navigation.PopAsync();
        }
    }
}
