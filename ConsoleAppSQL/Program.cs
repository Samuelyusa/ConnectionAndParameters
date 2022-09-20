using System;
using System.Data.SqlClient;
using ConsoleAppSQL.Models;

namespace MCCDSTS
{
    class Program
    {
        SqlConnection? sqlConnection;

        /*
             * Data Source
             * Initial Catalog
             * User ID
             * Password
        */

        string connectionString = "Data Source=DESKTOP-GK9TR5F;Initial Catalog=DTSMCC001;User ID=mccdts1;Password=mccdts;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        static void Main(string[] args)
        {
            Program program = new Program();
            //program.getById(1);
            
            Employees employees = new Employees()
            {
                EmployeeId = 26,
                FirstName = "Samuel Yusa"
            };
            //program.Insert(employees);
            //program.Update(employees);
            program.DeleteById(26);
            program.getAll();
        }

        void getAll()
        {
            string query = "select * from Employees";
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("ID: " + sqlDataReader[0] + " -- Name: " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        void getById(int EmployeeId)
        {
            string query = "select * from employees where EmployeeId = @EmployeeId";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@EmployeeId";
            sqlParameter.Value = EmployeeId;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("ID: " + sqlDataReader[0] + " -- Name: " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        void Insert(Employees employees)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Employees " +
                        "(EmployeeID, FirstName) VALUES (@EmployeeId, @FirstName)";

                    SqlParameter sqlParameterId = new SqlParameter();
                    sqlParameterId.ParameterName = "@EmployeeId";
                    sqlParameterId.Value = employees.EmployeeId;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@FirstName";
                    sqlParameter.Value = employees.FirstName;

                    sqlCommand.Parameters.Add(sqlParameterId);
                    sqlCommand.Parameters.Add(sqlParameter);

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        void Update(Employees employees)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "update Employees " +
                        "set FirstName = @FirstName where EmployeeId = @EmployeeId";

                    SqlParameter sqlParameterId = new SqlParameter();
                    sqlParameterId.ParameterName = "@EmployeeId";
                    sqlParameterId.Value = employees.EmployeeId;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@FirstName";
                    sqlParameter.Value = employees.FirstName;

                    sqlCommand.Parameters.Add(sqlParameterId);
                    sqlCommand.Parameters.Add(sqlParameter);

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        void DeleteById(int EmployeeId)
        {
            string query = "delete from employees where EmployeeId = @EmployeeId";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@EmployeeId";
            sqlParameter.Value = EmployeeId;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("ID: " + sqlDataReader[0] + " -- Name: " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }
    }
}