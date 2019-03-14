using System;
using System.Collections.Generic;
using System.Linq;
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
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window, INotifyPropertyChanged
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                OnPropertyChanged("MyFilteredItems");              
            }
        }

        public List<string> MyItems;
        public IEnumerable<string> MyFilteredItems
        {
            get
            {
                if (SearchText == null) return MyItems;
               
            
                return MyItems.Where(x => x.ToUpper().StartsWith(SearchText.ToUpper()));
            }
            
        }
        public ClientData currentClient;
        public List<OrderData> currentOrders;
        public List<ClientData> clientsTemp;
        public ClientWindow()
        {           
            InitializeComponent();
            SearchText = String.Empty;
            MyItems = new List<string>();           
            for (int i = 0; i < productsView.Count; i++)
            {               
                MyItems.Add(productsView[i].Name);
            }
            this.DataContext = this;          
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public List<ProductData> products = new List<ProductData>();
        public List<ProductData> productsView = new List<ProductData>();
        public List<ProductData> productsInBasket = new List<ProductData>();
      

        private void OpenBasket(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket(productsInBasket);           
            ((App)Application.Current).OverrideBasket(currentClient);
            ((App)Application.Current).UpdateTHERE(products);
            basket.Owner = this;
            basket.ShowDialog();
            
        }

        public void UpdateClientProducts(List<ProductData> list)
        {
            products = list;
            CompleteProductView();
            listOfProducts.ItemsSource = productsView;
        }
        public void UpdateOrdersList(List<OrderData> list)
        {
            currentOrders = list;
        }

        public void Update(ClientData id)
        {
            currentClient = id;
            //Console.WriteLine(currentClient.Email);
        }

        private void BackToMainWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
           
        private void ProductSelected(object sender, MouseButtonEventArgs e)
        {
            ProductWindow prodWin = new ProductWindow();
            ((App)Application.Current).UpdateBasket(currentClient);
            ((App)Application.Current).UpdateHERE(products);
            
            // listOfProducts.Items.Refresh()
            if (listOfProducts.SelectedItem != null)
            {
                if (prodWin.ShowDialog() == true) 
                {

                }
            }
        }
        void CompleteProductView()
        {
            foreach (ProductData product in products)
                if (productsView.Exists(x => x.Name == product.Name)) ;
                else
                    productsView.Add(product);

        }
        private void ShowClientAccount(object sender, RoutedEventArgs e)
        {
            ClientAccount cliAccountWin = new ClientAccount();
            ((App)Application.Current).UpdateData(currentClient);
            
            if (cliAccountWin.ShowDialog() == true)
            {
                
            }
        }

        // -----------------------------------WIDOKI--DANYCH----------------------------
        private ListCollectionView View 
        {
            get
            {  
                return (ListCollectionView)CollectionViewSource.GetDefaultView(listOfProducts.ItemsSource); 
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {          
            Char delimiter = ' ';
            ProductData pro;
            String[] substrings;
            View.Filter = delegate (object item)
            {
                pro = item as ProductData;
                substrings = pro.Name.Split(delimiter);
      
                foreach (var substring in substrings)
                {
                    if (substring.ToUpper().StartsWith(SearchText.ToUpper()))
                        return true;
                }
                    return false;              
             };
        }

        // --------------------------------GRUPOWANIE -----------------------------------
        private void GroupPrice(object sender, RoutedEventArgs e)
        {
            Progress pb = new Progress();
            if (pb.ShowDialog() == true)
            {

            }

            View.GroupDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
            PriceGrouper grouper = new PriceGrouper();
            grouper.GroupInterval = 10;
            View.GroupDescriptions.Add(new PropertyGroupDescription("Price", grouper));
            
            

        }

        private void GroupNone(object sender, RoutedEventArgs e)
        {
            View.GroupDescriptions.Clear();
        }

        // --------------------------------SORTOWANIE -----------------------------------

        private void SortName(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void SortPrice(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
        }

        private void SortNone(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.CustomSort = null;
        }


        // --------------------------------FILTROWANIE -----------------------------------
        private void FilterNone(object sender, RoutedEventArgs e)
        {
            View.Filter = null;
            sliderPrice.Value = 0;
            woman.IsChecked = false;
            man.IsChecked = false;           
           
        }

        private void Filter(object sender, RoutedEventArgs e) // albo pare przysickow bo inaczej nie ma sensu
        {
            decimal minimumPrice;          

            if ((man.IsChecked == true || woman.IsChecked == true) && Decimal.TryParse(priceForFilter.Text, out minimumPrice))
            {
                View.Filter = delegate (object item)
                {
                    ProductData pro = item as ProductData;
                    if (pro != null)
                    {
                        if (woman.IsChecked == true)
                        {
                            return (pro.Sex == 0 && pro.Price > minimumPrice);
                        }

                        if (man.IsChecked == true)
                        {
                            return (pro.Sex == 1 && pro.Price > minimumPrice);
                        }
                    }
                    return false;
                };
            }

            
           else if (Decimal.TryParse(priceForFilter.Text, out minimumPrice))
            {
                View.Filter = delegate (object item)
                {
                    ProductData pro = item as ProductData;
                    if (pro != null)
                    {
                        return (pro.Price > minimumPrice);
                    }
                    return false;
                };
            }
           
            else if (man.IsChecked == true || woman.IsChecked == true)
            {
                View.Filter = delegate (object item)
                {
                    ProductData pro = item as ProductData;
                    if (pro != null)
                    {
                        if (woman.IsChecked == true)
                        {
                            return (pro.Sex == 0);
                        }

                        if (man.IsChecked == true)
                        {
                            return (pro.Sex == 1);
                        }
                    }
                    return false;
                };
            }
        }
        public void SendClientsList(List<ClientData> clients)
        {
            clientsTemp = clients;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //StreamWriter saveClients = new StreamWriter("BazaDanych/Klienci.txt");
            //for (int i = 0; i < clientsTemp.Count; i++)
            //{
            //    if (i == clientsTemp.Count - 1)
            //    {
            //        saveClients.Write(clientsTemp[i].Name + " " + clientsTemp[i].Surname + " " + clientsTemp[i].Email + " " + clientsTemp[i].Address + " " + clientsTemp[i].Password + " " + clientsTemp[i].HowManyOrders);
            //    }
            //    else
            //    {
            //        saveClients.WriteLine(clientsTemp[i].Name + " " + clientsTemp[i].Surname + " " + clientsTemp[i].Email + " " + clientsTemp[i].Address + " " + clientsTemp[i].Password + " " + clientsTemp[i].HowManyOrders);
            //    }
            //}
            //saveClients.Close();
        }
    }
    }
