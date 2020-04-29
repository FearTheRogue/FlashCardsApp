using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVMBase;
using System.Threading.Tasks;


namespace Flash_Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThirdPage : ContentPage, IMainPageHelper
    {
        private readonly ThirdPageViewModel vm;
        private IMainPageHelper _viewHelper;

        public ThirdPage()
        {
            InitializeComponent();

            vm = new ThirdPageViewModel(this);
            BindingContext = vm;
        }

        INavigation IPage.NavigationProxy => Navigation;

        public void ScrollToObject(object obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task TextPopup(string title, string message)
        {
            await DisplayAlert(title, message, "Dismiss");
        }

        Task IMainPageHelper.TextPopup(string title, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}