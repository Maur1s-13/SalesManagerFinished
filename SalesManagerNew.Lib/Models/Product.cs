using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Lib.Models
{
    public class Product
    {

        
        public int ProductId {  get; set; }  

        public double Price {  get; set; }

        public string ProductName { get; set; }

        public bool Protection { get; set; } = false;

        public int Ust {  get; set; }   

        public List<Order> Ordered { get; set; } = new();

        

    }
}
