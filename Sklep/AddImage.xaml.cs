using Microsoft.Win32;
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
    /// Logika interakcji dla klasy AddImage.xaml
    /// </summary>
    public partial class AddImage : Window
    {
        public AddImage()
        {
            InitializeComponent();
            isCloseNormaly = false;
        }
        public string fileName=String.Empty;
        public string destFile;
        public string filePath;
        public bool isCloseNormaly;
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            string destFile;
            string targetPath = @"\images";
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                String[] substrings;
                Char delimiter ='\\';
                filePath = op.FileName;
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                substrings = op.FileName.Split(delimiter);
                foreach (var substring in substrings)
                    fileName = substring;
               
                //Console.WriteLine(fileName);
                
                if (!System.IO.Directory.Exists(targetPath))
                {
                    Console.WriteLine("Nie odnaleziono sciezki");
                }

                destFile = System.IO.Path.Combine(targetPath, op.FileName);
            }
            
            //System.IO.File.Copy(op.FileName, destFile, true);

        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            Console.WriteLine((Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()));
            Console.WriteLine(filePath);
            try
            {
                if (filePath != String.Empty)
                {
                    if (!filePath.Contains((Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString())))
                    {
                        isCloseNormaly = true;
                        Close();

                    }
                    else
                    {
                        MessageBox.Show("Nie mozesz dodać zdjęcia z tego folderu");
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception) { }
        }
        
        
        

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
