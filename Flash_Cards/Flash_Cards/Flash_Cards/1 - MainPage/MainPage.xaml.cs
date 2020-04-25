using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;


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

            CardListView.SelectionMode = ListViewSelectionMode.Single;
            CardListView.ItemSelected += CardListView_ItemSelectedAsync;

            AddButton.Clicked += AddButton_ClickedAsync;
            AddButtonCard.Clicked += AddButtonCard_ClickedAsync;
            //DataTemplate dataTemplate = new DataTemplate(() =>
            //{
            //    TextCell cell = new TextCell();

            //    MenuItem m1 = new MenuItem
            //    {
            //        Text = "Delete",
            //        IsDestructive = true
            //    };

            //    m1.SetBinding(MenuItem.CommandProperty, new Binding("DeleteCommand", source: this.BindingContext));
            //    m1.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            //    cell.ContextActions.Add(m1);
            //    return cell;
            //});

            //dataTemplate.SetBinding(TextCell.TextProperty, "Catagory");
            //dataTemplate.SetBinding(TextCell.DetailProperty, "CardCount");

            //CardListView.ItemTemplate = dataTemplate;
        }

        private async void CardListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            CustomCell itemString = (CustomCell)e.SelectedItem;
            int selectedRow = e.SelectedItemIndex;

            await vm.ItemSelectionChangedAsync(row: selectedRow, card: itemString);
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

        public void AddButtonCard_ClickedAsync(object sender, System.EventArgs e)
        {
            vm.Cards.Add(new CustomCell("New",0));
        }

        public void AddNewCard(CustomCell newCard)
        {
            vm.AddNewCard(newCard);
        }
    }
}