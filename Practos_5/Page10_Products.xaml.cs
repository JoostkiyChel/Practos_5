using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Page10_Products.xaml
    /// </summary>
    public partial class Page10_Products : Page
    {
        ProductsTableAdapter prod = new ProductsTableAdapter();
        CategoriesTableAdapter categories = new CategoriesTableAdapter();
        public Page10_Products()
        {
            InitializeComponent();
            datasetik.ItemsSource = prod.GetDataBy3();
            cbx.ItemsSource = categories.GetData();
            cbx.DisplayMemberPath = "Category";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbx.SelectedItem is DataRowView selectedcat)
                {
                    if (!int.TryParse(tbx4.Text, out int price))
                    {
                        MessageBox.Show("Пожалуйста, введите числовое значение в поле для цены.");
                        return;
                    }

                    if (cbx.SelectedItem == null || string.IsNullOrEmpty(tbx2.Text) || string.IsNullOrEmpty(tbx3.Text) || string.IsNullOrEmpty(tbx4.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int selectedcatid = Convert.ToInt32(selectedcat["ID_Category"]);
                    prod.InsertQuery(selectedcatid, tbx2.Text, tbx3.Text, price);
                    datasetik.ItemsSource = prod.GetDataBy3();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                    datasetik.Columns[1].Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            prod.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = prod.GetDataBy3();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbx.SelectedItem is DataRowView selectedcat)
                {
                    if (!int.TryParse(tbx4.Text, out int price))
                    {
                        MessageBox.Show("Пожалуйста, введите числовое значение в поле для цены.");
                        return;
                    }

                    if (cbx.SelectedItem == null || string.IsNullOrEmpty(tbx2.Text) || string.IsNullOrEmpty(tbx3.Text) || string.IsNullOrEmpty(tbx4.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int selectedcatid = Convert.ToInt32(selectedcat["ID_Category"]);
                    object id = (datasetik.SelectedItem as DataRowView).Row[0];
                    prod.UpdateQuery(selectedcatid, tbx2.Text, tbx3.Text, price, Convert.ToInt32(id));
                    datasetik.ItemsSource = prod.GetDataBy3();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                    datasetik.Columns[1].Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении данных: " + ex.Message);
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    
                    tbx2.Text = row.Row["Fishing_rod_Length"].ToString();
                    tbx3.Text = row.Row["Spining_Radius"].ToString();
                    tbx4.Text = row.Row["Price"].ToString();  
                    cbx.Text = row.Row["Category"].ToString();
                }
                
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
        }
    }
}
