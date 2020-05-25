using System;
using System.ComponentModel;
using Xamarin.Forms;
using MyAzureLib;
using System.Threading.Tasks;
using MVVMBase;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Flash_Cards
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StartUp : ContentPage
    {
        public AzureLibrary _library;
        //List<MyAzureLib.CardCatagories> cat = new List<MyAzureLib.CardCatagories>();

        //public AzureLibrary Library
        //{
        //    get => _library;
        //    set
        //    {
        //        if (_library != value)
        //        {
        //            _library = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public StartUp()
        {
            InitializeComponent();

            //Check();

            _library = SingletonModel.SingletonInstance.Library;

            _library = new AzureLibrary();

            SingletonModel.SingletonInstance.Library = _library;
        }

        private async Task WaitCreate()
        {
            await _library.GetContainer();
        }

        private async Task GetCategories()
        {
           // await _library.QueryItemsAsync(cat);
        }

        private async void ContButton_Clicked(object sender, EventArgs e)
        {  
            Spinner.IsRunning = true;

            await WaitCreate();
            //await GetCategories();

            Spinner.IsRunning = false;

            var nextPage = new MainPage();
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
