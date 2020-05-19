using System;
using System.ComponentModel;
using Xamarin.Forms;
using MyAzureLib;
using System.Threading.Tasks;

namespace Flash_Cards
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StartUp : ContentPage
    {
        public AzureLibrary AzureLibrary { get; }   

        public StartUp()
        {
            InitializeComponent();

            AzureLibrary = new AzureLibrary();

            CreateData();   
        }

        private async void CreateData()
        {
            await AzureLibrary.Go();
        }

        private async void ContButton_Clicked(object sender, EventArgs e)
        {
            var nextPage = new MainPage();
            await Navigation.PushAsync(nextPage, true);
        }

    }
}
