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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Practos_5.DataSet2TableAdapters;

namespace Practos_5
{
    /// <summary>
    /// Логика взаимодействия для Page7_Employees.xaml
    /// </summary>
    public partial class Page7_Employees : Page
    {
        QueriesTableAdapter backups = new QueriesTableAdapter();
        EmployeesTableAdapter emp = new EmployeesTableAdapter();
        AuthTableAdapter auth1 = new AuthTableAdapter();
        DepartmentTableAdapter department = new DepartmentTableAdapter();
        JobTableAdapter job = new JobTableAdapter();

        public Page7_Employees()
        {
            InitializeComponent();
            datasetik.ItemsSource = emp.GetDataBy1();
            cbx.ItemsSource = department.GetData();
            cbx.DisplayMemberPath = "Location_Department";
            cbx2.ItemsSource = job.GetData();
            cbx2.DisplayMemberPath = "Job";
            cbx5.ItemsSource = auth1.GetData();
            cbx5.DisplayMemberPath = "Login_Client";
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
            if (ContainsDigits(tbx3.Text) || ContainsDigits(tbx4.Text))
            {
                MessageBox.Show("Пожалуйста, введите только буквы в поля для имени и фамилии.");
                return;
            }
            if (string.IsNullOrEmpty(tbx3.Text) || string.IsNullOrEmpty(tbx4.Text) || cbx.SelectedItem == null || cbx2.SelectedItem == null || cbx5.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (cbx.SelectedItem is DataRowView selecteddpr && cbx2.SelectedItem is DataRowView selectedjob && cbx5.SelectedItem is DataRowView selectedauth)
            {
                

                int selecteddprid = Convert.ToInt32(selecteddpr["ID_Department"]);
                int selectedjobid = Convert.ToInt32(selectedjob["ID_Job"]);
                int selectedauthid = Convert.ToInt32(selectedauth["ID_Auth"]);
                emp.InsertQuery(selecteddprid, selectedjobid, tbx3.Text, tbx4.Text, selectedauthid);
                datasetik.ItemsSource = emp.GetDataBy1();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[1].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
                datasetik.Columns[5].Visibility = Visibility.Collapsed;
                datasetik.Columns[8].Visibility = Visibility.Collapsed;
                datasetik.Columns[10].Visibility = Visibility.Collapsed;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (datasetik.SelectedItem as DataRowView).Row[0];
            emp.DeleteQuery(Convert.ToInt32(id));
            datasetik.ItemsSource = emp.GetDataBy1();
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[8].Visibility = Visibility.Collapsed;
            datasetik.Columns[10].Visibility = Visibility.Collapsed;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (ContainsDigits(tbx3.Text) || ContainsDigits(tbx4.Text))
            {
                MessageBox.Show("Пожалуйста, введите только буквы в поля для имени и фамилии.");
                return;
            }

            if (string.IsNullOrEmpty(tbx3.Text) || string.IsNullOrEmpty(tbx4.Text) || cbx.SelectedItem == null || cbx2.SelectedItem == null || cbx5.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (cbx.SelectedItem is DataRowView selecteddpr && cbx2.SelectedItem is DataRowView selectedjob && cbx5.SelectedItem is DataRowView selectedauth)
            {

                int selecteddprid = Convert.ToInt32(selecteddpr["ID_Department"]);
                int selectedjobid = Convert.ToInt32(selectedjob["ID_Job"]);
                int selectedauthid = Convert.ToInt32(selectedauth["ID_Auth"]);
                object id = (datasetik.SelectedItem as DataRowView).Row[0];
                emp.UpdateQuery(selecteddprid, selectedjobid, tbx3.Text, tbx4.Text, selectedauthid, Convert.ToInt32(id));
                datasetik.ItemsSource = emp.GetDataBy1();
                datasetik.Columns[0].Visibility = Visibility.Collapsed;
                datasetik.Columns[1].Visibility = Visibility.Collapsed;
                datasetik.Columns[2].Visibility = Visibility.Collapsed;
                datasetik.Columns[5].Visibility = Visibility.Collapsed;
                datasetik.Columns[8].Visibility = Visibility.Collapsed;
                datasetik.Columns[10].Visibility = Visibility.Collapsed;
            }
        }

        private void datasetik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datasetik.SelectedItem != null)
            {
                DataRowView row = datasetik.SelectedItem as DataRowView;
                if (row != null)
                {
                    tbx3.Text = row.Row["Name_Employee"].ToString();
                    tbx4.Text = row.Row["SurName_Employee"].ToString();
                }

                DataRowView selectedRow = datasetik.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    int ordID = Convert.ToInt32(selectedRow["Department_ID"]);
                    int jobid = Convert.ToInt32(selectedRow["Job_ID"]);
                    int authid = Convert.ToInt32(selectedRow["Auth_ID"]);

                    
                    foreach (DataRowView item in cbx.Items)
                    {
                        if (Convert.ToInt32(item["ID_Department"]) == ordID)
                        {
                            cbx.SelectedItem = item;
                            break;
                        }
                    }

                    
                    foreach (DataRowView item in cbx2.Items)
                    {
                        if (Convert.ToInt32(item["ID_Job"]) == jobid)
                        {
                            cbx2.SelectedItem = item;
                            break;
                        }
                    }

                    
                    foreach (DataRowView item in cbx5.Items)
                    {
                        if (Convert.ToInt32(item["ID_Auth"]) == authid)
                        {
                            cbx5.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void BackUp_Click(object sender, RoutedEventArgs e)
        {
            backups.Backup_BD();
            MessageBox.Show("Бэкап создан");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datasetik.Columns[0].Visibility = Visibility.Collapsed;
            datasetik.Columns[1].Visibility = Visibility.Collapsed;
            datasetik.Columns[2].Visibility = Visibility.Collapsed;
            datasetik.Columns[5].Visibility = Visibility.Collapsed;
            datasetik.Columns[10].Visibility = Visibility.Collapsed;
        }
    }
}
