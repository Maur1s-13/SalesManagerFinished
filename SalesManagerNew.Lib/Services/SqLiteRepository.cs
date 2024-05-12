using Microsoft.EntityFrameworkCore;
using SalesManagerNew.Lib.Interfaces;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Lib.Services
{
    public class SqLiteRepository : IRepository
    {

        string _path = string.Empty; //

        public SqLiteRepository(string path)
        {
            _path = path;
        }

        public void AddCustomer(string firstname, string lastname, string email, bool protection, string plz, string ort, string adress)
        {
            using(var db = new MyDbContext(_path))
            {
                db.Add(new Customer {Firstname = firstname, Lastname = lastname, Email = email, Protection = protection, PLZ = plz, Ort = ort, Adress = adress});
                db.SaveChanges();
            }
        }

        public void AddProductToCustomer(int customerId, int productId, DateTime date, int amount, double total, double netto)
        {
            using(var db = new MyDbContext(_path))
            {
                var product = db.Products.First(p => p.ProductId == productId);
                var customer = db.Customers.First(c => c.CustomerId == customerId);

                customer.Ordered
                    .Add(new Order { ProductId = product.ProductId, Date = date, Amount = amount, Total = total, Netto = netto });
                db.SaveChanges();
            }
        }

        public void AddCustomerToProduct(int customerId, int productId, DateTime date, int amount, double total, double netto)
        {
            using(var db = new MyDbContext(_path))
            {
                var customer = db.Customers.First(c => c.CustomerId==customerId);
                var product = db.Products.First(p => p.ProductId==productId);

                product.Ordered
                    .Add(new Order { CustomerId = customer.CustomerId, Date = date, Amount =  amount,  Total = total, Netto = netto });
                db.SaveChanges();
            }
        }


        public void AddProduct (double price, string productname, bool protection, int ust)
        {
            using(var db = new MyDbContext(_path))
            {
                db.Add(new Product { Price = price, ProductName = productname, Protection = protection, Ust = ust});   
                db.SaveChanges();
            }
        }

        public List<Product> GetAllProducts() 
        { 
            using( var db = new MyDbContext(_path))
            {
                return db.Products.ToList();
            }
        }

        

        public List<Customer> GetAllCustomers()
        {
            using (var db = new MyDbContext(_path))
            {
                return db.Customers.ToList();
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                using (var db = new MyDbContext(_path))
                {
                    return db.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                return new List<Order>();
            }
        }

        public double GetHighestProductPrice()
        {
            try
            {
                using (var db = new MyDbContext(_path))
                {
                    var highestPrice = db.Products.Max(p => p.Price);
                    return highestPrice;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0; 
            }
        }

        public double GetHighestNettoPrice()
        {
            try
            {
                using(var db = new MyDbContext(_path))
                {
                    var highesNetto = db.Orders.Max(n => n.Netto);
                    return highesNetto;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
                throw;
            }
        }

 

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDbContext(_path))
                {
                    var existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                    if (existingCustomer == null)
                    {
                        return false;
                    }

                    existingCustomer.Firstname = customer.Firstname;
                    existingCustomer.Lastname = customer.Lastname;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.PLZ = customer.PLZ;
                    existingCustomer.Ort = customer.Ort;
                    existingCustomer.Adress = customer.Adress;
                    existingCustomer.Protection = customer.Protection;

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                using(var context  = new MyDbContext(_path))
                {
                    var existingOrder = context.Orders.FirstOrDefault(o => o.OrderID == order.OrderID);
                       
                    if (existingOrder == null) 
                    {
                        return false;
                    }

                    existingOrder.CustomerId = order.CustomerId;
                    existingOrder.ProductId = order.ProductId;
                    existingOrder.Total = order.Total;
                    existingOrder.Date = order.Date;
                    existingOrder.Netto = order.Netto;
                    existingOrder.Amount = order.Amount;
                    

                    context.SaveChanges();


                }

                return true;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                using (var context = new MyDbContext(_path))
                {
                    var existingProduct = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

                    if (existingProduct == null)
                    {
                        return false;
                    }

                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Price = product.Price;
                    existingProduct.Ust = product.Ust;
                    existingProduct.Protection = product.Protection;

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteProduct(Product product) 
        {
            try
            {
                using (var context = new MyDbContext(_path))
                {
                    context.Remove(product);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDbContext(_path))
                {
                    context.Remove(customer);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteOrder(Order oder)
        {
            try
            {
                using(var context = new MyDbContext(_path))
                {
                    context.Remove(oder);
                    context.SaveChanges(); 
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                return false;
            }
        }


    }
}
