using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void UpdateProductsToView(List<ProductData> products)
        {
            foreach (Window w in Windows)
            {
                if (w is ViewOrder)
                {
                    ((ViewOrder)w).UpdateProductsToView(products);
                }
            }
        }

        
        

        public void UpdateOrders(List<OrderData> orders)
        {
            foreach (Window w in Windows)
            {
                if (w is SellerWindow)
                {
                    ((SellerWindow)w).UpdateOrders(orders);
                }
            }
        }
        public void SendClientsList(List<ClientData> clients)
        {
            foreach (Window w in Windows)
            {
                if (w is ClientWindow)
                {
                    ((ClientWindow)w).SendClientsList(clients);
                }
            }
        }

        public void UpdateOrdersList(List<OrderData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is ClientWindow)
                {
                    ((ClientWindow)w).UpdateOrdersList(list);
                }
            }
        }

        public void UpdateOrdersInLogin(List<OrderData> orders)
        {
            foreach (Window w in Windows)
            {
                if (w is LoginWindow)
                {
                    ((LoginWindow)w).UpdateOrdersInLogin(orders);
                }
            }
        }

        public void OverrideBasket(ClientData client)
        {
            foreach (Window w in Windows)
            {
                if (w is Basket)
                {
                    ((Basket)w).OverrideBasket(client);
                }
            }
        }
        public void UpdateBasket(ClientData client)
        {
            foreach (Window w in Windows)
            {
                if (w is ProductWindow)
                {
                    ((ProductWindow)w).UpdateBasket(client);
                }
            }
        }
        public void Update(ClientData id)
        {
            foreach (Window w in Windows)
            {
                if (w is ClientWindow)
                {
                    ((ClientWindow)w).Update(id);
                }
            }
        }

        public void Updatex(List<ClientData> id)
        {
            foreach (Window w in Windows)
            {
                if (w is LoginWindow)
                {
                    ((LoginWindow)w).Updatex(id);
                }
            }
        }

        public void UpdateProducts(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is SellerWindow)
                {
                    ((SellerWindow)w).UpdateSeller(list);
                }
            }
        }
        public void UpdateClientProducts(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is ClientWindow)
                {
                    ((ClientWindow)w).UpdateClientProducts(list);
                }
            }
        }

        public void OvverrideList(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is LoginWindow)
                {
                    ((LoginWindow)w).OverrideList(list);
                }
            }
        }

        public void UpdateData(ClientData id)
        {
            foreach (Window w in Windows)
            {
                if (w is ClientAccount)
                {
                    ((ClientAccount)w).UpdateData(id);
                }
            }
        }
        public void UpdateClientsToSeller(List<ClientData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is SellerWindow)
                {
                    ((SellerWindow)w).UpdateClientsToSeller(list);
                }
            }
        }

        public void UpdateHERE(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is ProductWindow)
                {
                    ((ProductWindow)w).UpdateHERE(list);
                }
            }
        }


        public void UpdateTHERE(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is Basket)
                {
                    ((Basket)w).UpdateTHERE(list);
                }
            }
        }

        public void ToBasket(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is Basket)
                {
                    ((Basket)w).ToBasket(list);
                }
            }
        }

        public void UpdateAvailable(ProductData a,int b)
        {
            foreach (Window w in Windows)
            {
                if (w is ProductWindow)
                {
                    ((ProductWindow)w).UpdateAvailable(a,b);
                }
            }
        }
        public void AnotherUpdate(List<ProductData> list)
        {
            foreach (Window w in Windows)
            {
                if (w is Basket)
                {
                    ((Basket)w).AnotherUpdate(list);
                }
            }
        }

        public void UpdateRegister(ClientData newClient)
        {
            foreach (Window w in Windows)
            {
                if (w is LoginWindow)
                {
                    ((LoginWindow)w).UpdateRegister(newClient);
                }
            }
        }
    }
}
