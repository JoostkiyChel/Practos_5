using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeesTableAdapter emplyee = new EmployeesTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter(); 
        AuthTableAdapter auth = new AuthTableAdapter(); 
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var allLogins = auth.GetData().Rows;

                foreach (DataRow row in allLogins)
                {
                    if (row["Login_Client"].ToString() == loginTBX.Text && row["Password_Client"].ToString() == Password.Password)
                    {
                        int authorizationsID = (int)row["ID_Auth"];

                        
                        DataRow[] clientRow = clients.GetData().Select("Auth_ID = " + authorizationsID);
                        if (clientRow.Length > 0)
                        {
                            
                            Clients1 client = new Clients1();
                            client.Show();
                            Close();
                            return;
                        }

                        
                        DataRow[] employeeRow = emplyee.GetData().Select("Auth_ID = " + authorizationsID);
                        if (employeeRow.Length > 0)
                        {
                            // Если запись найдена в таблице Employees, значит это сотрудник
                            Admin1 administrator = new Admin1();
                            administrator.Show();
                            Close();
                            return;
                        }

                        
                        MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                
                MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
