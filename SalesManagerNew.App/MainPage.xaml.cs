using SalesManagerNew.Core.ViewModels;
using System.Windows.Input;

namespace SalesManagerNew.App
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage(MainViewModel vw)
        {
            InitializeComponent();
            this.BindingContext = vw;
        }

        


    }

}
