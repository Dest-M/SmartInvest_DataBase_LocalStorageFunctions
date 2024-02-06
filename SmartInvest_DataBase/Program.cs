using System.Data;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Data.Sqlite;
using SmartInvest_DataBase;


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



        connection.Close();
    }

};
Database database = new Database(conStr);
Fill fill = new Fill();
bool endflag = false;
Console.WriteLine("Welcome");
while (!endflag)
{
    Console.WriteLine("Avaliable tables:\n1  Customer\n2  Employee\n3  Transaction\n---------------\nAvaliable actions:\n1 - Display a table\n2 - Enter a new row into a table\n3 - Exit Program");
    int choice = fill.dumbChoiceCheck(3);
    switch (choice)
    {
        case 1:
            displayTablechoice();
            break;
        case 2:
            enterTablechoice();
            break;
        case 3:
            endflag = true;
            break;
        default:
            break;

    }



}

void enterTablechoice()
{
    Console.WriteLine("Avaliable tables:\n1  Customer\n2  Employee\n3  Transaction");
    int choice = fill.dumbChoiceCheck(3);
    switch (choice)
    {
        case 1:
            fill.fillCustomer(database);
            break;
        case 2:
            fill.fillEmployee(database);
            break;
        case 3:
            fill.fillTransaction(database);
            break;
        default:
            break;

    }
}
void displayTablechoice()
{
    Console.WriteLine("Avaliable tables:\n*  Customer\n*  Employee\n*  Transaction");
    int choice = fill.dumbChoiceCheck(3);
    switch (choice)
    {
        case 1:
            database.displayCustomer();
            break;
        case 2:
            database.displayEmployee();
            break;
         case 3:
            database.displayTransactions();
            break;
        default:
            break;
        
    }

}

