using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window , INotifyPropertyChanged
    {

        public List<ProductData> listProducts;

        public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        int amount;
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    throw new ArgumentException("Ilość nie może byc pusta !");
                amount = value;
            }
        }

        int quantityInBasket;
        public int QuantityInBasket { get { return quantityInBasket; } set { quantityInBasket = value; } }
        public ClientData currentClient;
        
        private static ClientWindow client_window;
        public ProductData current_product => (ProductData)client_window.listOfProducts.Items[client_window.listOfProducts.SelectedIndex];
        public Dictionary<string, ProductData> availableAmount = new Dictionary<string, ProductData>();
        public List<string> availableSize = new List<string>();
        BindingExpression be;
        public ProductWindow() // zrobic zeby wudzialo te elementy z view a nie z listOfProducts. selected item
        {
            InitializeComponent();
            foreach(Window w in Application.Current.Windows)
            {
                if (w.Title == "Client Window")
                    client_window = (ClientWindow)w;
            }
            Amount = 1;
            be = quantity.GetBindingExpression(TextBox.TextProperty);
            UpdateBasketQuantity();
            Console.WriteLine(current_product.ImagePath);
            foreach (ProductData product in client_window.products)
            {
                if (product.Name == current_product.Name && !availableSize.Contains(product.Size))
                {
                    availableAmount.Add(product.Size, product);
                    availableSize.Add(product.Size);
                }

            }
            SizeBox.ItemsSource = availableSize;
        }
        private void UpdateBasketQuantity()
        {
            QuantityInBasket = 0;
            if (client_window.productsInBasket.Count > 0)
                foreach (var product in client_window.productsInBasket)
                {

                    QuantityInBasket += product.Amount;
                }



            BasketAmount.Text = QuantityInBasket.ToString();
        }
        private void SizeSelected(object sender, SelectionChangedEventArgs e)
        {
            foreach (ProductData product in client_window.products)
                if (availableSize[SizeBox.SelectedIndex].ToString() == product.Size.ToString() && product.Name == current_product.Name)
                {
                    showAmount.Text = "Dostepnych: " + product.Amount;

                }
            
            be.UpdateSource();
           
        }
        public void UpdateHERE(List<ProductData> list)
        {
            listProducts = list;
        }
            //public void UpdateAvailable(int a, int b)
            //{
            //    for(int i=0; i<client_window.productsInBasket.Count;i++)
            //    {
            //        if(client_window.productsInBasket[i].SerialNumber == a)
            //        {
            //            client_window.productsInBasket[i].Amount += b;
            //        }

            //    }

            //}
            //public void AfterDeleteProduct(string product, int returned)
            //{
            //    availableAmount[product].Amount += returned;
            //    showAmount.Text = Convert.ToString(availableAmount[product].Amount);
            //}

            public void UpdateAvailable(ProductData a,int b)
        {
            int guar=0;
            for(int i=0;i< client_window.productsInBasket.Count;i++)
            {
                if(client_window.productsInBasket[i] == a)
                {
                    guar = 1;
                    a.Amount += b;
                }
            }
            if(guar==1)
            {
                ((App)Application.Current).AnotherUpdate(client_window.productsInBasket);
            }
            
        }

        private void ReturnToSearch(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenBasket(object sender, RoutedEventArgs e)
        {
            
            Basket basket = new Basket(client_window.productsInBasket);
            ((App)Application.Current).OverrideBasket(currentClient);
            ((App)Application.Current).ToBasket(listProducts);
            basket.Owner = this;
            basket.ShowDialog();
            showAmount.Text = "Dostepnych: " + availableAmount[SizeBox.SelectedItem.ToString()].Amount;
            UpdateBasketQuantity();
            be.UpdateSource();
        }

        private void AddToBasket(object sender, RoutedEventArgs e)
        {
            if (!CheckIfExist(availableAmount[SizeBox.SelectedItem.ToString()]))
            {

                client_window.productsInBasket.Add(new ProductData(availableAmount[SizeBox.SelectedItem.ToString()]));
                client_window.productsInBasket[client_window.productsInBasket.Count - 1].Amount = int.Parse(quantity.Text.ToString());
                availableAmount[SizeBox.SelectedItem.ToString()].Amount -= int.Parse(quantity.Text.ToString());
                Console.WriteLine(availableAmount[SizeBox.SelectedItem.ToString()].Amount);
                showAmount.Text = "Dostepnych: " + availableAmount[SizeBox.SelectedItem.ToString()].Amount;
               
            }
            else
            {
                client_window.productsInBasket.Find(x => x.Name == availableAmount[SizeBox.SelectedItem.ToString()].Name && x.Size == availableAmount[SizeBox.SelectedItem.ToString()].Size).Amount += int.Parse(quantity.Text.ToString());
                availableAmount[SizeBox.SelectedItem.ToString()].Amount -= int.Parse(quantity.Text.ToString());
                showAmount.Text = "Dostepnych: " + availableAmount[SizeBox.SelectedItem.ToString()].Amount;
            }
            UpdateBasketQuantity();
            be.UpdateSource();
            
        }

        bool CheckIfExist(ProductData item)
        {
            Console.WriteLine(item.Amount + " " + item.Name + " " + item.Size);
            if (client_window.productsInBasket.Exists(x => x.Name == item.Name && x.Size == item.Size))
                return true;
            return false;
        }

        public void UpdateBasket(ClientData client)
        {
            currentClient = client;
        }
    }
}
