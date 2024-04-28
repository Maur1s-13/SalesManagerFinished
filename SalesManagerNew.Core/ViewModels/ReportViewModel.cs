using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SalesManagerNew.Core.Messages;
using SalesManagerNew.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {
        IRepository _repository;

        [ObservableProperty]
        ObservableCollection<SalesManagerNew.Lib.Models.Product> _products = [];

        public ReportViewModel(IRepository repository)
        {
            _repository = repository;

            WeakReferenceMessenger.Default.
                Register<AddMessage>(this, (r, m) =>
                {
                    //m.Value: unser Entry-Objekt
                    System.Diagnostics.Debug.WriteLine(m.Value);
                    //add to list
                    this.Products.Add(m.Value);
                });

        }

        private bool _isLoaded = false;

        [RelayCommand]
        void LoadData()

        {
            if (!_isLoaded)
            {
                var entries = _repository.GetAllProducts();

                foreach (var entry in entries)
                {
                    Products.Add(entry);
                }
                _isLoaded = true;
            }

        }


    }
}
