using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPage : ContentPage
    {
        public AddCardPage()
        {
            InitializeComponent();

            SaveCardButton.Clicked += SaveCardButton_Clicked;
        }

        //INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }

        private void SaveCardButton_Clicked(object sender, EventArgs e)
        {
            string text = CatagoryTitle.Text;

            MessagingCenter.Send(this,"new", text);
         
            Navigation.PopAsync();
        } 
    }
}