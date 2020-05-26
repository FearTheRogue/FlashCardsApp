using System.Threading.Tasks;
using MVVMBase;

namespace Flash_Cards
{
    public interface IMainPageHelper : IPage
    {
        Task TextPopup(string title, string message);

        void ScrollToObject(object obj);
    }
}
