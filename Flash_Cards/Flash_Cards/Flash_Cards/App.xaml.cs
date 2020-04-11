using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flash_Cards
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FirstPage firstPage = new FirstPage();

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
