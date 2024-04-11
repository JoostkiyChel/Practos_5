using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Page4_Categories.xaml
    /// </summary>
    public partial class Page4_Categories : Page
    {
        CategoriesTableAdapter category = new CategoriesTableAdapter();
        public Page4_Categories()
        {
            InitializeComponent();
            datasetik.ItemsSource = category.GetData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string categoryText = tbx.Text.Trim();

            if (string.IsNullOrEmpty(categoryText))
            {
                MessageBox.Show("Пожалуйста, введите название категории.");
                return;
            }

            if (!Regex.IsMatch(categoryText, "^[а-яА-Я]+$"))
            {
                MessageBox.Show("Только буквы на русском языке");
                return;
            }

            if (categoryText.Length > 100)
            {
                MessageBox.Show("Не больше 100 символов");
                return;
            }

            try
            {
                category.InsertQuery(tbx.Text);
                datasetik.ItemsSource = category.GetData();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: Данные уже существуют в базе данных.");
                
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            category.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = category.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datasetik.SelectedItem != null)
                {
                    string categoryText = tbx.Text.Trim();


                    if (string.IsNullOrEmpty(categoryText))
                    {
                        MessageBox.Show("Пожалуйста, введите название категории.");
                        return;
                    }

                    if (!Regex.IsMatch(categoryText, "^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("Только буквы");
                        return;
                    }
                    if (categoryText.Length > 100)
                    {
                        MessageBox.Show("Не больше 100 символов");
                        return;
                    }

                    object id = (datasetik.SelectedItem as DataRowView).Row[0];
                    category.UpdateQuery(tbx.Text, Convert.ToInt32(id));
                    datasetik.ItemsSource = category.GetData();
                    datasetik.Columns[0].Visibility = Visibility.Collapsed;
                    
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
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx.Text = row.Row["Category"].ToString();



                }
            }
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            List<Category_model> forImport = Converter.DeserializeObject<List<Category_model>>();
            foreach (var item in forImport)
            {
                category.InsertQuery(item.Category);
            }
            datasetik.ItemsSource = null;
            datasetik.ItemsSource = category.GetData();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            
        }
    }
}
