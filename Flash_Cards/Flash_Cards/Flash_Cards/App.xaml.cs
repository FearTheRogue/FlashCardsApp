using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyAzureLib;

namespace Flash_Cards
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            StartUp firstPage = new StartUp();

            MainPage = new NavigationPage(firstPage);

            //AzureLibrary myLib = new AzureLibrary();
            //myLib.CheckDatabaseAsync();
            //myLib.CheckContainerAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
