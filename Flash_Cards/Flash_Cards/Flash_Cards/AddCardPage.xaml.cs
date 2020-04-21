using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPage : ContentPage
    {
        private string name;

        public AddCardPage()
        {
            InitializeComponent();

            //SaveCardButton.Clicked += SaveCardButton_Clicked;
            MyEntry.TextChanged += MyEntry_TextChanged;
        }

        /*
        private async void SaveCardButton_Clicked(object sender, EventArgs e)
        {
            var MyEntry = new Entry { Text = "I am an Entry" };
            string text = MyEntry.Text;

            SecondPage.AddNewCard(name);
            await Navigation.PopAsync();
        } 
        */

        private void MyEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //labelTemp.Text = MyEntry.Text;
            name = e.NewTextValue;
        }
    }
}