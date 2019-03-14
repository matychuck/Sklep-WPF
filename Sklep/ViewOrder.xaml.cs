﻿using System;
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

namespace Sklep
{
    /// <summary>
    /// Interaction logic for ViewOrder.xaml
    /// </summary>
    public partial class ViewOrder : Window
    {
        public List<ProductData> orders = new List<ProductData>();

        public ViewOrder()
        {
            InitializeComponent();
        }
        public void UpdateProductsToView(List<ProductData> prd)
        {
            orders = prd;
            ordersList.ItemsSource = prd;
        }
    }
}
