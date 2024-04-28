using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Lib.Models
{
    public class Order
    {
        
        public int OrderID { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }

        public double Total { get; set; }
        public double Netto { get; set; }

        public DateTime Date { get; set; }

     }




}

