using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    abstract class Employee : IComparable<Employee>
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public Employee(string a, string b)
        {
            FirstName = a;
            Surname = b;
        }

        abstract public decimal CalculateMonthlyPay();

        public int CompareTo(Employee emp)
        {
            int result = Surname.CompareTo(emp.Surname);
            return result;
        }

        public override string ToString()
        {
            string empType = "";
            if(this is PartTimeEmployee)
            {
                empType = "Part Time";
            }
            else
            {
                empType = "Full Time";
            }

            return string.Format($"{Surname.ToUpper()}, {FirstName} - {empType}");
        }
    }
}
