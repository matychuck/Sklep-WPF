using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sklep
{
    public class OrderData
    {
        public string Email { get; set; }
        public string Amount { get; set; }
        public string OrdersFile { get; set; }
        public string Status { get; set; }
        public StreamReader ordersFile;
        public List<ProductData> listOfOrders = new List<ProductData>();
        
        public OrderData(OrderData order)
        {
            Email = order.Email;
            Amount = order.Amount;
            Status = order.Status;
            OrdersFile = order.OrdersFile;          
        }

        public OrderData(string email, string amount, string status)
        {
            Email = email;
            Amount = amount;
            Status = status;
            
        }

        public OrderData(string email, string amount, string status,string f)
        {
            Email = email;
            Amount = amount;
            Status = status;
            OrdersFile = f;
            ordersFile = new StreamReader("BazaDanych/Zamowienia/" + f + ".txt");
            int N = File.ReadAllLines("BazaDanych/Zamowienia/" + f + ".txt").Length;
            string[] lines;
            for (int i = 0; i < N; i++)
            {
                lines = ordersFile.ReadLine().Split(',');
                listOfOrders.Add(new ProductData(lines[0], int.Parse(lines[1]), int.Parse(lines[2]), decimal.Parse(lines[3]), lines[4], int.Parse(lines[5])));

            }
            ordersFile.Close();
        }
    }
}
