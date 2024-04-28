using SalesManagerNew.Core.ViewModels;

namespace SalesManagerNew.App.Pages;

public partial class ReportPage : ContentPage
{
	public ReportPage(ReportViewModel vw)
	{
		InitializeComponent();
		this.BindingContext = vw;
	}
}