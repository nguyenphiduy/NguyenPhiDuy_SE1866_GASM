using BusinessLayer;
using BusinessLayer.DTO;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NguyenPhiDuyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }
        private readonly ProductBusiness _productBusiness = new();
        private List<ProductDTO> _allProducts;
        private List<CategoryDTO> _allCategories;


        //private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        //{
        //    CategoryWindow categoryWindow = new CategoryWindow(); // mở ở chế độ thêm mới
        //    categoryWindow.ShowDialog();
        //}
        
        private void LoadProducts()
        {
            var productBusiness = new ProductBusiness();
            var categoryBusiness = new CategoryBusiness();

            _allProducts = productBusiness.GetAll();
            _allCategories = categoryBusiness.GetAll();

            cbCategoryFilter.ItemsSource = _allCategories;
            cbCategoryFilter.SelectedIndex = -1;

            ApplyFilters();
        }




        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductWindow(null, LoadProducts); // ✅ luôn truyền LoadProducts
            win.ShowDialog(); // không cần check result nữa vì LoadProducts đã gọi bên trong
        }

        private void dgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgProducts.SelectedItem is ProductDTO selected)
            {
                var win = new ProductWindow(selected, LoadProducts); // ✅ luôn truyền LoadProducts
                win.ShowDialog(); // không cần kiểm tra result
            }
        }



        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts(); // ✅ gọi lại hàm đã có sẵn
        }


        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem is ProductDTO selected)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm '{selected.Name}'?",
                                             "Xác nhận xóa",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _productBusiness.Delete(selected.ProductID);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProducts(); // refresh lại danh sách
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        
        private void ApplyFilters()
        {
            if (_allProducts == null) return;

            int selectedCategoryId = cbCategoryFilter.SelectedValue is int id ? id : -1;
            string keyword = txtSearch.Text?.Trim().ToLower() ?? "";

            var filtered = _allProducts.Where(p =>
                (selectedCategoryId == -1 || p.CategoryID == selectedCategoryId) &&
                (string.IsNullOrWhiteSpace(keyword) || p.Name.ToLower().Contains(keyword))
            ).ToList();

            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = filtered;
        }
        private void cbCategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }
        
        private void btnViewCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new CategoryWindow();
            categoryWindow.ShowDialog();
            LoadProducts(); // Load lại sản phẩm sau khi thay đổi danh mục
        }

    }
}