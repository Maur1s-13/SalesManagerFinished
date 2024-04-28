using SalesManagerNew.Core.ViewModels;

namespace SalesManagerNew.App.Pages;

public partial class AddProduct : ContentPage
{
	public AddProduct(AddProductViewModel vw)
	{

        InitializeComponent();
        this.BindingContext = vw;
	}

   
}