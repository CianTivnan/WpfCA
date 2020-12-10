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
using System.Windows.Navigation;
using System.Windows.Shapes;
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 Author - Cian Tivnan
 Date - 10/12/2020
 Desc - CA2 December 2020, WPF Application
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employee> Employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();

            CreateEmployees();

            Display();
        }

        public void CreateEmployees()
        {
            FullTimeEmployee emp1 = new FullTimeEmployee("John", "Doe", 36000);
            Employees.Add(emp1);
            FullTimeEmployee emp2 = new FullTimeEmployee("Jane", "Doherty", 54000);
            Employees.Add(emp2);
            PartTimeEmployee emp3 = new PartTimeEmployee("Robert", "Kenny", 10, 18.5);   
            Employees.Add(emp3);
            PartTimeEmployee emp4 = new PartTimeEmployee("Anne", "McCormack", 10, 18.5);
            Employees.Add(emp4);
        }

        public void Display()
        {

        }
    }
}
