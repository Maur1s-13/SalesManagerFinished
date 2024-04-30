using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagerNew.Lib.Services;


namespace SalesManagerNew.Lib.Interfaces;

public interface IRepository
{

    public void AddCustomer(string firstname, string lastname, string email, bool protection, string plz, string ort, string adress);

    public void AddProductToCustomer(int customerId, int productId, DateTime date, int amount, double total, double netto);

    public void AddCustomerToProduct(int customerId, int productId, DateTime date, int amount, double total, double netto);

    public void AddProduct(double price, string productname, bool protection, int ust);

    public List<Product> GetAllProducts();

    public List<Customer> GetAllCustomers();
   
    public bool UpdateCustomer(Customer customer);

    public bool ApplyProtectionCustomer(Customer customer);

    public bool ApplyProtectionProduct(Product product);

    public bool UpdateProduct(Product product);

    public bool DeleteCustomer(Customer customer);

    public bool DeleteProduct(Product product);






}
