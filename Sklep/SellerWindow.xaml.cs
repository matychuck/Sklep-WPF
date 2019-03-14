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
using System.Windows.Xps.Packaging;
using System.IO;
using System.Windows.Xps;
using System.ComponentModel;

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy SellerWindow.xaml
    /// </summary>
    public partial class SellerWindow : Window
    {
        
        
        public List<OrderData> orderssList = new List<OrderData>();
        public List<ClientData> clientsTemp = new List<ClientData>();
        public List<ProductData> productTemp = new List<ProductData>();
        
        public SellerWindow()
        {
            InitializeComponent();                     
        }

        public void UpdateSeller(List<ProductData> list)
        {
            
            productTemp = list;
            productList.ItemsSource = productTemp;
            
        }


        /*
        * DLA LISTY KLIENTOW - clientList
       */
        public void UpdateClientsToSeller(List<ClientData> list)
        {
            clientsTemp = list;
            clientList.ItemsSource = clientsTemp;
        }
        
        private void CanDeleteClient(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            try
            {
                if (clientList.SelectedIndex > -1)
                {
                    e.CanExecute = true;
                }
            }
            catch (Exception) { }                           
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(clientsTemp[0].Email);
            string message = "Wybrany klient zostanie usunięty z bazy. Czy chcesz go usunąć? ";
            string title = "Usuwanie pozycji z listy";
            var result = MessageBox.Show(this, message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes && clientList.SelectedIndex > -1)
            {
                Progress pb = new Progress();
                if (pb.ShowDialog() == true)
                {

                }
                clientsTemp.RemoveAt(clientList.SelectedIndex);
                Console.WriteLine(clientList.SelectedIndex);
                clientList.ItemsSource = clientsTemp;
                clientList.Items.Refresh();           
            }
            if(result == MessageBoxResult.No)
            {
                clientList.Items.Refresh();
            }
        }


        /*
         * DLA STANU MAGAZYNU - productList
        */
        private void CanDeleteProduct(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            try
            {
                if (productList.SelectedIndex > -1)
                {
                    e.CanExecute = true;
                }
            }
            catch (Exception) { }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            string message = "Wybrana pozycja zostanie usunieta. Czy chcesz ją usunąć? ";
            string title = "Usuwanie pozycji z listy";
            var result = MessageBox.Show(this, message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Progress pb = new Progress();
                if (pb.ShowDialog() == true)
                {

                }
                productTemp.RemoveAt(productList.SelectedIndex);              
                productList.ItemsSource = productTemp;
                productList.Items.Refresh();               
            }
             if(result == MessageBoxResult.No   )
                {
                
                }
        }

        private ListCollectionView StoreView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(productTemp);
            }
        }

        private void SortName(object sender, RoutedEventArgs e)
        {
            StoreView.SortDescriptions.Clear();
            StoreView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void SortNone(object sender, RoutedEventArgs e)
        {
            StoreView.SortDescriptions.Clear();
            StoreView.CustomSort = null;
        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            ModalWindow newProduct = new ModalWindow();
            if (newProduct.ShowDialog() == true) 
            {
                productTemp.Add(newProduct.Data);
                productList.ItemsSource = productTemp;
                productList.Items.Refresh();             
            }
            if (SortNameCbI.IsSelected == true)  SortName(sender, e); 
            else SortNone(sender, e);
        }
      
        private void EditProduct(object sender, RoutedEventArgs e)
        {
            if (productList.SelectedIndex > -1)
            {
                EditProductWindow editedProduct = new EditProductWindow();

                editedProduct.Data = (ProductData)productList.SelectedItem; //przypisanie editedProduct do obecnie zaznaczonej pozycji
                if (editedProduct.ShowDialog() == true)
                {
                    Progress pb = new Progress();
                    if (pb.ShowDialog() == true)
                    {

                    }
                    productList.Items.Refresh();
                }
            }
        }

        public void UpdateOrders(List<OrderData> orders)
        {
            orderssList = orders;
            orderList.ItemsSource = orderssList;          
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            StreamWriter saveProducts = new StreamWriter("BazaDanych/Produkty.txt");           
           
            for (int i = 0; i < productTemp.Count; i++)
            {
                if (i == productTemp.Count - 1)
                {
                    saveProducts.Write(productTemp[i].Name + "," + productTemp[i].SerialNumber + "," + productTemp[i].Amount + "," + productTemp[i].Price + "," + productTemp[i].Size + "," + productTemp[i].Sex);
                }
                else
                {
                    saveProducts.WriteLine(productTemp[i].Name + "," + productTemp[i].SerialNumber + "," + productTemp[i].Amount + "," + productTemp[i].Price + "," + productTemp[i].Size + "," + productTemp[i].Sex);
                }
            }
            saveProducts.Close();

  
            
        }
        /*
         * DLA REALIZACJI ZAMOWIEN - orderList
        */

        private void CanDeleteOrder(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            try
            {
                if (orderList.SelectedIndex > -1)
                {
                    e.CanExecute = true;
                }
            }
            catch (Exception) { }
        }


        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            string message = "Wybrana pozycja zostanie usunieta. Czy chcesz ją usunąć? ";
            string title = "Usuwanie pozycji z listy";
            var result = MessageBox.Show(this, message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
             if (result == MessageBoxResult.Yes)
            {
                Progress pb = new Progress();
                if (pb.ShowDialog() == true)
                {

                }
                orderssList.RemoveAt(orderList.SelectedIndex);
                orderList.ItemsSource = orderssList;
                orderList.Items.Refresh();                          
            }
           
        }

        private void SendOrder(object sender, RoutedEventArgs e)
        {
            //!! powinno tu być sprawdzenie czy dany produkt juz nie został wysłany, można wysłać jedynie niewysłane produkty.
            //jesli zamowienie jest niewysłane - instrukcje ponizej:
            if (orderList.SelectedIndex > -1 && orderssList[orderList.SelectedIndex].Status != "wysłane")
            {
                string message = "Wybrana produkt zostanie wysłany do klienta. Czy chcesz go wysłać? ";
                string title = "Wysyłanie produktu";
                var result = MessageBox.Show(this, message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    //zmiana statusu na wyslano 
                    Progress pb = new Progress();
                    if (pb.ShowDialog() == true)
                    {

                    }
                    orderssList[orderList.SelectedIndex].Status = "wysłane";
                    orderList.ItemsSource = orderssList;
                    orderList.Items.Refresh();
                    MessageBox.Show("Zamówienie zostało wysłane");


                }
            }
            else
            {
                MessageBox.Show("Zamówienie zostało już zrealizowane!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

