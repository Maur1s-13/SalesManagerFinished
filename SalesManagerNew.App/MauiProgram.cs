
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using SalesManagerNew.App.Pages;
using SalesManagerNew.App.Services;
using SalesManagerNew.Core.Services;
using SalesManagerNew.Core.ViewModels;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Services;
using Syncfusion.Maui.Core.Hosting;


namespace SalesManagerNew.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string path = FileSystem.AppDataDirectory;
            //string filename = "data.xml";
            //string filename = "data.sqlite";
            //string filename = "data.csv";
            string filename = "bestellungen.db";
            string fullpath = System.IO.Path.Combine(path, filename);
            System.Diagnostics.Debug.WriteLine("Pfad:");
            System.Diagnostics.Debug.WriteLine(path);
            //for commit
            builder.Services.AddSingleton<IRepository>(rep => new SqLiteRepository(fullpath));
            builder.Services.AddSingleton<ISound>(new AudioViewModel(AudioManager.Current));
            

#if DEBUG
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<AddProduct>();
            builder.Services.AddSingleton<AddProductViewModel>();

            builder.Services.AddSingleton<AddCustomerViewModel>();
            builder.Services.AddSingleton<AddCustomer>();

            

            builder.Services.AddSingleton<AddOrder>();
            builder.Services.AddSingleton<AddOrderViewModel>();

            builder.Services.AddSingleton<ReportGridViewModel>();
            builder.Services.AddSingleton<ReportOrders>();

            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<LoginViewModel>();

            builder.Services.AddSingleton<ReportPage>();
            builder.Services.AddSingleton<ReportViewModel>();

            builder.Services.AddSingleton<IAlertService, AlertService>();

            

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
