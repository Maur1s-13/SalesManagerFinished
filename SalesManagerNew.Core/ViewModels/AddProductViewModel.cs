using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalesManagerNew.Core.Services;
using SalesManagerNew.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class AddProductViewModel (IRepository repository, IAlertService alertservice, ISound soundService) : ObservableObject, INotifyPropertyChanged
    {

        IRepository _repository = repository;

        IAlertService _alertservice = alertservice;

        ISound _soundService = soundService;

        [ObservableProperty]
        public string productName;

        [ObservableProperty]
        public double price;

        [ObservableProperty]
        public bool protection = false;

        [ObservableProperty]
        public List<int> saetze = new()
        {
            10,
            20,
            13
        };




        [ObservableProperty]
        public int ust;

        

        [RelayCommand]
        void Add()
        {
            try
            {
                 
                 _repository.AddProduct(this.Price, this.ProductName, this.Protection, this.Ust );
                this.Price = 0;
                this.ProductName = "";


                _alertservice.ShowAlert("Erfolg!",
                   "Das Produkt ist hinzugefügt worden!");
                _soundService.PlayAudioSucess();
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                _alertservice.ShowAlert("Fehler!",
                    "Beim Hinzufügen des Produktes ist ein Fehler aufgetreten!");
                _soundService.PlayAudioFail();
            }
        }


    }
}
