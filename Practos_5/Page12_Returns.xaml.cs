using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Логика взаимодействия для Page12_Returns.xaml
    /// </summary>
    public partial class Page12_Returns : Page
    {
        Returns_OrderTableAdapter ret = new Returns_OrderTableAdapter();
        OrdersTableAdapter ord = new OrdersTableAdapter();
        public Page12_Returns()
        {
            InitializeComponent();
            datasetik.ItemsSource = ret.GetData();
            cbx.ItemsSource = ord.GetData();
            cbx.DisplayMemberPath = "Date_Order";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(tbx2.Text) || string.IsNullOrEmpty(tbx3.Text) || cbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!DateTime.TryParseExact(tbx2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    MessageBox.Show("Неверный формат даты. Используйте формат yyyy-MM-dd.");
                    return;
                }

                if (!int.TryParse(tbx3.Text, out _))
                {
                    MessageBox.Show("Общая сумма возврата должна содержать только цифры.");
                    return;
                }

                if (cbx.SelectedItem is DataRowView selecteddpr)
                {
                    int selecteddprid = Convert.ToInt32(selecteddpr["ID_Order"]);
                    ret.InsertQuery(selecteddprid, tbx2.Text, Convert.ToInt32(tbx3.Text));
                    datasetik.ItemsSource = ret.GetData();
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
            if (datasetik.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для удаления.");
                return;
            }
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            ret.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = ret.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(tbx2.Text) || string.IsNullOrEmpty(tbx3.Text) || cbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!DateTime.TryParseExact(tbx2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    MessageBox.Show("Неверный формат даты. Используйте формат yyyy-MM-dd.");
                    return;
                }

                if (!int.TryParse(tbx3.Text, out _))
                {
                    MessageBox.Show("Общая сумма возврата должна содержать только цифры.");
                    return;
                }

                if (cbx.SelectedItem is DataRowView selecteddpr)
                {
                    int selecteddprid = Convert.ToInt32(selecteddpr["ID_Order"]);
                    object id = (datasetik.SelectedItem as DataRowView).Row[0];
                    ret.UpdateQuery(selecteddprid, tbx2.Text, Convert.ToInt32(tbx3.Text), Convert.ToInt32(id));
                    datasetik.ItemsSource = ret.GetData();
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

                    tbx2.Text = row.Row["Date_Returns"].ToString();
                    tbx3.Text = row.Row["returns_totalAmounts"].ToString();
                }
                DataRowView selectedRow = datasetik.SelectedItem as DataRowView;
                if (selectedRow != null)
                {

                    int ordID = Convert.ToInt32(selectedRow["Order_ID"]);


                    foreach (DataRowView item in cbx.Items)
                    {
                        if (Convert.ToInt32(item["ID_Order"]) == ordID)
                        {
                            cbx.SelectedItem = item;
                            break;
                        }
                    }
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
