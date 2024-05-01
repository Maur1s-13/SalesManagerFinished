using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalesManagerNew.Core.Services;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class LoginViewModel (IRepository repository, IAlertService alertService) : ObservableObject
    {
        IRepository _repository = repository;

        IAlertService _alertservice = alertService;

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Customer> _customers = [];

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Product> _products = [];

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Order> _orders = [];



        [ObservableProperty]
        Lib.Models.Customer _selectedCustomer = null;

        [ObservableProperty]
        Lib.Models.Product _selectedProduct = null;

        [ObservableProperty]
        Lib.Models.Order _selectedOrder = null;



        [ObservableProperty]
        public int? schluessel = null;

        [ObservableProperty]
        public string entrypassword;

        private readonly string _password = "passwort123";

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
                Schluessel = 0;
                this.Entrypassword = "";
                
            }
            else
            {
                _alertservice.ShowAlert("Fehler!",
                    "Falsches Passwort!");
            }
        }

        [RelayCommand]
        public void SetValueback()
        {
            Schluessel = null;
            this.SelectedCustomer = null;
            this.SelectedProduct = null;
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
            }
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Der Kunde konnte nicht gelöscht werden");
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
            }
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Die Bestellung konnte nicht gelöscht werden");
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
            }
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Das Produkt konnte nicht gelöscht werden");
            }
        }

        [RelayCommand]
        public void ChangeCustomer(Customer customer)
        {
            customer = SelectedCustomer;

                var result = _repository.UpdateCustomer(customer);

                if (result)
                {
                    int pos = this.Customers.IndexOf(customer);

                    if (pos != 1)
                    {
                        this.Customers[pos] = customer;
                        _alertservice.ShowAlert("Erfolg",
                            "Der Kunde wurde bearbeitet");
                    }
                    else
                    {
                        _alertservice.ShowAlert("Fehler",
                            "Der Kunde konnte nicht bearbeitet werden");
                    }
                }
            
        }

        [RelayCommand]
        public void ChangeProduct(Product product)
        {
                product = SelectedProduct;

            
                var result = _repository.UpdateProduct(product);

                if (result)
                {
                    int pos = this.Products.IndexOf(product);

                    if (pos != 1)
                    {
                        this.Products[pos] = product;
                        _alertservice.ShowAlert("Erfolg",
                            "Das Produkt konnte bearbeitet werden");
                    }
                    else
                    {
                        _alertservice.ShowAlert("Fehler",
                            "Das Produkt konnte nicht bearbeitet werden");
                    }
                }
            
            
        }



        [RelayCommand]
        public void ToggleFavoriteCustomer(Customer customer)
        {
            
                customer.Protection = !customer.Protection;
                var result = _repository.ApplyProtectionCustomer(customer);
                if (result)
                {
                    int pos = this.Customers.IndexOf(customer);

                    if (pos != 1)
                    {
                        this.Customers[pos] = customer;
                        _alertservice.ShowAlert("Erfolg",
                            "Der Status wurde verändert");
                    }
                    else
                    {
                        _alertservice.ShowAlert("Fehler",
                            "Der Status konnte nicht verändert werden");
                    }
                }
            

        }

        [RelayCommand]
        public void ToggleFavoriteProduct(Product product)
        {
            
                product.Protection = !product.Protection;
                var result = _repository.UpdateProduct(product);
                if (result)
                {
                    int pos = this.Products.IndexOf(product);

                    if (pos != 1)
                    {
                        this.Products[pos] = product;
                        _alertservice.ShowAlert("Erfolg",
                            "Der Status wurde verändert");
                    }
                    else
                    {
                        _alertservice.ShowAlert("Fehler",
                            "Der Status konnte nicht verändert werden");
                    }
                }

        }


    }
}
