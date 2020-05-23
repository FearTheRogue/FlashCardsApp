using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;
using System;
using System.Xml.Serialization;
using MyAzureLib;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IMainPageHelper
    {
        private readonly MainPageViewModel vm;

        public MainPage()
        {
            InitializeComponent();

            vm = new MainPageViewModel(this);
            BindingContext = vm;
            
            //GetCategory();
            CatagoryListView.SelectionMode = ListViewSelectionMode.Single;
            CatagoryListView.ItemSelected += Catagory_ItemSelectedAsync;

            //CatagoryListViewTemp.SelectionMode = ListViewSelectionMode.Single;
            //CatagoryListViewTemp.ItemSelected += Catagory_ItemSelectedAsync;

            AddButton.Clicked += AddButton_ClickedAsync;

            MessagingCenter.Subscribe<AddCardPage, string>(this, "new", (sender, e) =>
            {
                DisplayAlert("Yay! New Catagory", e + " has been Added", "Ok");
                CatagoryCell newCard = new CatagoryCell(e, 0);

                vm.AddNewCard(newCard);
            });

            
        }

        private async void Catagory_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            CatagoryCell itemString = (CatagoryCell)e.SelectedItem;
            await vm.ItemSelectionChangedAsync(card: itemString);
        }

        private async void GetCategory()
        {
            await vm.Temp();
        }

        INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup (string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }

        public async void AddButton_ClickedAsync(object sender, System.EventArgs e)
        {
            await vm.AddCardButtonAsync();
        }

        public void ScrollToObject(object obj)
        {
            CatagoryListView.ScrollTo(obj, ScrollToPosition.End, true);
        }
    }
}