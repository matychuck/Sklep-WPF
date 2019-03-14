using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Sklep
{   
    class RemoveCommands
    {
            private static RoutedUICommand delete;
            static RemoveCommands()
            {
                delete = new RoutedUICommand("Remove entry", "Remove", typeof(RemoveCommands));
            }
            public static RoutedUICommand Remove
            {
                get { return delete; }
            }
    }
    
}
