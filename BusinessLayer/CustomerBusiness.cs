using BusinessLayer.DTO;
using RepositoryLayer;
using RepositoryLayer.Entities;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer
{
    public class CustomerBusiness
    {
        private readonly CustomerRepository _repository;

        public CustomerBusiness(CustomerRepository repository)
        {
            _repository = repository;
        }

        // ✅ Mã hóa mật khẩu SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public bool Register(CustomerDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return false;

            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password)
            };

            return _repository.AddCustomer(customer);
        }

        public bool Login(string email, string password)
        {
            string hashed = HashPassword(password);
            return _repository.CheckLogin(email, hashed);
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = _repository.GetCustomerByEmail(email);
            if (user == null) return false;

            string oldHash = HashPassword(oldPassword);
            if (user.Password != oldHash) return false;

            user.Password = HashPassword(newPassword);
            return _repository.UpdateCustomer(user);
        }

        public CustomerDTO? GetCustomerByEmail(string email)
        {
            var customer = _repository.GetCustomerByEmail(email);
            if (customer == null) return null;

            return new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                Email = customer.Email,
                Password = "" // Không trả về password
            };
        }
    }
}
