using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml;
using Practos_5.DataSet2TableAdapters;

namespace Practos_5
{
    /// <summary>
    /// Логика взаимодействия для Admin1.xaml
    /// </summary>
   
    public partial class Admin1 : Window
    {
        List<string> nameTable = new List<string> { "Auth", "Categoties", "Clients", "Department", "Employees", "Job", "Orders", "Products", "Providers", "Returns", "Store", "WareHouse", "WareHouse_Products" };

        public Admin1()
        {
            InitializeComponent();
            Combobox.ItemsSource = nameTable;

        }
        private void Combobox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = Combobox.SelectedItem.ToString();

            if (nameTable == "Auth")
            {
                PageFrame.Content = new Page3_Auth();
            }
            else if (nameTable == "Categoties")
            {
                PageFrame.Content = new Page4_Categories();
            }
            else if (nameTable == "Clients")
            {
                PageFrame.Content = new Page5_Clients();
            }
            else if (nameTable == "Department")
            {
                PageFrame.Content = new Page6_Department();

            }
            else if (nameTable == "Employees")
            {
                PageFrame.Content = new Page7_Employees();
            }
            else if (nameTable == "Job")
            {
                PageFrame.Content = new Page8_Job();
            }
            else if (nameTable == "Orders")
            {
                PageFrame.Content = new Page9_Orders();
            }
            else if (nameTable == "Products")
            {
                PageFrame.Content = new Page10_Products();
            }
            else if (nameTable == "Providers")
            {
                PageFrame.Content = new Page11_Providers();
            }
            else if (nameTable == "Returns")
            {
                PageFrame.Content = new Page12_Returns();
            }
            else if (nameTable == "Store")
            {
                PageFrame.Content = new Page13_Store();
            }
            else if (nameTable == "WareHouse")
            {
                PageFrame.Content = new Page14_WareHouse();
            }
            else if (nameTable == "WareHouse_Products")
            {
                PageFrame.Content = new Page15_WareHouse_Products();
            }
        }
    }
}
