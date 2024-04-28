using SalesManagerNew.Core.ViewModels;
using System.Windows.Input;

namespace SalesManagerNew.App
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));


    }

}
