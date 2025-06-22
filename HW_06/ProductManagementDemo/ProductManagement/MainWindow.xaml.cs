using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductService iProductService;
        private readonly ICategoryService iCategoryService;

        public MainWindow()
        {
            InitializeComponent();
            iProductService = new ProductService();
            iCategoryService = new CategoryService();
        }

        public void LoadCategoryList()
        {
            try
            {
                var catList = iCategoryService.GetCategories();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of categories");
            }
        }

        public void LoadProductList()
        {
            try
            {
                var productList = iProductService.GetProducts();
                dgData.ItemsSource = productList;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Error on load list of products");
            }
            finally
            {
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = new Product
                {
                    ProductName = txtProductName.Text,
                    UnitPrice = Decimal.Parse(txtPrice.Text),
                    UnitsInStock = short.Parse(txtUnitsInStock.Text),
                    CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString())
                };
                iProductService.SaveProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedIndex < 0) return;

            var row = (DataGridRow)
                dataGrid.ItemContainerGenerator
                        .ContainerFromIndex(dataGrid.SelectedIndex);

            var cell = dataGrid.Columns[0]
                              .GetCellContent(row)
                              .Parent as DataGridCell;

            var idText = ((TextBlock)cell.Content).Text;
            var product = iProductService.GetProductById(Int32.Parse(idText));

            txtProductID.Text = product.ProductId.ToString();
            txtProductName.Text = product.ProductName;
            txtPrice.Text = product.UnitPrice.ToString();
            txtUnitsInStock.Text = product.UnitsInStock.ToString();
            cboCategory.SelectedValue = product.CategoryId;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    var product = new Product
                    {
                        ProductId = Int32.Parse(txtProductID.Text),
                        ProductName = txtProductName.Text,
                        UnitPrice = Decimal.Parse(txtPrice.Text),
                        UnitsInStock = short.Parse(txtUnitsInStock.Text),
                        CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString())
                    };
                    iProductService.UpdateProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    var product = new Product
                    {
                        ProductId = Int32.Parse(txtProductID.Text),
                        ProductName = txtProductName.Text,
                        UnitPrice = Decimal.Parse(txtPrice.Text),
                        UnitsInStock = short.Parse(txtUnitsInStock.Text),
                        CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString())
                    };
                    iProductService.DeleteProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product !");
                }
            }
            catch (Exception)
            {
                // ignore
            }
            finally
            {
                LoadProductList();
            }
        }

        private void resetInput()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtUnitsInStock.Clear();
            cboCategory.SelectedIndex = -1;
        }
    }
}
