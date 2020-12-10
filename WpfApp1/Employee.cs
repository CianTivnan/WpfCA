using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    abstract class Employee : IComparable<>
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public Employee(string a, string b)
        {
            FirstName = a;
            Surname = b;
        }

        abstract public decimal CalculateMonthlyPay();
    }
}
