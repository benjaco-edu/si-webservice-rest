using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MiniprojectSoapService
{
    public class RelationsDb : IRelationDb
    {
        string connectionString, DbName, connectionStringAN;

        public RelationsDb()
        {
            connectionString="server=mysql01;Uid=root;Pwd=test1234";
            DbName = "TeacherDb";
            connectionStringAN = connectionString + ";Database="+DbName;
        }

        public void NewDb(){
            CreateDb();
            CreateTable();
            PopulateTable();
        }
        private void CreateDb(){
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sqlstr = @"DROP DATABASE IF EXISTS "+ DbName+ "; CREATE DATABASE " + DbName;
                var command = new MySqlCommand(sqlstr,conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine($"New Database created : {DbName}");
            }
        }
        private void CreateTable(){
            using (var conn = new MySqlConnection(connectionStringAN))
            {
                conn.Open();
                var sqlstr = @"CREATE TABLE IF NOT EXISTS Teachers (
                    Id INT NOT NULL AUTO_INCREMENT,
                    Name VARCHAR(100),
                    Email VARCHAR(100),
                    Visible BOOLEAN,
                    PRIMARY KEY(Id)
                )";
                var command = new MySqlCommand(sqlstr,conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("New table created");
            }
        }
        private void PopulateTable(){
            using (var conn = new MySqlConnection(connectionStringAN))
            {
                conn.Open();
                var sqlstr = @"
                INSERT INTO Teachers (Name, Email, Visible) values ('Bob the teacher', 'bob@gmail.com', true);
                INSERT INTO Teachers (Name, Email, Visible) values ('Liz the teacher', 'liz@gmail.com', true);
                INSERT INTO Teachers (Name, Email, Visible) values ('Eve the teacher', 'eve@gmail.com', true);
                INSERT INTO Teachers (Name, Email, Visible) values ('Tim the teacher', 'tim@gmail.com', true);
                ";
                var command = new MySqlCommand(sqlstr,conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("Teacher table populated");
            }

        }


        public Teacher Read(int id)
        {
            Teacher res = new Teacher();
            using (var conn = new MySqlConnection(connectionStringAN))
            {
                conn.Open();
                var sqlstr = @"SELECT * FROM Teachers WHERE Id = @ID AND Visible=true";
                var command = new MySqlCommand(sqlstr,conn);
                command.Parameters.Add("@ID", MySqlDbType.Int32);
                command.Parameters["@ID"].Value = id;
                var reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                    res.Id = reader.GetInt32("Id");
                    res.Name = reader.GetString("Name");
                    res.Email = reader.GetString("Email");
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("bleh" + ex.Message);
                }
            }
            return res;
        }

        public List<Teacher> ReadAll()
        {
            List<Teacher> res = new List<Teacher>();
            using (var conn = new MySqlConnection(connectionStringAN))
            {
                conn.Open();
                var sqlstr = @"SELECT * FROM Teachers WHERE Visible=true";
                var command = new MySqlCommand(sqlstr,conn);
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Teacher(){
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Email = reader.GetString("Email")
                            }
                        );
                    }                    
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("bleh" + ex.Message);
                }
            }
            return res;
        }

        public void CreateTeacher(Teacher teacherData)
        {
            using(var conn = new MySqlConnection(connectionStringAN)){
                conn.Open();
                var sqlstr = @"INSERT INTO Teachers (Name, Email, Visible) VALUES (@NAME, @EMAIL, true)";
                var command = new MySqlCommand(sqlstr,conn);
                command.Parameters.Add("@NAME", MySqlDbType.String);
                command.Parameters.Add("@EMAIL", MySqlDbType.String);
                command.Parameters["@NAME"].Value = teacherData.Name;
                command.Parameters["@EMAIL"].Value = teacherData.Email;
                command.ExecuteNonQuery();
                System.Console.WriteLine("Teacher added to table"); 
            }
        }

        public void UpdateTeacher(int id, Teacher teacherData)
        {
            using (var conn = new MySqlConnection(connectionStringAN))
            {
                conn.Open();
                var sqlstr = @"UPDATE Teachers SET Name = @NAME, Email=@EMAIL WHERE Id=@ID";
                var command = new MySqlCommand(sqlstr,conn);
                command.Parameters.Add("@NAME", MySqlDbType.String);
                command.Parameters.Add("@EMAIL", MySqlDbType.String);
                command.Parameters.Add("@ID", MySqlDbType.Int32);
                command.Parameters["@NAME"].Value = teacherData.Name;
                command.Parameters["@EMAIL"].Value = teacherData.Email;
                command.Parameters["@ID"].Value = id;
                command.ExecuteNonQuery();
                System.Console.WriteLine("Teacher info updated");
            }
        }

        public void DeleteTeacher(int id)
        {
            using (var conn = new MySqlConnection(connectionStringAN))
            {
                conn.Open();
                var sqlstr = "UPDATE Teachers SET Visible=false WHERE Id = @ID";
                var command = new MySqlCommand(sqlstr,conn);
                command.Parameters.Add("@ID", MySqlDbType.Int32);
                command.Parameters["@ID"].Value = id;
                command.ExecuteNonQuery();
                System.Console.WriteLine("Teacher removed from table");
            }
        }
    }
}