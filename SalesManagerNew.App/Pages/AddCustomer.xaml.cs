using SalesManagerNew.Core.ViewModels;

namespace SalesManagerNew.App.Pages;

public partial class AddCustomer : ContentPage
{
	public AddCustomer(AddCustomerViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext = viewmodel;	
	}
}