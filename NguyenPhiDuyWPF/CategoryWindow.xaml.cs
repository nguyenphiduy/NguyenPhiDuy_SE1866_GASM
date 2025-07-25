using BusinessLayer;
using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
﻿using BusinessLayer.DTO;
using BusinessLayer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

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
        

        private string _imageBase64 = "";
        private int? _categoryId = null;

        public CategoryWindow(CategoryDTO category = null)
        {
            InitializeComponent();
            if (category != null)
            {
                _categoryId = category.CategoryID;
                txtName.Text = category.Name;
                txtDescription.Text = category.Description;
                _imageBase64 = category.Picture;
                imgPreview.Source = ConvertBase64ToImage(_imageBase64);
            }
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.png;*.bmp";

            if (dlg.ShowDialog() == true)
            {
                _imageBase64 = ConvertImageToBase64(dlg.FileName);
                imgPreview.Source = ConvertBase64ToImage(_imageBase64);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var dto = new CategoryDTO
            {
                CategoryID = _categoryId ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                Picture = _imageBase64
            };

            if (_categoryId == null)
                _business.Add(dto);
            else
                _business.Update(dto);

            MessageBox.Show("Lưu thành công");
            this.Close();
        }

        private string ConvertImageToBase64(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }

        private BitmapImage ConvertBase64ToImage(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return null;

            byte[] bytes = Convert.FromBase64String(base64);
            using MemoryStream ms = new(bytes);
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
