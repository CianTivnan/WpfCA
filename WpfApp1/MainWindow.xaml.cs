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
using System.Collections.ObjectModel;
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
        ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();

        public MainWindow()
        {
            InitializeComponent();

            //we call a method to create 4 employees and add them to our collection
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
            PartTimeEmployee emp4 = new PartTimeEmployee("Anne", "McCormack", 12, 24);
            Employees.Add(emp4);
            FullTimeEmployee emp5 = new FullTimeEmployee("John", "Doe", 36000);
            Employees.Add(emp1);
        }

        public void Display()
        {
            SortEmployees();

            List<Employee> MarkForDelete = new List<Employee>();

            if (cbxFullTime.IsChecked == true && cbxPartTime.IsChecked == true)
            {
                Employees.Clear();
                CreateEmployees();
                lbxEmployees.ItemsSource = Employees;
            }
            if (cbxFullTime.IsChecked == true && cbxPartTime.IsChecked == false)
            {
                lbxEmployees.ItemsSource = Employees;
                foreach (Employee item in lbxEmployees.Items)
                {
                    if(item is PartTimeEmployee)
                    {
                        MarkForDelete.Add(item);
                    }
                }
            }
            if (cbxFullTime.IsChecked == false && cbxPartTime.IsChecked == true)
            {
                lbxEmployees.ItemsSource = Employees;
                foreach (Employee item in lbxEmployees.Items)
                {
                    if (item is FullTimeEmployee)
                    {
                        MarkForDelete.Add(item);
                    }
                }
            }

            foreach (Employee employee in MarkForDelete)
            {
                Employees.Remove(employee);
            }
        }

        public void SortEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            foreach (Employee employee in Employees)
            {
                employeeList.Add(employee);
            }

            employeeList.Sort();
            Employees.Clear();

            foreach (Employee employee in employeeList)
            {
                Employees.Add(employee);
            }
            


        }

        private void cbxFullTime_Click(object sender, RoutedEventArgs e)
        {
            lbxEmployees.ItemsSource = null;
            Display();
        }

        private void cbxPartTime_Click(object sender, RoutedEventArgs e)
        {
            lbxEmployees.ItemsSource = null;
            Display();
        }

        private void lbxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            tblkMonthlyPay.Text = "Monthly Pay : ";

            if (selectedEmployee != null)
            {
                tbxFName.Text = selectedEmployee.FirstName;
                tbxLName.Text = selectedEmployee.Surname;

                if(selectedEmployee is PartTimeEmployee)
                {
                    PartTimeEmployee selectedPT = selectedEmployee as PartTimeEmployee;

                    rdoPartTime.IsChecked = true;
                    rdoFullTime.IsChecked = false;

                    tbxSalary.Clear();
                    tbxHourlyRate.Text = selectedPT.HourlyRate.ToString();
                    tbxHoursWorked.Text = selectedPT.HoursWorked.ToString();

                    tblkMonthlyPay.Text += selectedPT.CalculateMonthlyPay().ToString();
                }
                else
                {
                    FullTimeEmployee selectedFT = selectedEmployee as FullTimeEmployee;

                    rdoPartTime.IsChecked = false;
                    rdoFullTime.IsChecked = true;

                    tbxSalary.Text = selectedFT.Salary.ToString();
                    tbxHourlyRate.Clear();
                    tbxHoursWorked.Clear();

                    tblkMonthlyPay.Text += selectedFT.CalculateMonthlyPay().ToString();
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxFName.Clear();
            tbxLName.Clear();
            rdoFullTime.IsChecked = false;
            rdoPartTime.IsChecked = false;
            tbxSalary.Clear();
            tbxHourlyRate.Clear();
            tbxHoursWorked.Clear();
            tblkMonthlyPay.Text = "Monthly Pay : ";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (rdoFullTime.IsChecked == true)
            {
                string fName = tbxFName.Text;
                string lName = tbxLName.Text;
                decimal salary = decimal.Parse(tbxSalary.Text);
                Employees.Add(new FullTimeEmployee(fName, lName, salary));
            }
            else
            {
                string fName = tbxFName.Text;
                string lName = tbxLName.Text;
                decimal hourlyRate = decimal.Parse(tbxHourlyRate.Text);
                double hoursWorked = double.Parse(tbxHoursWorked.Text);
                Employees.Add(new PartTimeEmployee(fName, lName, hourlyRate, hoursWorked));
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if (selectedEmployee != null)
            {
                selectedEmployee.FirstName = tbxFName.Text;
                selectedEmployee.Surname = tbxLName.Text;

                if (selectedEmployee is PartTimeEmployee)
                {
                    PartTimeEmployee selectedPT = selectedEmployee as PartTimeEmployee;

                    selectedPT.HourlyRate = decimal.Parse(tbxHourlyRate.Text);
                    selectedPT.HoursWorked = double.Parse(tbxHoursWorked.Text);
                }
                else
                {
                    FullTimeEmployee selectedFT = selectedEmployee as FullTimeEmployee;

                    selectedFT.Salary = decimal.Parse(tbxSalary.Text);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if(selectedEmployee != null)
            {
                Employees.Remove(selectedEmployee);
                tbxFName.Clear();
                tbxLName.Clear();
                rdoFullTime.IsChecked = false;
                rdoPartTime.IsChecked = false;
                tbxSalary.Clear();
                tbxHourlyRate.Clear();
                tbxHoursWorked.Clear();
                tblkMonthlyPay.Text = "Monthly Pay : ";
            }
        }
    }
}
