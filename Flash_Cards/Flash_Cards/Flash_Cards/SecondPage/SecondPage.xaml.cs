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
    public partial class SecondPage : ContentPage
    {
        public string[] src;

        public SecondPage()
        {
            InitializeComponent();

            string sentence = "SOFT262 AINT255 Dinosaurs Food Netflix Books Sport OOP";
            src = sentence.Split(' ').ToArray<string>();
            MyList.ItemsSource = src;

            MyList.ItemTapped += MyList_ItemTappedAsync;
        }

        private async void MyList_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var nextPage = new ThirdPage();
            await Navigation.PushAsync(nextPage, true);

            nextPage.Title = (string)MyList.SelectedItem;
        }
    }
}