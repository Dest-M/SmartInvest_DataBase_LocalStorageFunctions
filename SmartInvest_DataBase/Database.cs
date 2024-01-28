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

        List<Customer> _customerList;
        List<Employee> _employeeList;
        List<Transaction> _transactionList;

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
        }


    }
}
