using CommunityToolkit.Mvvm.Messaging.Messages;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.Messages
{
    public class AddMessage : ValueChangedMessage<Product>
    {
        public AddMessage(Product value) : base(value)
        {
                
        }

    }
}
