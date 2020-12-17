using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    abstract class Employee : IComparable<Employee>
    {
        //properties
        public string FirstName { get; set; }

        public string Surname { get; set; }

        //constructors
        public Employee() { }

        public Employee(string a, string b)
        {
            FirstName = a;
            Surname = b;
        }

        //abstract method for calculating pay
        abstract public decimal CalculateMonthlyPay();

        //sort method for sorting by surname
        public int CompareTo(Employee emp)
        {
            return Surname.CompareTo(emp.Surname);
        }

        //method for ToString()
        public override string ToString()
        {
            //we check what type of employee we are dealing with
            string empType = "";
            if(this is PartTimeEmployee)
            {
                empType = "Part Time";
            }
            else
            {
                empType = "Full Time";
            }

            //then we return the string with the employee type within
            return string.Format($"{Surname.ToUpper()}, {FirstName} - {empType}");
        }
    }
}
