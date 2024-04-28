using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.Services
{
    public interface IAlertService
    {
        void ShowAlert(string title, string message);
        Task ShowALertAsync(string title, string message);

    }
}
