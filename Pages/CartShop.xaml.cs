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
using System.Windows.Shapes;
using WpfApp11.Base;

namespace WpfApp11.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartShop.xaml
    /// </summary>
    public partial class CartShop : Window
    {
        Order order;
        List<OrderProduct> orderProducts;

        decimal allProductPrice;
        decimal allProductAmount;
        public CartShop(List<Product> products)
        {
            InitializeComponent();

            order = new Order();
            order.id = AppData.db.Order.Max(o => o.id) + 1;
            orderProducts = new List<OrderProduct>();
            PicUpPointBox.ItemsSource = AppData.db.PicUpPoint.ToList();

            foreach (var product in products)
            {
                var existingOrderProduct = orderProducts.FirstOrDefault(p => p.idProduct == product.id);
                if (existingOrderProduct != null)
                {

                    existingOrderProduct.QuantityProductsOrder++;
                }
                else
                {
                    var ordersProduct = new OrderProduct()
                    {
                        idOrder = order.id,
                        Product = product,
                        idProduct = product.id,
                        QuantityProductsOrder = 1
                    };
                    orderProducts.Add(ordersProduct);
                }
            }

            CartShopView.ItemsSource = orderProducts;
            CalculShowPriceAmount(orderProducts);
        }

        private void CalculShowPriceAmount(List<OrderProduct> productsList)
        {
            allProductPrice = 0; // Сбрасываем значение перед расчетом новой суммы
            allProductAmount = 0; // Сбрасываем значение перед расчетом новой суммы

            foreach (var product in productsList)
            {
                decimal productTotalPrice = product.Product.Price * product.QuantityProductsOrder;
                allProductPrice += productTotalPrice;

                if (product.Product.Discount != null)
                {
                    decimal discountAmount = (decimal)(productTotalPrice - ((productTotalPrice / 100) * product.Product.Discount));
                    allProductAmount += discountAmount;
                }
            }

            ProductAmountTB.Text = $"{allProductAmount.ToString()}";
            ProductPriceTB.Text = $"{allProductPrice.ToString()}";
        }
        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is OrderProduct orderInProduct)
            {
                var ordersProduct = orderProducts.Find(x => x.idProduct == orderInProduct.idProduct);
                if (ordersProduct != null)
                {
                    ordersProduct.QuantityProductsOrder++;
                }
            }
            CalculShowPriceAmount(orderProducts);
            CartShopView.ItemsSource = orderProducts;
            CartShopView.Items.Refresh();
        }
        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is OrderProduct orderInProduct)
            {
                orderProducts.Where(x => x.idProduct == orderInProduct.idProduct).FirstOrDefault().QuantityProductsOrder--;
                if (orderProducts.Where(x => x.idProduct == orderInProduct.idProduct).FirstOrDefault().QuantityProductsOrder == 0)
                {
                    orderProducts.Remove(orderProducts.Where(x => x.idProduct == orderInProduct.idProduct).FirstOrDefault());
                }
            }
            CalculShowPriceAmount(orderProducts);
            CartShopView.ItemsSource = orderProducts;
            CartShopView.Items.Refresh();
        }

        private void FormOrder_Click(object sender, RoutedEventArgs e)
        {
            if (PicUpPointBox.SelectedItem != null)
            {
                var maxOrderCode = AppData.db.Order.Max(o => o.CodeDelivery);
                var newOrder = new Order()
                {
                    DateDelivery = DeliveryDate(orderProducts),
                    idPicUpPoint = ((PicUpPoint)PicUpPointBox.SelectedItem).id,
                    idStatus = 0,
                    CodeDelivery = maxOrderCode++,
                };
                AppData.db.Order.Add(newOrder);
                foreach (var item in orderProducts)
                {
                    var newOrderProduct = new OrderProduct()
                    {
                        idOrder = newOrder.id,
                        idProduct = item.idProduct,
                        QuantityProductsOrder = item.QuantityProductsOrder
                    };
                    AppData.db.OrderProduct.Add(newOrderProduct);
                }
                AppData.db.SaveChanges();

                // Формирование сообщения с информацией о заказе и списком товаров
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("Заказ оформлен");
                messageBuilder.AppendLine($"Номер заказа: {newOrder.id}");
                messageBuilder.AppendLine($"Дата доставки: {newOrder.DateDelivery}");
                messageBuilder.AppendLine($"Пункт выдачи: {((PicUpPoint)PicUpPointBox.SelectedItem).Name}");
                messageBuilder.AppendLine($"Код получения: {newOrder.CodeDelivery}");

                messageBuilder.AppendLine("\nСписок товаров в заказе:");
                foreach (var item in orderProducts)
                {
                    var product = AppData.db.Product.FirstOrDefault(p => p.id == item.idProduct);
                    messageBuilder.AppendLine($"{product.Name} - {item.QuantityProductsOrder} шт.");
                }

                string message = messageBuilder.ToString();

                MessageBox.Show(message, "Информация о заказе", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Выберите пункт выдачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private DateTime DeliveryDate(List<OrderProduct> oProductList)
        {
            if (oProductList.Count >= 6)
            {
                return DateTime.Now.AddDays(6);
            }
            else
            {
                foreach (var item in oProductList)
                {
                    if (item.Product.Quantity < item.QuantityProductsOrder)
                    {
                        return DateTime.Now.AddDays(6);
                    }
                }
            }
            return DateTime.Now.AddDays(3);

        }


        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            orderProducts.Clear();
            this.Close();
        }


    

       }
}
