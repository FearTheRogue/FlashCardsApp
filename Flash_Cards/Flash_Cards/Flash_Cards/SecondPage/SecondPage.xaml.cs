using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        public string[] src;

         static ObservableCollection<string> itemList;

        public SecondPage()
        {
            InitializeComponent();

            /*string sentence = "SOFT262 AINT255 Dinosaurs Food Netflix Books Sport OOP";
            src = sentence.Split(' ').ToArray<string>();
            MyList.ItemsSource = src; */

            itemList = new ObservableCollection<string>();
            itemList.Add("SOFT262");
            itemList.Add("AINT255");
            itemList.Add("Dinosaurs");
            itemList.Add("Food ");
            itemList.Add("Netflix");
            itemList.Add("Sport");
            itemList.Add("Oop");
            MyList.ItemsSource = itemList;

            MyList.ItemTapped += MyList_ItemTappedAsync;

            AddButton.Clicked += AddButton_Clicked;
            Add.Clicked += Add_Clicked;
        }

        internal static void AddNewCard(string newCardTitle)
        {
            itemList.Add(newCardTitle);
        }

        private async void MyList_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var nextPage = new ThirdPage();
            await Navigation.PushAsync(nextPage, true);

            nextPage.Title = (string)MyList.SelectedItem;
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            //Footer.Text = "Add button worked";

            var addPage = new AddCardPage();
            await Navigation.PushAsync(addPage, true);
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            itemList.Add("New");
        }

      
    }
}