
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using MVVMBase;

namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage, IMainPageHelper
    {
        private MainPageViewModel vm;

        public SecondPage() 
        {
            InitializeComponent();

            vm = new MainPageViewModel(this);
            BindingContext = vm;

            CardListView.SelectionMode = ListViewSelectionMode.Single;
            CardListView.ItemSelected += CardListView_ItemSelectedAsync;
        }

        private async void CardListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            string itemString = (string)e.SelectedItem;
            int selectedRow = e.SelectedItemIndex;

            await vm.ItemSelectionChangedAsync(row: selectedRow, cardString: itemString);
        }

        INavigation IPage.NavigationProxy => Navigation;

        public async Task TextPopup (string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }
    }
}