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
using Practos_5.DataSet2TableAdapters;

namespace Practos_5
{
    /// <summary>
    /// Логика взаимодействия для Clients1.xaml
    /// </summary>
    public partial class Clients1 : Window
    {
        List<string> nameTable = new List<string> { "Employees", "Products" };
        public Clients1()
        {
            InitializeComponent();
            Combobox.ItemsSource = nameTable;
        }

        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = Combobox.SelectedItem.ToString();

            
            if (nameTable == "Employees")
            {
                PageFrame.Content = new PageEmp_Clients();
            }
            
            if (nameTable == "Products")
            {
                PageFrame.Content = new PagePrd_Clients();
            }
            
        }
    }
}
