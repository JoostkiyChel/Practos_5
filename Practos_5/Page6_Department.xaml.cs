using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Page6_Department.xaml
    /// </summary>
    public partial class Page6_Department : Page
    {
        DepartmentTableAdapter depart = new DepartmentTableAdapter();
        public Page6_Department()
        {
            InitializeComponent();
            datasetik.ItemsSource = depart.GetData();
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
                if (string.IsNullOrEmpty(tbx.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                depart.InsertQuery(tbx.Text);
                datasetik.ItemsSource = depart.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Ошибка уникального ключа
                {
                    MessageBox.Show("Отдел с таким именем уже существует.");
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении отдела: " + ex.Message);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            depart.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = depart.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (datasetik.SelectedItem == null)
                {
                    MessageBox.Show("Выберите отдел для изменения.");
                    return;
                }

                if (string.IsNullOrEmpty(tbx.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                DataRowView selectedRow = datasetik.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    MessageBox.Show("Выбранная строка не содержит данных.");
                    return;
                }

                object id = selectedRow.Row[0];
                depart.UpdateQuery(tbx.Text, Convert.ToInt32(id));
                datasetik.ItemsSource = depart.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (SqlException ex)
            {
                
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx.Text = row.Row["Location_Department"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
