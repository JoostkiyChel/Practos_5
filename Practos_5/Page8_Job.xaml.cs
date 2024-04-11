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
    /// Логика взаимодействия для Page8_Job.xaml
    /// </summary>
    public partial class Page8_Job : Page
    {
        JobTableAdapter job = new JobTableAdapter();
        public Page8_Job()
        {
            InitializeComponent();
            datasetik.ItemsSource = job.GetData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ContainsDigits(tbx.Text))
                {
                    MessageBox.Show("Пожалуйста, введите только буквы в поле для должности.");
                    return;
                }

                if (string.IsNullOrEmpty(tbx.Text) || string.IsNullOrEmpty(tbx2.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                
                if (!decimal.TryParse(tbx2.Text, out _))
                {
                    MessageBox.Show("Пожалуйста, введите число в поле для зарплаты.");
                    return;
                }

                job.InsertQuery(tbx.Text, Convert.ToInt32(tbx2.Text));
                datasetik.ItemsSource = job.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (datasetik.SelectedItem as DataRowView)?.Row[0];
                if (id != null)
                {
                    job.DeleteQuery(Convert.ToInt32(id));
                    datasetik.ItemsSource = job.GetData();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message);
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (datasetik.SelectedItem as DataRowView)?.Row[0];
                if (id != null)
                {

                    if (ContainsDigits(tbx.Text))
                    {
                        MessageBox.Show("Пожалуйста, введите только буквы в поле для должности.");
                        return;
                    }

                    if (string.IsNullOrEmpty(tbx.Text) || string.IsNullOrEmpty(tbx2.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля.");
                        return;
                    }

                    if (!decimal.TryParse(tbx2.Text, out _))
                    {
                        MessageBox.Show("Пожалуйста, введите число в поле для зарплаты.");
                        return;
                    }

                    job.UpdateQuery(tbx.Text, Convert.ToInt32(tbx2.Text), Convert.ToInt32(id));
                    datasetik.ItemsSource = job.GetData();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Выберите запись для изменения.");
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
                    tbx.Text = row.Row["Job"].ToString();
                    tbx2.Text = row.Row["salary"].ToString();
                    
                }
            }
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


        private void Import_Click(object sender, RoutedEventArgs e)
        {
            List<Job_Model> forImport = Converter.DeserializeObject<List<Job_Model>>();
            foreach (var item in forImport)
            {
                job.InsertQuery(item.Job, Convert.ToDecimal(item.salary));
            }
            datasetik.ItemsSource = null;
            datasetik.ItemsSource = job.GetData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
