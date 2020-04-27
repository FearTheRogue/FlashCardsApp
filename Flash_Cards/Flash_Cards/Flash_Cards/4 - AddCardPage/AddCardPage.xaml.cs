using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using MVVMBase;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPage : ContentPage, IMainPageHelper
    {
        private readonly MainPage vvm;

        public AddCardPage()
        {
            InitializeComponent();

            vvm = new MainPage();
            BindingContext = vvm;

            SaveCardButton.Clicked += SaveCardButton_Clicked;
            
        }

        INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }

        private async void SaveCardButton_Clicked(object sender, EventArgs e)
        { 
            String text =(MyEntry.Text);

            //vvm.Cards.Add(new CustomCell(text,0));
            MessagingCenter.Send<>;
         
            Navigation.PopAsync();
        } 

        private void MyEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //labelTemp.Text = MyEntry.Text;
            //name = e.NewTextValue;
        }
    }
}