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
    public partial class MainWindow : Window
    {
        //our first collection stores employees which are being displayed
        ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();
        //the second one stores employees who are not currently being displayed
        ObservableCollection<Employee> HiddenEmployees = new ObservableCollection<Employee>();

        public MainWindow()
        {
            InitializeComponent();

            //we call a method to create 4 employees and add them to our collection
            CreateEmployees();

            //we call our display method
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
        }

        public void Display()
        {
            //we use if statements to see what buttons have been pressed on the list box
            if (cbxFullTime.IsChecked == true && cbxPartTime.IsChecked == true)
            {
                //if there are hidden employees to be dispalyed:
                if (HiddenEmployees !=null)
                {
                    //we set source to null and add these employees back into the main collection
                    lbxEmployees.ItemsSource = null;
                    foreach (Employee employee in HiddenEmployees)
                    {
                        Employees.Add(employee);
                    }
                    //we then clear the hidden employees collection
                    HiddenEmployees.Clear();
                }
                //now we sort and set our source back to Employees
                SortEmployees();
                lbxEmployees.ItemsSource = Employees;
            }
            if (cbxFullTime.IsChecked == true && cbxPartTime.IsChecked == false)
            {
                // we loop through our employees to see if they are part time
                // if they are, we remove them
                foreach (Employee item in lbxEmployees.Items)
                {
                    if(item is PartTimeEmployee)
                    {
                        HiddenEmployees.Add(item);
                    }
                }
                //then we sort
                SortEmployees();
            }
            if (cbxFullTime.IsChecked == false && cbxPartTime.IsChecked == true)
            {
                // we loop through our employees to see if they are full time
                // if they are, we remove them
                foreach (Employee item in lbxEmployees.Items)
                {
                    if (item is FullTimeEmployee)
                    {
                        HiddenEmployees.Add(item);
                    }
                }
                //then we sort
                SortEmployees();
            }

            if (cbxFullTime.IsChecked == false && cbxPartTime.IsChecked == false)
            {
                //if both are unchecked, we set source to null so the box is empty
                lbxEmployees.ItemsSource = null;
            }

            foreach (Employee employee in HiddenEmployees)
            {
                //for each item we want to hide, we now remove it from the Employee Collection
                Employees.Remove(employee);
            }
        }

        public void SortEmployees()
        {
            //we set item source to null
            lbxEmployees.ItemsSource = null;
            //we create a list for the employees
            List<Employee> employeeList = new List<Employee>();

            //we loop through our ObservableCollection and add the elements to the new list
            foreach (Employee employee in Employees)
            {
                employeeList.Add(employee);
            }

            //we can now use the Sort() method to sort it
            employeeList.Sort();
            //we empty out our Observable Collection
            Employees.Clear();

            //now we add the sorted contents of our list back into the Observable Collection
            foreach (Employee employee in employeeList)
            {
                Employees.Add(employee);
            }

            //and we set our source back to the Collection
            lbxEmployees.ItemsSource = Employees;

        }

        private void cbxFullTime_Click(object sender, RoutedEventArgs e)
        {    
            Display();
        }

        private void cbxPartTime_Click(object sender, RoutedEventArgs e)
        {
            Display();
        }

        private void lbxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //we get the selected employee
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            //we reset the monthly pay text
            tblkMonthlyPay.Text = "Monthly Pay : ";

            if (selectedEmployee != null)
            {
                //we fill in the first and last name
                tbxFName.Text = selectedEmployee.FirstName;
                tbxLName.Text = selectedEmployee.Surname;

                if(selectedEmployee is PartTimeEmployee)
                {
                    //if its a part time employee, we move over to a part time employee object
                    PartTimeEmployee selectedPT = selectedEmployee as PartTimeEmployee;

                    //we fill the appropriate checkbox
                    rdoPartTime.IsChecked = true;
                    rdoFullTime.IsChecked = false;

                    //empty salary as it is not relevant
                    tbxSalary.Clear();

                    //convert hours and rate to string and display them
                    tbxHourlyRate.Text = selectedPT.HourlyRate.ToString();
                    tbxHoursWorked.Text = selectedPT.HoursWorked.ToString();

                    //and append the monthly pay to the monthly pay textbox
                    tblkMonthlyPay.Text += selectedPT.CalculateMonthlyPay().ToString();
                }
                else
                {
                    //if not part time, we move over to a full time object
                    FullTimeEmployee selectedFT = selectedEmployee as FullTimeEmployee;

                    //check the appropriate box
                    rdoPartTime.IsChecked = false;
                    rdoFullTime.IsChecked = true;

                    //convert salary to stirng and display it
                    tbxSalary.Text = selectedFT.Salary.ToString();

                    //clear irrelevant boxes
                    tbxHourlyRate.Clear();
                    tbxHoursWorked.Clear();

                    //and append the monthly pay
                    tblkMonthlyPay.Text += selectedFT.CalculateMonthlyPay().ToString();
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //this method sets all boxes and input fields to null or unchecked
            tbxFName.Clear();
            tbxLName.Clear();
            rdoFullTime.IsChecked = false;
            rdoPartTime.IsChecked = false;
            tbxSalary.Clear();
            tbxHourlyRate.Clear();
            tbxHoursWorked.Clear();
            //it also clears any text in the monthly pay area
            tblkMonthlyPay.Text = "Monthly Pay : ";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //we check whether we are creating a full or part time object
            if (rdoFullTime.IsChecked == true)
            {
                //if full time, we get the values of each input field
                string fName = tbxFName.Text;
                string lName = tbxLName.Text;
                decimal salary = decimal.Parse(tbxSalary.Text);
                //then we use a constructor to make a new full time employee
                Employees.Add(new FullTimeEmployee(fName, lName, salary));
            }
            else
            {
                //if part time, we get the values of each relevant input field
                string fName = tbxFName.Text;
                string lName = tbxLName.Text;
                decimal hourlyRate = decimal.Parse(tbxHourlyRate.Text);
                double hoursWorked = double.Parse(tbxHoursWorked.Text);
                //then we create using a constructor
                Employees.Add(new PartTimeEmployee(fName, lName, hourlyRate, hoursWorked));
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //we get the selected employee
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if (selectedEmployee != null)
            {
                //we set their name as the value of the text fields
                selectedEmployee.FirstName = tbxFName.Text;
                selectedEmployee.Surname = tbxLName.Text;

                //we check if it is part or full time
                if (selectedEmployee is PartTimeEmployee)
                {
                    PartTimeEmployee selectedPT = selectedEmployee as PartTimeEmployee;

                    //we then assign the hours and rates
                    selectedPT.HourlyRate = decimal.Parse(tbxHourlyRate.Text);
                    selectedPT.HoursWorked = double.Parse(tbxHoursWorked.Text);
                }
                else
                {
                    FullTimeEmployee selectedFT = selectedEmployee as FullTimeEmployee;

                    //we then assign the salary
                    selectedFT.Salary = decimal.Parse(tbxSalary.Text);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //we get the selected employee
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            //if the employee is not null
            if(selectedEmployee != null)
            {
                //we remove the employee from the collection
                Employees.Remove(selectedEmployee);

                //we then clear all input fields
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
