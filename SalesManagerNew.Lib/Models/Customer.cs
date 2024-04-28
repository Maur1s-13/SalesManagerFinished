using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Lib.Models
{
    public class Customer
    {
        public int CustomerId {  get; set; } 

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public string PLZ { get; set; }

        public string Ort { get; set; }

        public string Adress { get; set; }

        public bool Protection {  get; set; } = false;

        public List<Order> Ordered { get; set; } = new();

        




    }
}
