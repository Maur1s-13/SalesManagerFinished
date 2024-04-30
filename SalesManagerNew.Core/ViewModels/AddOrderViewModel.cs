using CommunityToolkit.Mvvm.ComponentModel;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagerNew.Lib.Models;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using SalesManagerNew.Core.Services;
using CommunityToolkit.Mvvm.Messaging;
using SalesManagerNew.Core.Messages;
using System.ComponentModel;
using Syncfusion.Maui.Picker;
using System.Runtime.CompilerServices;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class AddOrderViewModel(IRepository repository, IAlertService alertService) : ObservableObject
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
        public int _amount = 0;

        [ObservableProperty]
        public int _price = 0;

        [ObservableProperty]
        public DateTime _date = DateTime.Now;

        [ObservableProperty]
        string description = string.Empty;

        [ObservableProperty]
        public string customerId = string.Empty;

        [ObservableProperty]
        public string productId = string.Empty;

        [ObservableProperty]
        public double total = 0;

        [ObservableProperty]
        public double netto = 0;

        [RelayCommand]
        public void AddOrder()
        {

            try
            {
                if (SelectedCustomer != null && SelectedProduct != null)
                {
                    this.Total = this.SelectedProduct.Price * this.Amount;

                    if (this.SelectedProduct.Ust == 20)
                    {
                        this.Netto = this.Total * 0.8;
                    }
                    else if (this.SelectedProduct.Ust == 13)
                    {
                        this.Netto = this.Total * 0.87;
                    }
                    else if (this.SelectedProduct.Ust == 10)
                    {
                        this.Netto = this.Total * 0.9;
                    }
                    

                     _repository.AddProductToCustomer(this.SelectedCustomer.CustomerId, this.SelectedProduct.ProductId, this.Date, this.Amount, this.Total, this.Netto);
                    _alertservice.ShowAlert("Erfolg",
                        "Die Bestellung konnnte Erfolgreich hinzugefügt werden");
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                _alertservice.ShowAlert("Fehler!",
                    "Beim Hinzufügen der Bestellung ist ein Fehler aufgetreten!");
            }

        }



        [RelayCommand]
        public void Load()
        {
            this.Customers.Clear();
            this.Products.Clear();
            

            foreach(var customer in _repository.GetAllCustomers())
            {
                this.Customers.Add(customer);
            }

            foreach(var product in _repository.GetAllProducts())
            {
                this.Products.Add(product);
            }
            


        }

        

        [RelayCommand]
        public void ChangeCustomer(Customer customer)
        {
            customer = SelectedCustomer;

            if (customer.Protection == false)
            {
                
                
               var result =  _repository.UpdateCustomer(customer);

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
            else
            {
                _alertservice.ShowAlert("Fehler!",
                    "Dieser Kunde ist schreibgeschützt!");
            }
        }

        [RelayCommand]
        public void ChangeProduct(Product product)
        {
            product = SelectedProduct;

            if (product.Protection == false)
            {
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
            else
            {
                _alertservice.ShowAlert("Fehler!",
                    "Dieses Produkt ist schreibgeschützt!");
            }
        }


        [RelayCommand]
        public void ToggleFavoriteCustomer(Customer customer)
        {
            if (customer.Protection == false)
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
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Sie haben keine Berechtigung den Schreibschutz zu entfernen!");
            }
   
        }

        [RelayCommand]
        public void ToggleFavoriteProduct(Product product)
        {
            if (product.Protection == false)
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
            else
            {
                _alertservice.ShowAlert("Fehler",
                    "Sie haben keine Berechtigung den Schreibschutz zu entfernen!");
            }






        }






    }
    }

