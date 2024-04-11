using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Page15_WareHouse_Products.xaml
    /// </summary>
    public partial class Page15_WareHouse_Products : Page
    {  
        WareHouse_ProductsTableAdapter wp = new WareHouse_ProductsTableAdapter();
        ProductsTableAdapter prod = new ProductsTableAdapter();
        WareHouseTableAdapter ware = new WareHouseTableAdapter();
        public Page15_WareHouse_Products()
        {
            InitializeComponent();
            datasetik.ItemsSource = wp.GetDataBy3();
            cbx.ItemsSource = prod.GetData();
            cbx.DisplayMemberPath = "Price";
            cbx2.ItemsSource = ware.GetData();
            cbx2.DisplayMemberPath = "Location_WareHouse";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (cbx.SelectedItem == null || cbx2.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbx.SelectedItem is DataRowView selecteddpr && cbx2.SelectedItem is DataRowView selectedjob)
            {

                int selecteddprid = Convert.ToInt32(selecteddpr["ID_Product"]);
                int selectedjobid = Convert.ToInt32(selectedjob["ID_WareHouse"]);
                wp.InsertQuery(selecteddprid, selectedjobid);
                datasetik.ItemsSource = wp.GetDataBy3();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[1].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            wp.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = wp.GetDataBy3();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

            if (cbx.SelectedItem == null || cbx2.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (cbx.SelectedItem is DataRowView selecteddpr && cbx2.SelectedItem is DataRowView selectedjob)
            {
                int selecteddprid = Convert.ToInt32(selecteddpr["ID_Product"]);
                int selectedjobid = Convert.ToInt32(selectedjob["ID_WareHouse"]);
                object id = (datasetik.SelectedItem as DataRowView).Row[0];
                wp.UpdateQuery(selecteddprid, selectedjobid, Convert.ToInt32(id));
                datasetik.ItemsSource = wp.GetDataBy3();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[1].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    cbx.Text = row.Row["Price"].ToString();
                    cbx2.Text = row.Row["Location_WareHouse"].ToString();
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
        }
    }
}
