using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;

namespace RepositoryLayer
{
    public class CustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Customer? GetCustomerByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public Customer? GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public bool AddCustomer(Customer customer)
        {
            if (GetCustomerByEmail(customer.Email) != null)
                return false; // Email đã tồn tại

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            return _context.SaveChanges() > 0;
        }

        public bool CheckLogin(string email, string hashedPassword)
        {
            var user = GetCustomerByEmail(email);
            return user != null && user.Password == hashedPassword;
        }
    }
}
