using SalesManagerNew.Core.ViewModels;

namespace SalesManagerNew.App.Pages;

public partial class ReportOrders : ContentPage
{
	public ReportOrders(ReportGridViewModel vw)
	{
		InitializeComponent();
		this.BindingContext = vw;
	}
}