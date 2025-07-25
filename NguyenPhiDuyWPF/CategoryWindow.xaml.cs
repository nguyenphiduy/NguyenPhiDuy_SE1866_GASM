using BusinessLayer;
using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace NguyenPhiDuyWPF
{
    public partial class CategoryWindow : Window
    {
        private readonly CategoryBusiness _business = new();

        public CategoryWindow()
        {
            InitializeComponent();
            dgCategories.ItemsSource = _categoryList;
            LoadCategories();
        }




        private ObservableCollection<CategoryDTO> _categoryList = new();

        private void LoadCategories()
        {
            var categories = _business.GetAll();
            _categoryList = new ObservableCollection<CategoryDTO>(categories);
            dgCategories.ItemsSource = _categoryList; // gán lại ItemsSource
        }








        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddCategoryWindow(LoadCategories); // 👈 truyền callback
            addWindow.ShowDialog();
        }



        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var category = (sender as FrameworkElement)?.DataContext as CategoryDTO;
            if (category != null)
            {
                var editWindow = new AddCategoryWindow(LoadCategories, category); // 👈 truyền callback
                editWindow.ShowDialog();
            }
        }
        //private void BtnEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dgCategories.SelectedItem is CategoryDTO selected)
        //    {
        //        var win = new AddCategoryWindow(LoadCategories, selected); // ✅ luôn truyền LoadProducts
        //        win.ShowDialog(); // không cần kiểm tra result
        //    }
        //}


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var category = (sender as FrameworkElement)?.DataContext as CategoryDTO;
            if (category != null)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa category '{category.Name}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _business.Delete(category.CategoryID);
                        LoadCategories();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Không thể xóa Category này vì đang được sử dụng trong bảng khác (ví dụ: Product).\n\nChi tiết: " + ex.Message,
                            "Lỗi xóa dữ liệu",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
        }
        

    }
}
