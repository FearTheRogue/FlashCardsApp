using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;
using System;
using MyAzureLib;
using System.Threading;

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
            
            CatagoryListView.SelectionMode = ListViewSelectionMode.Single;
            CatagoryListView.ItemSelected += Catagory_ItemSelectedAsync;

            AddButton.Clicked += AddButton_ClickedAsync;
        }

        private async void Catagory_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            CatagoryCell itemString = (CatagoryCell)e.SelectedItem;

            await vm.ItemSelectionChangedAsync(card: itemString);
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

        private async void CatagoryListView_Refreshing(object sender, System.EventArgs e)
        { 
            CatagoryListView.ItemsSource = null;

            // Clearing the data from both collections
            vm._appCategories.Clear();
            vm._list.Clear();

            CatagoryListView.ItemsSource = vm._appCategories;

            // Retrieving data from DB
            await vm.GetCategoriesFromDB();

            // Inputting data to Collection
            vm.InputDataToCollection();

            CatagoryListView.IsRefreshing = false;
        }
    }
}