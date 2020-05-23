using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyAzureLib;
using System.Runtime.CompilerServices;

namespace Flash_Cards
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            StartUp firstPage = new StartUp();

            MainPage = new NavigationPage(firstPage);
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
