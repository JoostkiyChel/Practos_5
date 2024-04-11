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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Practos_5.DataSet2TableAdapters;

namespace Practos_5
{
    /// <summary>
    /// Логика взаимодействия для PagePrd_Clients.xaml
    /// </summary>
    public partial class PagePrd_Clients : Page
    {   
        ProductsTableAdapter prod = new ProductsTableAdapter();
        public PagePrd_Clients()
        {
            InitializeComponent();
            datasetik.ItemsSource = prod.GetDataBy3();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
        }
    }
}
