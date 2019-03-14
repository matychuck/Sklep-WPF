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
using System.IO;
namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        
        public int test=3;
        public List<ClientData> listOfClients = new List<ClientData>();
        public List<ProductData> listOfProducts = new List<ProductData>();
        public List<OrderData> listOfOrders = new List<OrderData>();


        public MainWindow()
        {
            LoadOrders();
            InitializeComponent();
            LoadClients();
            LoadProducts();
            CatWindow cw = new CatWindow();
            if (cw.ShowDialog() == true)
            {

            }

        }

        public void LoadOrders()
        {
            StreamReader ordersFile = new StreamReader("BazaDanych/Zamowienia/Zamowienia.txt");
            string[] lines;

            int N = File.ReadAllLines("BazaDanych/Zamowienia/Zamowienia.txt").Length;

            
            for (int i = 0; i < N; i++)
            {
                lines = ordersFile.ReadLine().Split(' ');
                listOfOrders.Add(new OrderData(lines[0], lines[1], lines[2], lines[3]));

            }
            ordersFile.Close();

        }

        public void LoadProducts()
        {
            StreamReader productsFile = new StreamReader("BazaDanych/Produkty.txt");
            
            string[] lines;

            int N = File.ReadAllLines("BazaDanych/Produkty.txt").Length;


            for (int i = 0; i < N; i++)
            {
                lines = productsFile.ReadLine().Split(',');
                listOfProducts.Add(new ProductData(lines[0], int.Parse(lines[1]), int.Parse(lines[2]), int.Parse(lines[3]), lines[4], int.Parse(lines[5])));

            }
            productsFile.Close();
        }

        public void AddOrder(OrderData order)
        {
            listOfOrders.Add(new OrderData(order));
        }

        public void LoadClients()
        {
            StreamReader customesFile = new StreamReader("BazaDanych/Klienci.txt");
            InitializeComponent();
            string[] lines;

            int N = File.ReadAllLines("BazaDanych/Klienci.txt").Length;


            for (int i = 0; i < N; i++)
            {
                lines = customesFile.ReadLine().Split(' ');
                listOfClients.Add(new ClientData(lines[0], lines[1], lines[2], lines[3], lines[4], int.Parse(lines[5])));

            }
            customesFile.Close();
        }

        private void OpenSellerWindow(object sender, RoutedEventArgs e)
        {
            
            SellerWindow selWin = new SellerWindow();
            ((App)Application.Current).UpdateClientsToSeller(listOfClients);
            ((App)Application.Current).UpdateProducts(listOfProducts);
            ((App)Application.Current).UpdateOrders(listOfOrders);
            this.Hide();
            if (selWin.ShowDialog() == true) //kiedy zamknieto okno dialogowe 
            {
               
            }
            Close();
            
        }

        private void OpenLoginWindow(object sender, RoutedEventArgs e)
        {
            
            LoginWindow loginWin = new LoginWindow();
            ((App)Application.Current).Updatex(listOfClients);
            // Console.WriteLine(listOfOrders[0].Email);
            ((App)Application.Current).UpdateOrdersInLogin(listOfOrders);
            ((App)Application.Current).OvverrideList(listOfProducts);
            this.Hide();
            if (loginWin.ShowDialog() == true) //kiedy zamknieto okno dialogowe --to tylko do testow
            {
                MessageBox.Show("Powrot do okna glownego");
            }
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            StreamWriter saveClients = new StreamWriter("BazaDanych/Klienci.txt");
            //StreamWriter saveProducts = new StreamWriter("BazaDanych/Produkty.txt");
            StreamWriter saveOrders = new StreamWriter("BazaDanych/Zamowienia/Zamowienia.txt");
            for (int i = 0; i < listOfClients.Count; i++)
            {
                if (i == listOfClients.Count - 1)
                {
                    saveClients.Write(listOfClients[i].Name + " " + listOfClients[i].Surname + " " + listOfClients[i].Email + " " + listOfClients[i].Address + " " + listOfClients[i].Password + " " + listOfClients[i].HowManyOrders);
                }
                else
                {
                    saveClients.WriteLine(listOfClients[i].Name + " " + listOfClients[i].Surname + " " + listOfClients[i].Email + " " + listOfClients[i].Address + " " + listOfClients[i].Password + " " + listOfClients[i].HowManyOrders);
                }
            }
            saveClients.Close();

            //for (int i = 0; i < listOfProducts.Count; i++)
            //{
            //    if (i == listOfProducts.Count - 1)
            //    {
            //        saveProducts.Write(listOfProducts[i].Name + "," + listOfProducts[i].SerialNumber + "," + listOfProducts[i].Amount + "," + listOfProducts[i].Price + "," + listOfProducts[i].Size + "," + listOfProducts[i].Sex);
            //    }
            //    else
            //    {
            //        saveProducts.WriteLine(listOfProducts[i].Name + "," + listOfProducts[i].SerialNumber + "," + listOfProducts[i].Amount + "," + listOfProducts[i].Price + "," + listOfProducts[i].Size + "," + listOfProducts[i].Sex);
            //    }
            //}
            //saveProducts.Close();

            for (int i = 0; i < listOfOrders.Count; i++)
            {
                if (i == listOfOrders.Count - 1)
                {

                    saveOrders.Write(listOfOrders[i].Email + " " + listOfOrders[i].Amount + " " + listOfOrders[i].Status + " " + listOfOrders[i].OrdersFile);
                    
                }
                else
                {
                    saveOrders.WriteLine(listOfOrders[i].Email + " " + listOfOrders[i].Amount + " " + listOfOrders[i].Status + " " + listOfOrders[i].OrdersFile);


                }
            }
            saveOrders.Close();


        }
    }
}
