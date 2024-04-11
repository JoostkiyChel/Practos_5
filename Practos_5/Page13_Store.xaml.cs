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
    /// Логика взаимодействия для Page13_Store.xaml
    /// </summary>
    public partial class Page13_Store : Page
    {
        StoreTableAdapter str = new StoreTableAdapter();
        ProvidersTableAdapter prov = new ProvidersTableAdapter();
        WareHouseTableAdapter ware = new WareHouseTableAdapter();
        public Page13_Store()
        {
            InitializeComponent();
            datasetik.ItemsSource = str.GetDataBy3();
            cbx2.ItemsSource = prov.GetData();
            cbx2.DisplayMemberPath = "Contact_Person";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (datasetik.SelectedItem == null)
                {
                    MessageBox.Show("Выберите элемент для удаления.");
                    return;
                }

                if (string.IsNullOrEmpty(tbx.Text) || cbx2.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(tbx.Text, out int storeId))
                {
                    MessageBox.Show("Введите числовое значение для идентификатора склада/магазина.");
                    return;
                }

                if (cbx2.SelectedItem is DataRowView selecteddpr)
                {
                    int selecteddprid = Convert.ToInt32(selecteddpr["ID_Provider"]);
                    str.InsertQuery(storeId, selecteddprid);
                    datasetik.ItemsSource = str.GetDataBy3();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                    datasetik.Columns[1].Visibility = Visibility.Collapsed;
                    datasetik.Columns[2].Visibility = Visibility.Collapsed;
                    datasetik.Columns[3].Visibility = Visibility.Collapsed;
                    datasetik.Columns[4].Visibility = Visibility.Collapsed;
                    datasetik.Columns[5].Visibility = Visibility.Collapsed;
                    datasetik.Columns[6].Visibility = Visibility.Collapsed;
                    datasetik.Columns[9].Visibility = Visibility.Collapsed;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (datasetik.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для удаления.");
                return;
            }
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            str.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = str.GetDataBy3();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[3].Visibility = Visibility.Collapsed;
            datasetik.Columns[4].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[6].Visibility = Visibility.Collapsed;
            datasetik.Columns[9].Visibility = Visibility.Collapsed;

        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (datasetik.SelectedItem == null)
                {
                    MessageBox.Show("Выберите элемент для удаления.");
                    return;
                }

                if (string.IsNullOrEmpty(tbx.Text) || cbx2.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(tbx.Text, out int storeId))
                {
                    MessageBox.Show("Введите числовое значение для идентификатора склада/магазина.");
                    return;
                }

                if (cbx2.SelectedItem is DataRowView selecteddpr)
                {
                    int selecteddprid = Convert.ToInt32(selecteddpr["ID_Provider"]);
                    object id = (datasetik.SelectedItem as DataRowView).Row[0];
                    str.UpdateQuery(storeId, selecteddprid, Convert.ToInt32(id));
                    datasetik.ItemsSource = str.GetDataBy3();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                    datasetik.Columns[1].Visibility = Visibility.Collapsed;
                    datasetik.Columns[2].Visibility = Visibility.Collapsed;
                    datasetik.Columns[3].Visibility = Visibility.Collapsed;
                    datasetik.Columns[4].Visibility = Visibility.Collapsed;
                    datasetik.Columns[5].Visibility = Visibility.Collapsed;
                    datasetik.Columns[6].Visibility = Visibility.Collapsed;
                    datasetik.Columns[9].Visibility = Visibility.Collapsed;

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
                    int wareHouseId = Convert.ToInt32(row["WareHouse_ID"]);

                    
                    foreach (DataSet2.WareHouseRow wareHouseRow in ware.GetData().Rows)
                    {
                        if (wareHouseRow.ID_WareHouse == wareHouseId)
                        {
                            tbx.Text = wareHouseRow.Location_WareHouse;
                            break;
                        }
                    }
                    cbx2.Text = row["Contact_Person"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[3].Visibility = Visibility.Collapsed;
            datasetik.Columns[4].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[6].Visibility = Visibility.Collapsed;
            datasetik.Columns[9].Visibility = Visibility.Collapsed;
            
        }
    }
}
