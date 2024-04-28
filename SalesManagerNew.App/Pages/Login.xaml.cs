using SalesManagerNew.Core.ViewModels;

namespace SalesManagerNew.App.Pages;

public partial class Login : ContentPage
{
	public Login(LoginViewModel vw)
	{
		InitializeComponent();
		this.BindingContext = vw;
	}
}