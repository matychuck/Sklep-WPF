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
    /// Logika interakcji dla klasy ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        public ProductData Data;

        private void OnOK(object sender, RoutedEventArgs e)
        {
            string destFile;
            string fileName;
            string filePath;
            string targetPath = @"\images";

            int minPrice, amount, serialNb, sex;

            if (int.TryParse(txtSerialNb.Text, out serialNb) && int.TryParse(txtPrice.Text, out minPrice) && int.TryParse(txtAmount.Text, out amount) && int.TryParse(txtSex.Text, out sex))
            {
                if (serialNb > 0 && minPrice > 0 && amount > 0 && (sex == 1 || sex == 0))
                {
                    if (txtName.Text.Length > 0 && txtSerialNb.Text.Length > 0 && txtAmount.Text.Length > 0 && txtPrice.Text.Length > 0 && txtSex.Text.Length > 0 && txtSize.Text.Length > 0)
                    {
                        MainWindow main = (MainWindow)Application.Current.MainWindow;
                        if (main.listOfProducts.Exists(x => x.Name == txtName.Text && x.Size == txtSize.Text))
                            MessageBox.Show("Istnieje produkt o takiej nazwie");
                        else
                        {
                            if (!main.listOfProducts.Exists(x => x.Name == txtName.Text))
                            {
                                AddImage newImage = new AddImage();
                                if (newImage.ShowDialog() == true)
                                { }

                                if (newImage.isCloseNormaly)
                                {
                                    destFile = newImage.destFile;
                                    fileName = newImage.fileName;
                                    filePath = newImage.filePath;

                                    if (Data == null) Data = new ProductData("", 0, 0, 0, "", 2);
                                                                   
                                    destFile = System.IO.Path.Combine(targetPath, fileName);
                                    System.IO.File.Copy(filePath, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + destFile, true);
                                    // Console.WriteLine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + destFile);
                                    //Console.WriteLine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + targetPath + "\\" + txtName.Text + ".jpg");
                                    File.Move(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + destFile, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + targetPath + "\\" + txtName.Text + ".jpg");

                                    newImage.Close();

                                    Data.Name = txtName.Text;
                                    Data.SerialNumber = serialNb;
                                    Data.Amount = amount;
                                    Data.Price = minPrice;
                                    Data.Size = txtSize.Text;
                                    Data.Sex = sex;
                                    DialogResult = true;
                                }
                                else
                                {
                                    MessageBox.Show("Zmiany nie zostana zapisane");
                                }
                            }
                            else // dla dodawania
                            {
                                if (Data == null) Data = new ProductData("", 0, 0, 0, "", 2);
                                Data.Name = txtName.Text;
                                Data.SerialNumber = serialNb;
                                Data.Amount = amount;
                                Data.Price = minPrice;
                                Data.Size = txtSize.Text;
                                Data.Sex = sex;
                                DialogResult = true;
                            } 
                            

                            Close();
                        }
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
              //jesli dodajemy: tworzymy obiekt ProductData         
            
        }
    }
}
