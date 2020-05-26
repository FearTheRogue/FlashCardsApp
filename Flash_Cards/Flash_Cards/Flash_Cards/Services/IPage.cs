using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MVVMBase
{
    public interface IPage
    {
        INavigation NavigationProxy { get; }
    }
}
