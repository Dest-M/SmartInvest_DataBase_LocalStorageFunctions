using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SmartInvest_DataBase.Models;

namespace SmartInvest_DataBase
{
    internal class Fill
    {
        public void fillCustomer(Database data)
        
        {
            string input;
            Customer customer = new Customer();
            customer.id = data.getCustomerId();
            customer.FirstName = dumbTypingCheck("First Name");
            customer.LastName = dumbTypingCheck("Last Name");
            customer.Email = dumbTypingCheck("Email");

            data.addCustomer(customer);
        }

        

        public void fillEmployee(Database data)
        {
            string input;
            Employee employee = new Employee();
            employee.id = data.getEmployeeId();
            employee.FirstName = dumbTypingCheck("First Name");
            employee.LastName = dumbTypingCheck("Last Name");
            employee.Email = dumbTypingCheck("Email");
            employee.Position = dumbTypingCheck("Position");
            employee.Salary = Convert.ToDouble(dumbTypingCheck("Salary (in digits)"));


            data.addEmployee(employee);
        }

        public void fillTransaction(Database data)
        {
            string input;
            Transaction transaction = new Transaction();
            transaction.id = data.getTransactionId();
            transaction.EmployeeInChargeID = Convert.ToInt32(dumbTypingCheck("ID of the Employee in Charge"));
            transaction.CustomerID = Convert.ToInt32(dumbTypingCheck("Customer ID"));
            transaction.Date = Convert.ToDateTime(dumbTypingCheck("Date and time"));
            transaction.Amount = Convert.ToDouble(dumbTypingCheck("Amount (in digits)")); ;
                


            data.addTransaction(transaction);
        }

        private string dumbTypingCheck(string type)
        {
            string input;
            while (true)
            {
                Console.WriteLine("Please enter " + type);
                input = Console.ReadLine();
                if (input != null || input != "\n" || input != " ")
                {
                    return input;
                }
            }
        }
        public int dumbChoiceCheck(int max)
        {
            string input;
            while (true)
            {
                Console.WriteLine("Enter Choice");
                input = Console.ReadLine();
                if(Convert.ToInt16(input)> max || input == null)
                {
                    Console.WriteLine("ERROR. Enter digit within given range to make the choice (1-" + max.ToString());
                }
                else
                {
                    return Convert.ToInt16(input);
                }
            }
        }
    }
}
