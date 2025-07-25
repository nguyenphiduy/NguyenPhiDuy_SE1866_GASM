using BusinessLayer;
using Microsoft.Extensions.Configuration; // để đọc appsettings.json
using NguyenPhiDuyWPF;
using RepositoryLayer;
using System.IO;
using System.Windows;

namespace NguyenPhiDuyWPF
{
    public partial class LoginWindow : Window
    {
        private readonly CustomerBusiness _customerBusiness;
        private readonly string _adminEmail;
        private readonly string _adminPassword;

        public LoginWindow()
        {
            InitializeComponent();

            var dbContext = new AppDbContext();
            var repo = new CustomerRepository(dbContext);
            _customerBusiness = new CustomerBusiness(repo);

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _adminEmail = config["AdminAccount:Email"] ?? "admin@FUMiniTikiSystem.com";
            _adminPassword = config["AdminAccount:Password"] ?? "@@abc123@@";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password!", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (email.Equals(_adminEmail, StringComparison.OrdinalIgnoreCase)
                && password == _adminPassword)
            {
                MessageBox.Show("✅ Login as ADMIN successfully!", "Admin Login",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
                return;
            }

            bool loginSuccess = _customerBusiness.Login(email, password);

            if (loginSuccess)
            {
                CustomerWindow customer = new CustomerWindow();
                customer.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Invalid email or password!", "Login Failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWin = new RegisterWindow();
            registerWin.Show();
            this.Close();
        }
    }
}
