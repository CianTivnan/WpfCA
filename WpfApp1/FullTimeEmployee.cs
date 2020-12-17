using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class FullTimeEmployee : Employee
    {
        //properties
        public decimal Salary { get; set; }

        //constructors
        public FullTimeEmployee() { }

        public FullTimeEmployee(string a, string b, decimal c) : base(a, b)
        {
            Salary = c;
        }

        //override method for calculating pay
        public override decimal CalculateMonthlyPay()
        {
            return Salary / 12;
        }
    }
}
