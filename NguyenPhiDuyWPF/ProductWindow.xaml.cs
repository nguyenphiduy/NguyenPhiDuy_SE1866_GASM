using BusinessLayer;
using BusinessLayer.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NguyenPhiDuyWPF
{
    public partial class ProductWindow : Window
    {
        private readonly ProductBusiness _productBusiness = new();
        private readonly CategoryBusiness _categoryBusiness = new();
        private int? _productId = null;
        private readonly Action _onSaveCallback;

        public ProductWindow(ProductDTO dto = null)
        {
            InitializeComponent();
            LoadCategories();

            if (dto != null)
            {
                _productId = dto.ProductID;
                txtName.Text = dto.Name;
                txtPrice.Text = dto.Price.ToString();
                txtDescription.Text = dto.Description;
                cbCategory.SelectedValue = dto.CategoryID;
            }
        }

        private void LoadCategories()
        {
            var categories = _categoryBusiness.GetAll();
            cbCategory.ItemsSource = categories;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                !decimal.TryParse(txtPrice.Text, out decimal price) ||
                cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            var dto = new ProductDTO
            {
                ProductID = _productId ?? 0,
                Name = txtName.Text,
                Price = price,
                Description = txtDescription.Text,
                CategoryID = (int)cbCategory.SelectedValue
            };

            if (_productId == null)
                _productBusiness.Add(dto);
            else
                _productBusiness.Update(dto);

            MessageBox.Show("Lưu thành công!");

            _onSaveCallback?.Invoke(); // ✅ GỌI lại MainWindow.LoadProducts()

            this.DialogResult = true;
            this.Close();
        }



        public ProductWindow(ProductDTO dto = null, Action onSave = null)
        {
            InitializeComponent();
            _onSaveCallback = onSave; // ✅ lưu lại callback
            LoadCategories();

            if (dto != null)
            {
                _productId = dto.ProductID;
                txtName.Text = dto.Name;
                txtPrice.Text = dto.Price.ToString();
                txtDescription.Text = dto.Description;
                cbCategory.SelectedValue = dto.CategoryID;
            }
        }

    }
}
