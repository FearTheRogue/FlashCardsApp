using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            ContButton.Clicked += ContButton_Clicked;
        }

        private async void ContButton_Clicked(object sender, EventArgs e)
        {
            //ContButton.Text = "Button Is Clicked";
            var nextPage = new SecondPage();
            await Navigation.PushAsync(nextPage, true);
        }

    }
}
