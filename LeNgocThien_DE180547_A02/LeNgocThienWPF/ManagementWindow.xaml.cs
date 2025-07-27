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

namespace LeNgocThienWPF
{
    /// <summary>
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        public ManagementWindow()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            frm.Source = new Uri("CustomerManagementPage.xaml", UriKind.Relative);
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            frm.Source = new Uri("RoomManagementPage.xaml", UriKind.Relative);
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            frm.Source = new Uri("StaticReportPage.xaml", UriKind.Relative);
        }
    }
}
