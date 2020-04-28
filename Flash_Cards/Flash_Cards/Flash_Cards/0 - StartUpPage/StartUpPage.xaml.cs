using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Flash_Cards
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FirstPage : ContentPage
    {
        public FirstPage()
        {
            InitializeComponent();
        }

        private async void ContButton_Clicked(object sender, EventArgs e)
        {
            var nextPage = new MainPage();
            await Navigation.PushAsync(nextPage, true);
        }

    }
}
