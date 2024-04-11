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
   
    public partial class Page3_Auth : Page
    {
        AuthTableAdapter auth = new AuthTableAdapter();
        public Page3_Auth()
        {
            InitializeComponent();
            datasetik.ItemsSource = auth.GetData();
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                datasetik.Columns[0].Visibility = Visibility.Collapsed;

                auth.InsertQuery(tbx.Text, tbx2.Text);
                datasetik.ItemsSource = auth.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Ошибка: Данные уже существуют в базе данных.");
                
            }


        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
           try
           {
                object id = (datasetik.SelectedItem as DataRowView).Row[0];
            auth.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = auth.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
                
           }
           catch (SqlException ex)
           {
               if (ex.Number == 547) 
               {
               
                   MessageBox.Show("Невозможно удалить этого сотрудника, так как у него есть связанные данные.");
               
               }
           }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datasetik.SelectedItem != null)
                {
                    object id = (datasetik.SelectedItem as DataRowView).Row[0];
                    auth.UpdateQuery(tbx.Text, tbx2.Text, Convert.ToInt32(id));
                    datasetik.ItemsSource = auth.GetData();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                    datasetik.Columns[2].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Выберите строку, которую хотите изменить");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx.Text = row.Row["Login_Client"].ToString();
                    tbx2.Text = row.Row["Password_Client"].ToString();
                   
                }
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
        }
    }
}
