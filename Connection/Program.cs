using Connection.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Connection
{
    class Program
    {
        SqlConnection sqlConnection;

        string connectionString = "Data Source = NEKOZAWA; Initial Catalog = DTSMCC; User Id = user; Password = 12345678;";
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("------------------View By Id----------------------------------");
            program.ViewById(1301174265);

            Console.WriteLine("------------------Insert Provinsi-----------------------------");
            Console.WriteLine("Table sebelum di insert : ");
            program.ViewAllProvinsi();
            Provinsi provinsi = new Provinsi()
            {
                idProvinsi = "KSN",
                namaProvinsi = "Kalimantan Selatan"
            };
            program.InsertProvinsi(provinsi);
            Console.WriteLine();
            Console.WriteLine("Table setelah di insert : ");
            program.ViewAllProvinsi();

            Console.WriteLine("------------------Update Grade---------------------------------");
            Console.WriteLine("Table sebelum di update : ");
            program.ViewAllGrade();
            Grade grade = new Grade()
            {
                idGrade = "GA",
                grade = "A",
                gaji = 8000000
            };
            program.UpdateGrade(grade);
            Console.WriteLine();
            Console.WriteLine("Table setelah di update : ");
            program.ViewAllGrade();

            Console.WriteLine("------------------Delete Provinsi------------------------------");
            Console.WriteLine("Table sebelum di delete : ");
            program.ViewAllProvinsi();
            Console.WriteLine();
            Console.WriteLine("Table setelah di delete : ");
            program.DeleteProvinsi("KSN");
            program.ViewAllProvinsi();

        }

        void ViewAllProvinsi()
        {
            string query = "SELECT * FROM Provinsi";

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
                            Console.WriteLine(sqlDataReader[0] + " | " + sqlDataReader[1]);
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
        void ViewAllGrade()
        {
            string query = "SELECT * FROM Grade";

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
                            Console.WriteLine(sqlDataReader[0] + " | " + sqlDataReader[1] + " | " + sqlDataReader[2]);
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
        void ViewById(int id)
        {
            string query = "SELECT * FROM pegawai WHERE idPegawai = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

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
                            Console.WriteLine(sqlDataReader[0] + " | " + sqlDataReader[1] + " | " + sqlDataReader[2] + " | "
                                + sqlDataReader[3] + " | " + sqlDataReader[4] + " | " + sqlDataReader[5] + " | " + sqlDataReader[6]
                                + " | " + sqlDataReader[7] + " | " + sqlDataReader[8]);
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
        void InsertProvinsi(Provinsi provinsi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Provinsi (IdProvinsi, NamaProvinsi) VALUES (@id, @nama)";
                    command.Parameters.AddWithValue("@id", provinsi.idProvinsi);
                    command.Parameters.AddWithValue("@nama", provinsi.namaProvinsi);

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Terdapat Error !");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        void UpdateGrade(Grade grade)
        {
            var sql = "UPDATE Grade SET Gaji = @gaji WHERE IdGrade = @id";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.NVarChar).Value = grade.idGrade;
                        command.Parameters.Add("@gaji", SqlDbType.NVarChar).Value = grade.gaji;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Terdapat Error !");
            }
        }
        void DeleteProvinsi(string id)
        {
            var sql = "DELETE FROM provinsi WHERE idProvinsi = @id";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Terdapat Error !");
            }
        }
    }
}
