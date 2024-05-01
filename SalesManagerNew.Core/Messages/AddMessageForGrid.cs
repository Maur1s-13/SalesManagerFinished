using CommunityToolkit.Mvvm.Messaging.Messages;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Core.Messages
{
    public class AddMessageForGrid : ValueChangedMessage<Order>
    {
        public AddMessageForGrid(Order value) :base(value) 
        {
            
        }
    }
}
