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
using System.Security;

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
            _Email = String.Empty; _Surname = String.Empty; _Name = String.Empty;
            _Adres = String.Empty;
            be[0] = Email.GetBindingExpression(TextBox.TextProperty);
            be[1] = Surname.GetBindingExpression(TextBox.TextProperty);
            be[2] = FirstName.GetBindingExpression(TextBox.TextProperty);
            be[3] = Adres.GetBindingExpression(TextBox.TextProperty);
            be[4] = Password.GetBindingExpression(TextBox.TextProperty);
            be[5] = Repeat.GetBindingExpression(TextBox.TextProperty);
        }
        //private void OpenClientWindow(object sender, RoutedEventArgs e)
        //{
        //    ClientWindow clientWin = new ClientWindow();
        //    if (clientWin.ShowDialog() == true) 
        //    {

        //    }
        //}
        private string email;
        public string _Email
        {
            get { return email; }
            set
            {
                
                email = value;
            }
        }
        private string name;
        public string _Name
        {
            get { return name; }
            set
            {

                name = value;
            }
        }
        private string surname;
        public string _Surname
        {
            get { return surname; }
            set
            {

                surname = value;
            }
        }
        private string adres;
        public string _Adres
        {
            get { return adres; }
            set
            {

                adres = value;
            }
        }
        private SecureString password;
        public SecureString _Password
        {
            get { return password; }
            set
            {

                password = value;
            }
        }
        private SecureString rpassword;
        public SecureString _RepeatPasword
        {
            get { return rpassword; }
            set
            {

                rpassword = value;
            }
        }
        BindingExpression[] be=new BindingExpression[6];
        public void Register(object sender, RoutedEventArgs e)
        {   for(int i=0;i<4;i++)
            be[i].UpdateSource();
            if(FirstName.Text!=String.Empty && Surname.Text != String.Empty && Email.Text != String.Empty && Adres.Text != String.Empty && Password.Password != String.Empty)

                if (Repeat.Password == Password.Password && Password.Password.Length > 3)
                {
                    StreamWriter registration = new StreamWriter("BazaDanych/Klienci.txt", append: true);
                    ((App)Application.Current).UpdateRegister(new ClientData(FirstName.Text, Surname.Text, Email.Text, Adres.Text, Password.Password));
                    registration.WriteLine();
                    registration.Write(FirstName.Text + " " + Surname.Text + " " + _Email + " " + Adres.Text + " " + Password.Password);
                    Progress pb = new Progress();
                    if (pb.ShowDialog() == true)
                    {

                    }
                    this.Close();
                    registration.Close();
                }

            Warning.Text = String.Empty;

            if (Repeat.Password != Password.Password)
                Warning.Text = "Hasła nie są takie same";
            if (Password.Password == String.Empty || Repeat.Password == String.Empty)
                Warning.Text = "Brak hasła";
            else if (Password.Password.Length < 4 || Repeat.Password.Length < 4)
                Warning.Text = "Hasło za krótkie";

        }


    }
}
