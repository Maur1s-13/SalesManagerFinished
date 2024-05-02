using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalesManagerNew.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace SalesManagerNew.Core.ViewModels
{
    public partial class MainViewModel(ISound soundService) : ObservableObject
    {

        ISound _soundService = soundService;

        [ObservableProperty]
        public bool mute = false;

        [RelayCommand]
        public void MuteSound()
        {
            if (Mute == false)
            {
                Mute = true;
                _soundService.MuteSound(Mute);
            }
            else
            {
                Mute = false;
                _soundService.MuteSound(Mute);
            }
        }
        
    }
}
