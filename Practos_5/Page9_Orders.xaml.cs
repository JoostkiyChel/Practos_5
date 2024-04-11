using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для Page9_Orders.xaml
    /// </summary>
    public partial class Page9_Orders : Page
    {
        OrdersTableAdapter ord = new OrdersTableAdapter();
        StoreTableAdapter store = new StoreTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        ClientsTableAdapter client = new ClientsTableAdapter();
        CategoriesTableAdapter categories = new CategoriesTableAdapter();
        public Page9_Orders()
        {
            InitializeComponent();
            datasetik.ItemsSource = ord.GetDataBy3();
            cbx.ItemsSource = store.GetData();
            cbx.DisplayMemberPath = "WareHouse_Products_ID";
            cbx2.ItemsSource = employees.GetData();
            cbx2.DisplayMemberPath = "Name_Employee";
            cbx3.ItemsSource = client.GetData();
            cbx3.DisplayMemberPath = "Name_Client";

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (cbx.SelectedItem is DataRowView selectedStr &&
        cbx2.SelectedItem is DataRowView selectedemp &&
        cbx3.SelectedItem is DataRowView selectedclt)
            {
                int selectedStrid, selectedempid, selectedcltid;

                if (!int.TryParse(selectedStr["ID_Store"].ToString(), out selectedStrid))
                {
                    MessageBox.Show("Некорректное значение для ID_Store. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbx.SelectedItem == null || cbx2.SelectedItem == null || cbx3.SelectedItem == null || string.IsNullOrEmpty(tbx4.Text) || string.IsNullOrEmpty(tbx5.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(selectedemp["ID_Employee"].ToString(), out selectedempid))
                {
                    MessageBox.Show("Некорректное значение для ID_Employee. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(selectedclt["ID_Clients"].ToString(), out selectedcltid))
                {
                    MessageBox.Show("Некорректное значение для ID_Clients. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!DateTime.TryParse(tbx4.Text, out _))
                {
                    MessageBox.Show("Некорректное значение для Date_Order. Пожалуйста, введите корректную дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(tbx5.Text, out int totalAmount))
                {
                    MessageBox.Show("Некорректное значение для TotalAmount. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ord.InsertQuery(selectedStrid, selectedempid, selectedcltid, tbx4.Text, totalAmount);
                datasetik.ItemsSource = ord.GetDataBy3();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[1].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
                datasetik.Columns[3].Visibility = Visibility.Collapsed;
                datasetik.Columns[6].Visibility = Visibility.Collapsed;
                datasetik.Columns[7].Visibility = Visibility.Collapsed;
                datasetik.Columns[8].Visibility = Visibility.Collapsed;
                datasetik.Columns[9].Visibility = Visibility.Collapsed;
                datasetik.Columns[10].Visibility = Visibility.Collapsed;
                datasetik.Columns[11].Visibility = Visibility.Collapsed;
                datasetik.Columns[14].Visibility = Visibility.Collapsed;
                datasetik.Columns[15].Visibility = Visibility.Collapsed;
                datasetik.Columns[17].Visibility = Visibility.Collapsed;
                datasetik.Columns[18].Visibility = Visibility.Collapsed;
                datasetik.Columns[19].Visibility = Visibility.Collapsed;
                datasetik.Columns[20].Visibility = Visibility.Collapsed;
                datasetik.Columns[21].Visibility = Visibility.Collapsed;
                datasetik.Columns[22].Visibility = Visibility.Collapsed;
                datasetik.Columns[23].Visibility = Visibility.Collapsed;
                datasetik.Columns[24].Visibility = Visibility.Collapsed;
                datasetik.Columns[25].Visibility = Visibility.Collapsed;
                datasetik.Columns[26].Visibility = Visibility.Collapsed;
                datasetik.Columns[27].Visibility = Visibility.Collapsed;
                datasetik.Columns[28].Visibility = Visibility.Collapsed;
                datasetik.Columns[29].Visibility = Visibility.Collapsed;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            ord.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = ord.GetDataBy3();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[3].Visibility = Visibility.Collapsed;
            datasetik.Columns[6].Visibility = Visibility.Collapsed;
            datasetik.Columns[7].Visibility = Visibility.Collapsed;
            datasetik.Columns[8].Visibility = Visibility.Collapsed;
            datasetik.Columns[9].Visibility = Visibility.Collapsed;
            datasetik.Columns[10].Visibility = Visibility.Collapsed;
            datasetik.Columns[11].Visibility = Visibility.Collapsed;
            datasetik.Columns[14].Visibility = Visibility.Collapsed;
            datasetik.Columns[15].Visibility = Visibility.Collapsed;
            datasetik.Columns[17].Visibility = Visibility.Collapsed;
            datasetik.Columns[18].Visibility = Visibility.Collapsed;
            datasetik.Columns[19].Visibility = Visibility.Collapsed;
            datasetik.Columns[20].Visibility = Visibility.Collapsed;
            datasetik.Columns[21].Visibility = Visibility.Collapsed;
            datasetik.Columns[22].Visibility = Visibility.Collapsed;
            datasetik.Columns[23].Visibility = Visibility.Collapsed;
            datasetik.Columns[24].Visibility = Visibility.Collapsed;
            datasetik.Columns[25].Visibility = Visibility.Collapsed;
            datasetik.Columns[26].Visibility = Visibility.Collapsed;
            datasetik.Columns[27].Visibility = Visibility.Collapsed;
            datasetik.Columns[28].Visibility = Visibility.Collapsed;
            datasetik.Columns[29].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (cbx.SelectedItem is DataRowView selectedStr &&
            cbx2.SelectedItem is DataRowView selectedemp &&
            cbx3.SelectedItem is DataRowView selectedclt)
            {
                int selectedStrid, selectedempid, selectedcltid;

                if (!int.TryParse(selectedStr["ID_Store"].ToString(), out selectedStrid))
                {
                    MessageBox.Show("Некорректное значение для ID_Store. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbx.SelectedItem == null || cbx2.SelectedItem == null || cbx3.SelectedItem == null || string.IsNullOrEmpty(tbx4.Text) || string.IsNullOrEmpty(tbx5.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(selectedemp["ID_Employee"].ToString(), out selectedempid))
                {
                    MessageBox.Show("Некорректное значение для ID_Employee. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(selectedclt["ID_Clients"].ToString(), out selectedcltid))
                {
                    MessageBox.Show("Некорректное значение для ID_Clients. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!DateTime.TryParse(tbx4.Text, out _))
                {
                    MessageBox.Show("Некорректное значение для Date_Order. Пожалуйста, введите корректную дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(tbx5.Text, out int totalAmount))
                {
                    MessageBox.Show("Некорректное значение для TotalAmount. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                object id = (datasetik.SelectedItem as DataRowView).Row[0];
                ord.UpdateQuery(selectedStrid, selectedempid, selectedcltid, tbx4.Text, totalAmount, Convert.ToInt32(id));
                datasetik.ItemsSource = ord.GetDataBy3();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[1].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
                datasetik.Columns[3].Visibility = Visibility.Collapsed;
                datasetik.Columns[6].Visibility = Visibility.Collapsed;
                datasetik.Columns[7].Visibility = Visibility.Collapsed;
                datasetik.Columns[8].Visibility = Visibility.Collapsed;
                datasetik.Columns[9].Visibility = Visibility.Collapsed;
                datasetik.Columns[10].Visibility = Visibility.Collapsed;
                datasetik.Columns[11].Visibility = Visibility.Collapsed;
                datasetik.Columns[14].Visibility = Visibility.Collapsed;
                datasetik.Columns[15].Visibility = Visibility.Collapsed;
                datasetik.Columns[17].Visibility = Visibility.Collapsed;
                datasetik.Columns[18].Visibility = Visibility.Collapsed;
                datasetik.Columns[19].Visibility = Visibility.Collapsed;
                datasetik.Columns[20].Visibility = Visibility.Collapsed;
                datasetik.Columns[21].Visibility = Visibility.Collapsed;
                datasetik.Columns[22].Visibility = Visibility.Collapsed;
                datasetik.Columns[23].Visibility = Visibility.Collapsed;
                datasetik.Columns[24].Visibility = Visibility.Collapsed;
                datasetik.Columns[25].Visibility = Visibility.Collapsed;
                datasetik.Columns[26].Visibility = Visibility.Collapsed;
                datasetik.Columns[27].Visibility = Visibility.Collapsed;
                datasetik.Columns[28].Visibility = Visibility.Collapsed;
                datasetik.Columns[29].Visibility = Visibility.Collapsed;
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx4.Text = row.Row["Date_Order"].ToString();
                    tbx5.Text = row.Row["TotalAmount"].ToString();
                    cbx.Text = row.Row["WareHouse_Products_ID"].ToString();
                    cbx2.Text = row.Row["Name_Employee"].ToString();
                    cbx3.Text = row.Row["Name_Client"].ToString();
                }


                
            }
        }


        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView selectedRow = datasetik.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    // Получение информации о заказе
                    string orderId = selectedRow["ID_Order"].ToString();
                    string product = selectedRow["Fishing_rod_Length"].ToString();
                    string cat = selectedRow["Category"].ToString();
                    string orderDate = selectedRow["Date_Order"].ToString();
                    string totalAmount = selectedRow["TotalAmount"].ToString();

                    // Формирование содержимого чека
                    StringBuilder receiptText = new StringBuilder();
                    receiptText.AppendLine("<Название программы>");
                    receiptText.AppendLine($"  Кассовый чек №{orderId}");
                    receiptText.AppendLine($"Товары в заказе: {cat}" );

                    foreach (DataRow row in selectedRow.Row.GetChildRows("FK_Orders_Store"))
                    {
                        string productName = row.GetChildRows("FK_Store_WareHouse_Products")[0]["Fishing_rod_Length"].ToString();
                        string productPrice = row.GetChildRows("FK_Store_WareHouse_Products")[0]["Price"].ToString();
                        receiptText.AppendLine($"{productName}\t- {productPrice}");
                    }

                    receiptText.AppendLine($"Итого к оплате: {totalAmount}");
                    receiptText.AppendLine($"Внесено: {tbx5.Text}");

                    decimal paidAmount = Convert.ToDecimal(tbx5.Text);
                    decimal totalAmountDecimal = Convert.ToDecimal(totalAmount);
                    decimal change = paidAmount - totalAmountDecimal;
                    receiptText.AppendLine($"Сдача: {change}");

                    // Сохранение чека на рабочем столе
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, $"Чек_{orderId}.txt");

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.Write(receiptText.ToString());
                        }

                        MessageBox.Show("Чек сохранен на рабочем столе.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении чека: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для составления чека.");
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[3].Visibility = Visibility.Collapsed;
            datasetik.Columns[6].Visibility = Visibility.Collapsed;
            datasetik.Columns[7].Visibility = Visibility.Collapsed;
            datasetik.Columns[8].Visibility = Visibility.Collapsed;
            datasetik.Columns[9].Visibility = Visibility.Collapsed;
            datasetik.Columns[10].Visibility = Visibility.Collapsed;
            datasetik.Columns[11].Visibility = Visibility.Collapsed;
            datasetik.Columns[14].Visibility = Visibility.Collapsed;
            datasetik.Columns[15].Visibility = Visibility.Collapsed;
            datasetik.Columns[17].Visibility = Visibility.Collapsed;
            datasetik.Columns[18].Visibility = Visibility.Collapsed;
            datasetik.Columns[19].Visibility = Visibility.Collapsed;
            datasetik.Columns[20].Visibility = Visibility.Collapsed;
            datasetik.Columns[21].Visibility = Visibility.Collapsed;
            datasetik.Columns[22].Visibility = Visibility.Collapsed;
            datasetik.Columns[23].Visibility = Visibility.Collapsed;
            datasetik.Columns[24].Visibility = Visibility.Collapsed;
            datasetik.Columns[25].Visibility = Visibility.Collapsed;
            datasetik.Columns[26].Visibility = Visibility.Collapsed;
            datasetik.Columns[27].Visibility = Visibility.Collapsed;
            datasetik.Columns[28].Visibility = Visibility.Collapsed;
            datasetik.Columns[29].Visibility = Visibility.Collapsed;
        }
    }
}
