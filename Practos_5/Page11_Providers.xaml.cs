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
    /// Логика взаимодействия для Page11_Providers.xaml
    /// </summary>
    public partial class Page11_Providers : Page
    {
        ProvidersTableAdapter prov = new ProvidersTableAdapter();
        public Page11_Providers()
        {
            InitializeComponent();
            datasetik.ItemsSource = prov.GetData();
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
            try
            {
                if (string.IsNullOrEmpty(tbx.Text) || string.IsNullOrEmpty(tbx2.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (IsNumberAlreadyExists(tbx2.Text))
                {
                    MessageBox.Show("Номер контакта уже существует.");
                    return;
                }

                
                if (!int.TryParse(tbx2.Text, out _))
                {
                    MessageBox.Show("Контактный номер должен содержать только цифры.");
                    return;
                }

                if (ContainsDigits(tbx.Text))
                {
                    MessageBox.Show("Имя поставщика не должно содержать цифры.");
                    return;
                }

                prov.InsertQuery(tbx.Text, tbx2.Text);
                datasetik.ItemsSource = prov.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }

        private bool IsNumberAlreadyExists(string contactNumber)
        {
            var data = prov.GetData();

            foreach (var row in data.Rows)
            {
                DataRowView row1 = datasetik.SelectedItem as DataRowView;
                if (row1.Row["Contact_Number"].ToString() == contactNumber)
                {
                    return true;
                }
            }

            return false;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            prov.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = prov.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(tbx.Text) || string.IsNullOrEmpty(tbx2.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (IsNumberAlreadyExists(tbx2.Text))
                {
                    MessageBox.Show("Номер контакта уже существует.");
                    return;
                }

                if (ContainsDigits(tbx.Text))
                {
                    MessageBox.Show("Имя поставщика не должно содержать цифры.");
                    return;
                }

                if (!int.TryParse(tbx2.Text, out _))
                {
                    MessageBox.Show("Контактный номер должен содержать только цифры.");
                    return;
                }

                object id = (datasetik.SelectedItem as DataRowView).Row[0];
                prov.UpdateQuery(tbx.Text, tbx2.Text, Convert.ToInt32(id));
                datasetik.ItemsSource = prov.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }


        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx.Text = row.Row["Contact_Person"].ToString();
                    tbx2.Text = row.Row["Contact_Number"].ToString();
                    

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
