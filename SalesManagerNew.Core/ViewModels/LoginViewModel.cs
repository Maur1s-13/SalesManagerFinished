using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalesManagerNew.Core.Services;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Windows.Input;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class LoginViewModel (IRepository repository, IAlertService alertService, ISound soundService) : ObservableObject
    {
        IRepository _repository = repository;

        IAlertService _alertservice = alertService;

        ISound _soundService = soundService;

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Customer> _customers = [];

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Product> _products = [];

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Order> _orders = [];


        


        [RelayCommand]
        public void OpenFile()
        {
            string filePath = @"C:\Users\thoma\AppData\Local\Packages\com.companyname.salesmanagernew.app_9zz4h110yvjzm\LocalState\bestellungen.db";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening file: {ex.Message}");
            }
        }


        [ObservableProperty]
        Lib.Models.Customer _selectedCustomer = null;

        [ObservableProperty]
        Lib.Models.Product _selectedProduct = null;

        [ObservableProperty]
        Lib.Models.Order _selectedOrder = null;



        [ObservableProperty]
        public int? schluessel = null;

        [ObservableProperty]
        public int? showPassword = null;

        [ObservableProperty]
        public string entrypassword;

        [ObservableProperty]
        private string _password = "passwort123";

        [RelayCommand]
        public void Load()
        {
            this.Customers.Clear();
            this.Products.Clear();
            this.Orders.Clear();
            

            foreach (var customer in _repository.GetAllCustomers())
            {
                this.Customers.Add(customer);
            }

            foreach (var product in _repository.GetAllProducts())
            {
                this.Products.Add(product);
            }
            foreach (var order in _repository.GetAllOrders())
            {
                this.Orders.Add(order);
            }
          
        }

        [RelayCommand]
        public void SetValue()
        {
            if (Entrypassword == _password)
            {
                _soundService.PlayAudioSucess();
                Schluessel = 0;
                this.Entrypassword = "";
                
            }
            else
            {
                _soundService.PlayAudioFail();
                _alertservice.ShowAlert("Fehler!",
                    "Falsches Passwort!");
            }
        }

        [RelayCommand]
        public void SetEye()
        {
            ShowPassword = 0;
        }

        [RelayCommand]
        public void SetEyeBack()
        {
            ShowPassword = null;
        }

        [RelayCommand]
        public void SetValueback()
        {
            Schluessel = null;
            this.SelectedCustomer = null;
            this.SelectedProduct = null;
            this.SelectedOrder = null;
            ShowPassword = null;
        }

        [RelayCommand]
        public void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                _repository.DeleteCustomer(SelectedCustomer);
                _alertservice.ShowAlert("Erfolg!",
                    "Der Kunde wurde gelöscht!");
                this.Customers.Remove(SelectedCustomer);
                _soundService.PlayAudioSucess();
            }
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Der Kunde konnte nicht gelöscht werden, da keiner ausgewählt ist");
                _soundService.PlayAudioFail();
            }
        }

        [RelayCommand]
        public void DeleteOrder()
        {
            if (SelectedOrder != null)
            {
                _repository.DeleteOrder(SelectedOrder);
                _alertservice.ShowAlert("Erfolg!",
                    "Die Bestellung wurde gelöscht");
                this.Orders.Remove(SelectedOrder);
                _soundService.PlayAudioSucess();
            }
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Die Bestellung konnte nicht gelöscht werden, da keine ausgewählt ist");
                _soundService.PlayAudioFail();
            }
        }

        [RelayCommand]
        public void DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                _repository.DeleteProduct(SelectedProduct);
                _alertservice.ShowAlert("Erfolg!",
                    "Das Produkt wurde gelöscht!");
                this.Products.Remove(SelectedProduct);
                _soundService.PlayAudioSucess();
            }
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Das Produkt konnte nicht gelöscht werden, da keines ausgewählt ist");
                _soundService.PlayAudioFail();
            }
        }

        [RelayCommand]
        public void ChangeCustomer()
        {
           var customer = SelectedCustomer;

            if (customer != null)
            {
                _repository.UpdateCustomer(customer);

                this.Customers.Clear();
                foreach (var item in _repository.GetAllCustomers())
                {
                    Customers.Add(item);
                }

                _alertservice.ShowAlert("Erfolg",
                    "Der Kunde wurde bearbeitet");
                _soundService.PlayAudioSucess();
            }
            else
            {
                _alertservice.ShowAlert("Fehler!",
                    "Sie müssen einen Kunden auswählen!");
                _soundService.PlayAudioFail();
            }

                 
                    
                
            
        }

        [RelayCommand]
        public void ChangeOrder()
        {
            var order = SelectedOrder;

            if (order != null)
            {
                _repository.UpdateOrder(order);

                this.Orders.Clear();
                foreach (var item in _repository.GetAllOrders())
                {
                    Orders.Add(item);
                }


                _alertservice.ShowAlert("Erfolg",
                    "Die Bestellung wurde bearbeitet");
                _soundService.PlayAudioSucess();
            }
            else
            {
                _alertservice.ShowAlert("Fehler!",
                    "Sie müssen eine Bestellung auswählen!");
                _soundService.PlayAudioFail();
            }

             
                
            
        }

        [RelayCommand]
        public void ChangeProduct()
        {
               var product = SelectedProduct;

            if (product != null)
            {
                _repository.UpdateProduct(product);

                this.Products.Clear();

                foreach (var item in _repository.GetAllProducts())
                {
                    Products.Add(item);
                }


                _alertservice.ShowAlert("Erfolg",
                    "Das Produkt konnte bearbeitet werden");
                _soundService.PlayAudioSucess();
            }
            else
            {
                _alertservice.ShowAlert("Fehler!",
                    "Sie müssen ein Produkt auswählen!");
                _soundService.PlayAudioFail();
            }

           
        }



        [RelayCommand]
        public void ToggleFavoriteCustomer(Customer customer)
        {
            
                customer.Protection = !customer.Protection;
                 _repository.UpdateCustomer(customer);


            this.Customers.Clear();
            foreach (var item in _repository.GetAllCustomers())
            {
                Customers.Add(item);
            }

            

            _alertservice.ShowAlert("Erfolg",
                            "Der Status wurde verändert");
                    _soundService.PlayAudioSucess();
                    
                
            

        }

        [RelayCommand]
        public void ToggleFavoriteProduct(Product product)
        {
            
           product.Protection = !product.Protection;
           _repository.UpdateProduct(product);

            _repository.UpdateProduct(product);

            this.Products.Clear();

            foreach (var item in _repository.GetAllProducts())
            {
                Products.Add(item);
            }

            _alertservice.ShowAlert("Erfolg",
                            "Der Status wurde verändert");
                    _soundService.PlayAudioSucess();
                   

        }


    }
}
