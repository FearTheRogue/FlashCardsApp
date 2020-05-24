using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;
using MyAzureLib;
using System.Collections.Generic;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPage : ContentPage
    {
        public AzureLibrary azure;
        public List<CatagoryCell> cell = new List<CatagoryCell>();

        public AddCardPage()
        {
            InitializeComponent();

            azure = SingletonModel.SingletonInstance.Library;
            cell = SingletonModel.SingletonInstance.Categories;

            CategoryID.Text = "ID: catagory." + (cell.Count + 1);

            SaveCardButton.Clicked += SaveCardButton_Clicked;
        }

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }

        public async Task AddNewCard()
        {
            string text = CatagoryTitle.Text;

            await azure.AddCardToCategory("category." + (cell.Count + 1), text);
        }

        private void SaveCardButton_Clicked(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await AddNewCard();
            }).Wait();

            Navigation.PopAsync();
        } 
    }
}