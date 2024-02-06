using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SmartInvest_DataBase.Models;

namespace SmartInvest_DataBase
{
    internal class Database
    {
        string CustomersGet = @"SELECT * FROM Customers;";
        string EmployeesGet = @"SELECT * FROM Employees;";
        string TransactionsGet = @"SELECT * FROM Transactions;";

        List<Customer> _customerList = new List<Customer>();
        List<Employee> _employeeList = new List<Employee>();
        List<Transaction> _transactionList = new List<Transaction>();
        int? customerCurrentId = 1;
        int? employeeCurrentId = 1;
        int? transactionCurrentId = 1;

        public Database(string conStr)
        {
            using SqliteConnection connection = new SqliteConnection(conStr);
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = CustomersGet;
                
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();   
                    customer.id = Convert.ToInt32(reader["id"]);
                    customer.FirstName = reader["FirstName"].ToString();
                    customer.LastName = reader["LastName"].ToString();
                    customer.Email = reader["Email"].ToString();

                    _customerList.Add(customer);
                    
                }
                reader.Close();

                command.CommandText = EmployeesGet;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();   
                    employee.id = Convert.ToInt32(reader["id"]);
                    employee.FirstName = reader["FirstName"].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.Email = reader["Email"].ToString();
                    employee.Position = reader["Position"].ToString();
                    employee.Salary = Convert.ToDouble(reader["Salary"]);

                    _employeeList.Add(employee);

                }
                
                reader.Close();


                command.CommandText = TransactionsGet;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.id = Convert.ToInt32(reader["id"]);
                    transaction.EmployeeInChargeID = Convert.ToInt32(reader["EmployeeInChargeID"]);
                    transaction.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                    transaction.Date = Convert.ToDateTime(reader["datetime"]);
                    transaction.Amount = Convert.ToDouble(reader["Amount"]);

                    _transactionList.Add(transaction);

                }
                reader.Close();

            }
            try
            {
                customerCurrentId = _customerList.Select(x => x.id).Max();
                employeeCurrentId = _employeeList.Select(x => x.id).Max();
                customerCurrentId = _transactionList.Select(x => x.id).Max();
            }
            catch
            {
                customerCurrentId = 0;
                employeeCurrentId = 0;
                transactionCurrentId = 0;
            }
        }

        public int getCustomerId()
        {
            customerCurrentId++;
            return Convert.ToInt32(customerCurrentId);
        }
        public void addCustomer(Customer customer)
        {
            _customerList.Add(customer);
        }
        public void displayCustomer()
        {
            if (_customerList!= null)
            {
                Console.WriteLine("\n\nCUSTOMER\n___________________________________________________________________________________\n Id\t|\tFirst Name\t|\tLast Name\t|\tEmail\n___________________________________________________________________________________");
                foreach (Customer customer in _customerList)
                {
                    Console.WriteLine(" " + customer.id.ToString() + "\t|\t" + customer.FirstName + "\t|\t" + customer.LastName + "\t|\t" + customer.Email);

                }
            }
        }

        public int getEmployeeId()
        {
            employeeCurrentId++;
            return Convert.ToInt32(employeeCurrentId);
        }

        public void addEmployee(Employee employee)
        {
            _employeeList.Add(employee);
        }

        public void displayEmployee()
        {
            if (_employeeList != null)
            {
                Console.WriteLine("\n\nEMPLOYEE\n___________________________________________________________________________________\n Id\t|\tFirst Name\t|\tLast Name\t|\tEmail\t|\tPosition\t|\tSalary\n___________________________________________________________________________________");
                foreach (Employee employee in _employeeList)
                {
                    Console.WriteLine(" " + employee.id.ToString() + "\t|\t" + employee.FirstName + "\t|\t" + employee.LastName + "\t|\t" + employee.Email + "\t|\t" + employee.Position + "\t|\t" + employee.Salary.ToString());

                }
            }
        }
        public int getTransactionId()
        {
            transactionCurrentId++;
            return Convert.ToInt32(transactionCurrentId);
        }
        public void addTransaction(Transaction transaction)
        {
            _transactionList.Add(transaction);
        }
        public void displayTransactions()
        {
            if (_transactionList != null)
            {
                Console.WriteLine("\n\nTRANSACTION\n___________________________________________________________________________________\n Id\t|\tEmployee ID\t|\tCustomer ID\t|\tDate\t|\tAmount\n___________________________________________________________________________________");
                foreach (Transaction transaction in _transactionList)
                {
                    Console.WriteLine(" " + transaction.id.ToString() + "\t|\t" + transaction.EmployeeInChargeID.ToString() + "\t|\t" + transaction.CustomerID.ToString() + "\t|\t" + transaction.Date.ToString() + "\t|\t" + transaction.Amount.ToString());

                }
            }

        }
    }
}
