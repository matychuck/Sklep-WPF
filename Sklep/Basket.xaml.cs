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
using System.IO;

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        public List<ProductData> lastList;
        public List<ProductData> productInBasket;
        public ClientData currentClient;
        public ProductWindow Products;
       
        int productsCost = 0,counter=0;
        int numberOfPositions = 0;
        public void AnotherUpdate(List<ProductData> list)
        {
            productInBasket = list;
            productsList.ItemsSource = productInBasket;
        }
        public Basket(List<ProductData> productInBasket)
        {
            InitializeComponent();
           
          productsList.ItemsSource = productInBasket;
            this.productInBasket = productInBasket;
            this.Products = Products;

            foreach (ProductData product in productInBasket)
            {
                productsCost += int.Parse(product.Price.ToString()) * int.Parse(product.Amount.ToString());
                counter += int.Parse(product.Amount.ToString());
            }
            allCost.Text = productsCost.ToString();
            productCounter.Text = counter.ToString();
            if(productInBasket.Count<1)
            {
                commitButton.IsEnabled = false;
            }
            else
            {
                commitButton.IsEnabled = true;
            }
        
    }


        
        private void Refresh(object sender, RoutedEventArgs e)
        {            
            productsList.Items.Refresh();
            productsCost = 0;
            counter = 0;

            foreach (ProductData product in productInBasket)
            {

                productsCost += int.Parse(product.Price.ToString()) * int.Parse(product.Amount.ToString());
                counter += int.Parse(product.Amount.ToString());
            }
            allCost.Text = productsCost.ToString();
            productCounter.Text = counter.ToString();
            if (productInBasket.Count < 1)
            {
                commitButton.IsEnabled = false;
            }
            else
            {
                commitButton.IsEnabled = true;
            }
            numberOfPositions = productInBasket.Count;
        }

        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is ClientWindow)
                {
                    ((ClientWindow)w).products.Find(x => x.Name == productInBasket[productsList.SelectedIndex].Name
                    && x.Size == productInBasket[productsList.SelectedIndex].Size).Amount += productInBasket[productsList.SelectedIndex].Amount; ;
                }
            }
            productInBasket.RemoveAt(productsList.SelectedIndex);
            Refresh(sender, e);
        }

        private void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (productsList.SelectedItem != null)
            {
                e.CanExecute = true;
            }
        }

       public void OverrideBasket(ClientData cd)
        {
            currentClient = cd;
            FirstName.Text = cd.Name;
            Surname.Text = cd.Surname;
            Email.Text = cd.Email;
            Address.Text = cd.Address;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             
            numberOfPositions = productInBasket.Count;
        }
        public void ToBasket(List<ProductData> list)
        {
            lastList = list;
        }

        public void UpdateTHERE(List<ProductData> list)
        {
            lastList = list;
        }

        private void GiveOrder(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zamowienie zostało przyjęte!");
            
            int amountOfOrders = ++currentClient.HowManyOrders;
            string fileName;
            fileName = currentClient.Email + amountOfOrders;
            StreamWriter saveOrder = new StreamWriter("BazaDanych/Zamowienia/"+fileName+".txt");
            for(int i=0;i< numberOfPositions; i++)
            {
                if(i == numberOfPositions - 1)
                {
                    saveOrder.Write(productInBasket[i].Name + "," + productInBasket[i].SerialNumber + "," + productInBasket[i].Amount + "," + productInBasket[i].Price + "," + productInBasket[i].Size + "," + productInBasket[i].Sex);
                }
                else
                {
                    saveOrder.WriteLine(productInBasket[i].Name + "," + productInBasket[i].SerialNumber + "," + productInBasket[i].Amount + "," + productInBasket[i].Price + "," + productInBasket[i].Size + "," + productInBasket[i].Sex);
                }
                
            }
            saveOrder.Close();
            OrderData od = new OrderData(currentClient.Email, Convert.ToString(numberOfPositions), "przyjęte",fileName);
            StreamReader tmp = new StreamReader("BazaDanych/Zamowienia/Zamowienia.txt");
            int N = File.ReadAllLines("BazaDanych/Zamowienia/Zamowienia.txt").Length;
            tmp.Close();
            StreamWriter orders = new StreamWriter("BazaDanych/Zamowienia/Zamowienia.txt",append: true);
            
            if(N>0)
            {
                orders.WriteLine();
                orders.Write(currentClient.Email + " " + Convert.ToString(numberOfPositions) + " " + "przyjęte" + " " + fileName);
            }
            else
            {
                orders.Write(currentClient.Email + " " + Convert.ToString(numberOfPositions) + " " + "przyjęte" + " " + fileName);
            }            
           
            orders.Close();
            ((MainWindow)Application.Current.MainWindow).AddOrder(od);
            productInBasket.Clear();
            productsList.ItemsSource = productInBasket;
            productsList.Items.Refresh();
            if (productInBasket.Count < 1)
            {
                commitButton.IsEnabled = false;
            }
            else
            {
                commitButton.IsEnabled = true;
            }


            StreamWriter saveProducts = new StreamWriter("BazaDanych/Produkty.txt");
            for (int i = 0; i < lastList.Count; i++)
            {
                if (i == lastList.Count - 1)
                {
                    saveProducts.Write(lastList[i].Name + "," + lastList[i].SerialNumber + "," + lastList[i].Amount + "," + lastList[i].Price + "," + lastList[i].Size + "," + lastList[i].Sex);
                }
                else
                {
                    saveProducts.WriteLine(lastList[i].Name + "," + lastList[i].SerialNumber + "," + lastList[i].Amount + "," + lastList[i].Price + "," + lastList[i].Size + "," + lastList[i].Sex);
                }
            }
            saveProducts.Close();
            Refresh(sender, e);
        }

        private void BackToShop(object sender, RoutedEventArgs e)
        {
            Close();
        }



        }
}
