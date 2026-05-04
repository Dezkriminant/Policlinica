using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Policlinica.DB;

public class UserRepository
{
    MySqlConnection connection;

    public UserRepository(IOptions<DatabaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }


    public void InsertUser(User user)
    {
        var sql1 = "INSERT INTO Polyclinica.users (id, name, password) VALUES (0, @name, @password); ";
        var sql2 = "SELECT max(id) as id FROM Polyclinica.users;";

    }


    public List<User> GetUsersByTest()
    {
        List<User> result = new List<User>();
        string sql = "select  * from users";
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    result.Add(new User
                    {
                        Id = dr.GetInt32("id"),
                        Name = dr.GetString("name"),
                        Password = dr.GetString("password"),

                    });
                }
            }

            connection.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }


    public List<User> CheckLoginAndPassword(string name, string password)
    {
        List<User> us = new List<User>();
        string sql = @"select * from `users` where `Name` = @name and `Password` = @password";
        try
        {
            using (var mc = new MySqlCommand(sql, connection))
            {
                mc.Parameters.AddWithValue("@name", name);
                mc.Parameters.AddWithValue("@password", password);
                var reader = mc.ExecuteReader();
                while (reader.Read())
                {
                    us.Add(new User()
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Password = reader.GetString("password"),
                    });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

        }

        return us;
    }
}