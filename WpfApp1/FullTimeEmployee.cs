using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class FullTimeEmployee : Employee
    {
        public decimal Salary { get; set; }

        public FullTimeEmployee(string a, string b, decimal c) : base(a, b)
        {
            Salary = c;
        }

        public override decimal CalculateMonthlyPay()
        {
            return Salary / 12;
        }
    }
}
