using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
   public class ProductData
    {
        public string Name { get; set; }
        public int SerialNumber { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }    
        public string Size { get; set; }
        public int Sex { get; set; }
        public string ImagePath { get; set; }


        public ProductData(string name, int serialNumber, int amount, decimal price, string size, int sex)
        {
            Name = name;
            SerialNumber = serialNumber;
            Amount = amount;
            Price = price;
            Size = size;
            Sex = sex;
            ImagePath = name + ".jpg";
        }
        public ProductData(ProductData product)
        {
            this.Name = product.Name;
            this.SerialNumber = product.SerialNumber;
            this.Amount = product.Amount;
            this.Price = product.Price;
            this.Size = product.Size;
            this.Sex = product.Sex;
            this.ImagePath = product.ImagePath;
        }
    }
}
