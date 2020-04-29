﻿using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;


namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IMainPageHelper
    {
        private readonly MainPageViewModel vm;
        private IMainPageHelper _viewHelper;

        public MainPage()
        {
            InitializeComponent();

            vm = new MainPageViewModel(this);
            BindingContext = vm;

            CatagoryListView.SelectionMode = ListViewSelectionMode.Single;
            CatagoryListView.ItemSelected += Catagory_ItemSelectedAsync;

            AddButton.Clicked += AddButton_ClickedAsync;

            MessagingCenter.Subscribe<AddCardPage, string>(this, "new", (sender, e) =>
            {
                DisplayAlert("Yay! New Catagory", e + " has been Added", "Ok");
                CatagoryCell newCard = new CatagoryCell(e, 0);

                vm.AddNewCard(newCard);
                //_viewHelper.ScrollToObject(newCard);
            });
        }

        private async void Catagory_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            CatagoryCell itemString = (CatagoryCell)e.SelectedItem;
            int selectedRow = e.SelectedItemIndex;

            //await vm.ItemSelectionChangedAsync(row: selectedRow, card: itemString);
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
    }
}