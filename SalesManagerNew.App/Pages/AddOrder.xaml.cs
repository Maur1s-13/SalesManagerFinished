using SalesManagerNew.Core.ViewModels;
using SalesManagerNew.Lib.Models;
using System.Collections.ObjectModel;




namespace SalesManagerNew.App.Pages;

public partial class AddOrder : ContentPage
{
	public AddOrder(AddOrderViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext = viewmodel;
        
    }

   
}