﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Models;
using SalesManagerNew.Lib.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SalesManagerNew.Core.Messages;
using SalesManagerNew.Core.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class AddCustomerViewModel(IRepository repository, IAlertService alertService, ISound soundservice) : ObservableObject
    {

        

        IRepository _repository = repository;
        IAlertService _alertService = alertService;
        ISound _soundService = soundservice;

        [ObservableProperty]
        public string _firstname;

        [ObservableProperty]
        string _lastname;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        public string _email = "@";

        [ObservableProperty]
        public bool _protection = false;

        [ObservableProperty]
        public string plz;

        [ObservableProperty]
        public string ort;

        [ObservableProperty]
        public string adress;

        public bool CanAdd => this.Email.Contains("@");


        [RelayCommand(CanExecute = nameof(CanAdd))]
        void Add()
        {


            try
            {
       
                _repository.AddCustomer(this.Firstname, this.Lastname, this.Email, this.Protection, this.Plz, this.Ort, this.Adress);

                this.Firstname = "";
                this.Lastname = "";
                this.Email = "";
                this.Plz = "";
                this.Adress = "";
                this.Ort = "";

                _alertService.ShowAlert("Erfolg!",
                    "Der Kunde konnte hinzugefügt werden!");
                _soundService.PlayAudioSucess();
            }
            catch (Exception ex)
            {
                _alertService.ShowAlert("Fehler!",
                    "Es gab ein Problem bei hinzufügen des Kunden!");
                _soundService.PlayAudioFail();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }

           


        }

    }
}
