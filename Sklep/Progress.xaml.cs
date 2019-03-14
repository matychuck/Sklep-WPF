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
using System.Threading;
using System.ComponentModel;

namespace Sklep
{
    /// <summary>
    /// Logika interakcji dla klasy Progress.xaml
    /// </summary>
    public partial class Progress : Window
    {
        public Progress()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();

        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 50; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i*2);
                
                Thread.Sleep(50);
                
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
            Percentage.Text = Convert.ToString(pbStatus.Value) + "/ 100%";
            if (pbStatus.Value==100)
            {
                this.Close();
            }
        }
    }
}
