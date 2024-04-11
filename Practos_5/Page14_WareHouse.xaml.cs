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
    /// Логика взаимодействия для Page14_WareHouse.xaml
    /// </summary>
    public partial class Page14_WareHouse : Page
    {   WareHouseTableAdapter ware = new WareHouseTableAdapter();
        public Page14_WareHouse()
        {
            InitializeComponent();
            datasetik.ItemsSource = ware.GetData();
            
        }

        private bool ContainsDigits(string str)
        {
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbx.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поле.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ContainsDigits(tbx.Text))
            {
                MessageBox.Show("Поле не должно содержать цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ware.InsertQuery(tbx.Text);
            datasetik.ItemsSource = ware.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            ware.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = ware.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbx.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поле.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ContainsDigits(tbx.Text))
            {
                MessageBox.Show("Поле не должно содержать цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            ware.UpdateQuery(tbx.Text, Convert.ToInt32(id));
            datasetik.ItemsSource = ware.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx.Text = row.Row["Location_WareHouse"].ToString();
                   

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
