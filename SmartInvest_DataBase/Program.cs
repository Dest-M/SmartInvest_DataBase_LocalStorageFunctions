using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Data.Sqlite;


// Company Name:        SmartInvest
//
//Table 1:              Customers
//Table 2:              Employees
//Table 3:              Transactions


string conStr = @"Data Source = SmartInvestSQL.sql";
string filename = @"SmartInvestSQL.sql";
string CustomersCreate = @"CREATE TABLE Customers(
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       FirstName text,
                       LastName text,
                       Email text
                           );";
string EmployeeCreate = @"CREATE TABLE Employees(
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       FirstName text,
                       LastName text,
                       Email text,
                       Position text,
                       Salary double
                           );";
string TransactionsCreate = @"CREATE TABLE Transactions(
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       EmployeeInChargeID int,
                       CustomerID int,
                        Date datetime,
                        Amount double,
                       FOREIGN KEY(EmployeeInChargeID) REFERENCES Employees(id),
                       FOREIGN KEY(CustomerID) REFERENCES Customers(id)
                           );";





if (!File.Exists(filename))
{
    File.Create(filename).Close();

};

using SqliteConnection connection = new SqliteConnection(conStr);

connection.Open();
using (var command = connection.CreateCommand())
{
    command.CommandType = System.Data.CommandType.Text;
    command.CommandText = CustomersCreate;
    command.ExecuteNonQuery();
    command.CommandText = EmployeeCreate;
    command.ExecuteNonQuery();
    command.CommandText = TransactionsCreate;
    command.ExecuteNonQuery();



}


connection.Close();



