using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sklep
{
    public class ClientData
    {
        public string Error
        {
            get { return null; }
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int HowManyOrders { get; set; }


        public ClientData(string name, string surname, string email, string address, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
            Password = password;
            HowManyOrders = 0;
        }

        public ClientData(string name, string surname, string email, string address, string password,int hmo)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
            Password = password;
            HowManyOrders = hmo;
        }

    }
}
