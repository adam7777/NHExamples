using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHib.Entities
{
    public class Store
    {
        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<Employee> Staff { get; set; }

        public Store()
        {
            Products = new List<Product>();
            Staff = new List<Employee>();
        }

        public virtual void AddProduct(Product prod)
        {
            prod.StoresStockedIn.Add(this);
            Products.Add(prod);
        }

        public virtual void AddEmployee(Employee emp)
        {
            emp.Store = this;
            Staff.Add(emp);
        }

    }
}
