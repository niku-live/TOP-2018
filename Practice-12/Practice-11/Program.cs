using System;
using System.Data.SqlClient;
namespace Practice_11
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            var con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + @"Database3.mdf;Integrated Security=True");
            con2.Open();
            var cmd = new SqlCommand("INSERT INTO [Table] (Id, Name, Amount) VALUES (1, 'Test', 1)", con2);
            cmd.ExecuteNonQuery();
            con2.Close();
           
            using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=DynamicsNAV100;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [Table]", connection);
                connection.Open();

                //DataReader example
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(1)}");

                }
                reader.Close();

                //Direct Update example 
                string newName = "Test";
                int id = 3;
                //Very bad
                command = new SqlCommand("UPDATE [Table] SET Name = '" + newName + "' where id = " + id, connection);
                command.ExecuteNonQuery();

                //Bad
                command = new SqlCommand($"UPDATE [Table] SET Name = '{newName}' where id = {id}", connection);
                command.ExecuteNonQuery();

                //Better
                command = new SqlCommand("UPDATE [Table] SET Name = @name where id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", newName);
                command.ExecuteNonQuery();


                var dataSet = new System.Data.DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Table]", connection);
                dataAdapter.Fill(dataSet);


                dataSet.Tables[0].Rows[0][1] = "Bla";
                dataSet.Tables["Table"].Rows[0]["Name"] = "Bla";



                dataAdapter.DeleteCommand = new SqlCommand("DELETE FROM [Table] WHERE [Id] = @id", connection);
                dataAdapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int, 0, "Id"));

                dataAdapter.UpdateCommand = new SqlCommand("UPDATE [Table] SET [Name] = @name, [Amount] = @amount WHERE [Id] = @id", connection);
                dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar, 10, "Name"));
                dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@amount", System.Data.SqlDbType.Decimal, 0, "Amount"));
                dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int, 0, "Id"));

                dataAdapter.InsertCommand = new SqlCommand("INSERT INTO [Table] (Id, Name, Amount) VALUES (@id, @name, @amount)", connection);
                dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.VarChar, 10, "Name"));
                dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@amount", System.Data.SqlDbType.Decimal, 0, "Amount"));
                dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int, 0, "Id"));

                dataAdapter.Fill(dataSet);
                var table = dataSet.Tables[0];
                //Insert
                var newRow = table.NewRow();
                newRow["Id"] = 10;  //Set field by name
                newRow[1] = "Naujas"; //Set field by index
                newRow["Amount"] = 10M;
                table.Rows.Add(newRow);
                //Delete
                var rowToDelete = table.Rows[2];
                rowToDelete.Delete();
                //Update
                var rowToUpdate = table.Rows[0];
                rowToUpdate[2] = 5M;
                rowToUpdate["Name"] = "Updated";

                //Save changes
                dataAdapter.Update(dataSet);


            }
           

    


        }
    }
}
