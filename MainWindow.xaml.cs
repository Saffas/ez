using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp11.Base;
using WpfApp11.Pages;

namespace WpfApp11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Product> products;
        public MainWindow()
        {
            InitializeComponent();
            ListProducts.ItemsSource = AppData.db.Product.ToList();
        }

        private void BtnAddInOrder_Click(object sender, RoutedEventArgs e)
        {
            if (products == null)
            {
                products = new List<Product>();
            }
           
            Product selectedProduct = (Product)ListProducts.SelectedItem;
            products.Add(selectedProduct);
            CartShoping.IsEnabled = true;
            MessageBox.Show("Продукт добавлен в корзину.");
        }

        

        private void AddInOrder_Click(object sender, RoutedEventArgs e)
        {
            if (products == null)
            {
                products = new List<Product>();
            }
            if ((sender as MenuItem).DataContext is Product product)
            {
                products.Add(product);
            }
            MessageBox.Show("Продукт добавлен в корзину.");
            CartShoping.IsEnabled = true;


        }

        private void ShowOrder_Click(object sender, RoutedEventArgs e)
        {
            CartShop cartShop = new CartShop(products);
            cartShop.ShowDialog();
            
        }
    }
}
