using BusinessLayer;
using BusinessLayer.DTO;
using NguyenPhiDuyWPF;
using RepositoryLayer;
using System.Windows;
using System.Xml.Linq;

namespace NguyenPhiDuyWPF
{
    public partial class RegisterWindow : Window
    {
        private readonly CustomerBusiness _customerBusiness;

        public RegisterWindow()
        {
            InitializeComponent();

            var dbContext = new AppDbContext();
            var repo = new CustomerRepository(dbContext);
            _customerBusiness = new CustomerBusiness(repo);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("⚠ Please fill all fields!", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dto = new CustomerDTO
            {
                Name = name,
                Email = email,
                Password = password
            };

            bool registerSuccess = _customerBusiness.Register(dto);

            if (registerSuccess)
            {
                MessageBox.Show("Register successful", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                var loginWin = new LoginWindow();
                loginWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Email already exists! Please use another one.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWin = new LoginWindow();
            loginWin.Show();
            this.Close();
        }
    }
}
