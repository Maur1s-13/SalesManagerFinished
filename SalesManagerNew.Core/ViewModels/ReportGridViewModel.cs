using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SalesManagerNew.Core.Messages;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class ReportGridViewModel : ObservableObject
    {

        IRepository _repository;

        string _path = "";

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Order> _orders = [];

        [ObservableProperty]
        double highesPrice;

        [ObservableProperty]
        double maximum;

        [ObservableProperty]
        Order order;

        public ReportGridViewModel(IRepository repository)
        {
            _repository = repository;

            WeakReferenceMessenger.Default.
                Register<AddMessageForGrid>(this, (r, m) =>
                {
                    //m.Value: unser Entry-Objekt
                    System.Diagnostics.Debug.WriteLine(m.Value);
                    //add to list
                    this.Orders.Add(m.Value);
                });

            this.HighesPrice = _repository.GetHighestNettoPrice();

            this.Maximum = this.HighesPrice + 40;

        }

        private bool _isLoaded = false;

        [RelayCommand]
        void LoadData()

        {
            if (!_isLoaded)
            {
                var entries = _repository.GetAllOrders();

                foreach (var entry in entries)
                {
                    Orders.Add(entry);
                }
                _isLoaded = true;
            }

        }



    }
}
