using Practos_5.DataSet2TableAdapters;
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


namespace Practos_5
{
    /// <summary>
    /// Логика взаимодействия для PageEmp_Clients.xaml
    /// </summary>
    public partial class PageEmp_Clients : Page
    {   
        EmployeesTableAdapter emp = new EmployeesTableAdapter();
        public PageEmp_Clients()
        {
            InitializeComponent();
            datasetik.ItemsSource = emp.GetDataBy1();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[6].Visibility = Visibility.Collapsed;
            datasetik.Columns[8].Visibility = Visibility.Collapsed;
            datasetik.Columns[9].Visibility = Visibility.Collapsed;
            datasetik.Columns[10].Visibility = Visibility.Collapsed;
        }
    }
}
