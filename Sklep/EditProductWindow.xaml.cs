using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public EditProductWindow()
        {
            InitializeComponent();
        }
        public ProductData Data;

        private void OnOK(object sender, RoutedEventArgs e)
        {
            int minPrice, amount, serialNb, sex;

            if (int.TryParse(txtSerialNb.Text, out serialNb) && int.TryParse(txtPrice.Text, out minPrice) && int.TryParse(txtAmount.Text, out amount) && int.TryParse(txtSex.Text, out sex))
            {
                if (serialNb > 0 && minPrice > 0 && amount > 0 && (sex == 1 || sex == 0))
                {
                    if (txtName.Text.Length > 0 && txtSerialNb.Text.Length > 0 && txtAmount.Text.Length > 0 && txtPrice.Text.Length > 0 && txtSex.Text.Length > 0 && txtSize.Text.Length > 0)
                    {
                        Data.Name = txtName.Text;
                        Data.SerialNumber = serialNb;
                        Data.Amount = amount;
                        Data.Price = minPrice;
                        Data.Size = txtSize.Text;
                        Data.Sex = sex;
                        DialogResult = true;
                        
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Wypełnij wszystkie dane produktu, aby zaakceptować.");
                    }
                }
                else
                {
                    MessageBox.Show("Wypełnij poprawnie dane produktu! \nNumer Seryjny, Cena i Ilość muszą być większe od 0.\nPłeć równa 0 oznacza rodzaj damski, a 1 rodzaj męski.");
                }
            }
            else
            {
                MessageBox.Show("Wypełnij poprawnie dane produktu, aby zaakceptować. Numer seryjny, ilość, cena i płeć muszą być liczbami naturalnymi");
            }

        }


        private void LoadProductData(object sender, RoutedEventArgs e)
        {
            try
            {
                txtName.Text = Data.Name;
                txtSerialNb.Text = Convert.ToString(Data.SerialNumber);
                txtAmount.Text = Convert.ToString(Data.Amount);
                txtPrice.Text = Convert.ToString(Data.Price);
                txtSize.Text = Data.Size;
                txtSex.Text = Convert.ToString(Data.Sex);
            }catch (Exception) { }

        
            
        }
    }
}
