using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Page5_Clients.xaml
    /// </summary>
    public partial class Page5_Clients : Page
    {   
        ClientsTableAdapter clients = new ClientsTableAdapter();
        AuthTableAdapter auth = new AuthTableAdapter();
        public Page5_Clients()
        {
            InitializeComponent();
            datasetik.ItemsSource = clients.GetDataBy3();
            cbx4.ItemsSource = auth.GetData();
            cbx4.DisplayMemberPath = "Login_Client";
            
        }
        private bool ContainsDigits(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbx3.Text.Length > 12)
                {
                    MessageBox.Show("Вы ввели слишком много символов в поле для номера телефона.");
                    return;
                }

                if (string.IsNullOrEmpty(tbx.Text) || string.IsNullOrEmpty(tbx2.Text) || string.IsNullOrEmpty(tbx3.Text) || cbx4.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                else if (!Regex.IsMatch(tbx3.Text, @"^[+\d]+$"))
                {
                    MessageBox.Show("Пожалуйста, введите только цифры и символ '+' в поле для номера телефона.");
                    return;
                }

                else if (ContainsDigits(tbx.Text) || ContainsDigits(tbx2.Text))
                {
                    MessageBox.Show("Пожалуйста, введите только буквы в поля для имени и фамилии.");
                    return;
                }
                else
                {

                    if (cbx4.SelectedItem is DataRowView selecteddpr)
                    {
                        int selecteddprid = Convert.ToInt32(selecteddpr["ID_Auth"]);
                        clients.InsertQuery(tbx.Text, tbx2.Text, tbx3.Text, selecteddprid);
                        datasetik.ItemsSource = clients.GetDataBy3();
                        datasetik.Columns[0].Visibility = Visibility.Collapsed;
                        datasetik.Columns[4].Visibility = Visibility.Collapsed;
                        datasetik.Columns[5].Visibility = Visibility.Collapsed;
                        datasetik.Columns[7].Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Ошибка: Запись с такими данными уже существует.");
                    
                }
                else
                {
                    
                    MessageBox.Show("Ошибка базы данных: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            clients.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = clients.GetDataBy3();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[4].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[7].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                object id = (datasetik.SelectedItem as DataRowView).Row[0];

                if (datasetik.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите строку, которую хотите изменить.");
                    return;
                }

                if (tbx3.Text.Length > 12)
                {
                    MessageBox.Show("Вы ввели слишком много символов в поле для номера телефона.");
                    return;
                }

                if (string.IsNullOrEmpty(tbx.Text) || string.IsNullOrEmpty(tbx2.Text) || string.IsNullOrEmpty(tbx3.Text) || cbx4.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                else if (!Regex.IsMatch(tbx3.Text, @"^[+\d]+$"))
                {
                    MessageBox.Show("Пожалуйста, введите только цифры и символ '+' в поле для номера телефона.");
                    return;
                }

                else if (ContainsDigits(tbx.Text) || ContainsDigits(tbx2.Text))
                {
                    MessageBox.Show("Пожалуйста, введите только буквы в поля для имени и фамилии.");
                    return;
                }
                else
                {
                    if (cbx4.SelectedItem is DataRowView selecteddpr)
                    {
                        int selecteddprid = Convert.ToInt32(selecteddpr["ID_Auth"]);

                        clients.UpdateQuery(tbx.Text, tbx2.Text, tbx3.Text, selecteddprid, Convert.ToInt32(id));
                        datasetik.ItemsSource = clients.GetDataBy3();
                        datasetik.Columns[0].Visibility = Visibility.Collapsed;
                        datasetik.Columns[4].Visibility = Visibility.Collapsed;
                        datasetik.Columns[5].Visibility = Visibility.Collapsed;
                        datasetik.Columns[7].Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Код ошибки для ограничения уникальности
                {
                    MessageBox.Show("Ошибка при обновлении данных: Номер телефона уже существует в базе данных.");
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при обновлении данных: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла общая ошибка: " + ex.Message);
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx.Text = row.Row["Name_Client"].ToString();
                    tbx2.Text = row.Row["Surname_Client"].ToString();
                    tbx3.Text = row.Row["Phone"].ToString();
                    cbx4.Text = row.Row["Login_Client"].ToString();
                    
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[4].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[7].Visibility = Visibility.Collapsed;
        }
    }
}
