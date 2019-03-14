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
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // StreamWriter
        public List<ClientData> clientsTmp;
        public List<ProductData> productsTmp;
        public List<OrderData> ordersTmp;
        public LoginWindow()
        {
            InitializeComponent();

        }
        public void UpdateOrdersInLogin(List<OrderData> ords)
        {
            ordersTmp = ords;
        }


        public void OverrideList(List<ProductData> pd)
        {
            productsTmp = pd;
        }


        public void Updatex(List<ClientData> id)
        {
            clientsTmp = id;
        }

        private void IsPressedEnter(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                OpenClientWindow(sender, e);
            }
        }

        public void UpdateRegister(ClientData newClient)
        {
            clientsTmp.Add(newClient);
        }

        private void OpenClientWindow(object sender, RoutedEventArgs e)
        {
            string passwordTemp = null;
            bool guard = false;
            int memorizer = 0;
            if (login.Text == string.Empty)
            {
                warning.Text = "Podaj adres e-mail!";
            }
            else if (password.Password == string.Empty)
            {
                warning.Text = "Podaj hasło!";
            }
            else
            {
                for (int i = 0; i < clientsTmp.Count; i++)
                {
                    if (clientsTmp[i].Email == login.Text)
                    {
                        memorizer = i;
                        passwordTemp = clientsTmp[i].Password;
                        guard = true;
                    }
                    else
                    {
                        if (i == clientsTmp.Count - 1 && guard == false)
                        {
                            warning.Text = "Nie ma takiego klienta!";
                        }

                    }
                }
                if (guard)
                {
                    if (password.Password == passwordTemp)
                    {

                        ClientWindow clientWin = new ClientWindow();
                        ((App)Application.Current).Update(clientsTmp[memorizer]);
                        ((App)Application.Current).UpdateClientProducts(productsTmp);
                        ((App)Application.Current).UpdateOrdersList(ordersTmp);
                        ((App)Application.Current).SendClientsList(clientsTmp);

                        if (clientWin.ShowDialog() == true)
                        {

                        }

                    }
                    else
                    {
                        warning.Text = "Niepoprawne hasło!";
                    }
                }
            }


        }

        private void OpenSignUpWindow(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWin = new SignUpWindow();
            if (signUpWin.ShowDialog() == true)
            { }
        }
    }
}
