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
    /// Logika interakcji dla klasy ClientAccount.xaml
    /// </summary>
    public partial class ClientAccount : Window
    {
        ClientData k;
        List<OrderData> OwnOrdersList = new List<OrderData>();
        List<ProductData> OwnProductsList = new List<ProductData>();
        List<OrderData> tmp = new List<OrderData>();
        public ClientAccount()
        {
            InitializeComponent();
            
        }
        public void UpdateData(ClientData id)
        {
            k = id;
            clientName.Text = k.Name;
            clientSurname.Text = k.Surname;
            clientEmail.Text = k.Email;
            clientAddress.Text = k.Address;
            clientPassword.Text = k.Password;
            GetYourOrders();
        }

        public void GetYourOrders()
        {
            StreamReader reader = new StreamReader("BazaDanych/Zamowienia/Zamowienia.txt");
            string[] lines;
            string[] linesProduct;
            int N = File.ReadAllLines("BazaDanych/Zamowienia/Zamowienia.txt").Length;

            
            for (int i = 0; i < N; i++)
            {
                lines = reader.ReadLine().Split(' ');
                if(lines[0]==k.Email)
                {
                    OwnOrdersList.Add(new OrderData(lines[0], lines[1], lines[2], lines[3]));
                    StreamReader readerTwo = new StreamReader("BazaDanych/Zamowienia/" + lines[3] + ".txt");
                    int M = File.ReadAllLines("BazaDanych/Zamowienia/" + lines[3] + ".txt").Length;
                    for(int j=0;j<M;j++)
                    {
                        linesProduct = readerTwo.ReadLine().Split(',');
                        OwnProductsList.Add(new ProductData(linesProduct[0], int.Parse(linesProduct[1]), int.Parse(linesProduct[2]), decimal.Parse(linesProduct[3]), linesProduct[4], int.Parse(linesProduct[5])));

                    }
                    readerTwo.Close();

                }
              

            }
            reader.Close();
            
            ordersList.ItemsSource = OwnOrdersList;
            ordersList.Items.Refresh();
            productCounter.Text = Convert.ToString(OwnOrdersList.Count);
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            if (OwnOrdersList.Count > 0)
            {
                ViewOrder vb = new ViewOrder();
                ((App)Application.Current).UpdateProductsToView(OwnOrdersList[ordersList.SelectedIndex].listOfOrders);
                vb.ShowDialog();
            }
        }

        private void comeBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
