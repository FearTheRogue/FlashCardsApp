using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;
using MyAzureLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPage : ContentPage
    {
        public AzureLibrary azure;
        public ObservableCollection<CatagoryCell> cell = new ObservableCollection<CatagoryCell>();

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
           
            await azure.AddCardToCategory("catagory." + (cell.Count + 1), text);
        }

        private async void SaveCardButton_Clicked(object sender, EventArgs e)
        {
            if (CatagoryTitle.Text == null)
            {
                await DisplayAlert("Oops!", "You must enter a category title", "Ok");
            }
            else
            {
                await AddNewCard();
                await Navigation.PopAsync();
            }  
        } 
    }
}