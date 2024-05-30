using Examen_Final_Progra.Data.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Final_Progra.Data
{
    internal class ConexionMySql
    {
        string connectionString = "server=localhost;database=personajes_marvel;user=root;password=MATEOHERNANDEZ0909/h";
        MySqlConnection connection;


        //constructor
        public ConexionMySql()
        {
            connection = new MySqlConnection(connectionString);
        }

        public List<Heroes> ObtenerPersonajes()
        {
            List<Heroes> heroes = new List<Heroes>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM heroes";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    int count = 0;
                    while (reader.Read())
                    {
                        Heroes heroe = new Heroes
                        (
                            id: reader.GetInt32("ID"),
                            nombre: reader.GetString("Nombre"),
                            alias: reader.GetString("Alias"),
                            superpoder: reader.GetString("Super_Poder"),
                            afiliacion: reader.GetString("Afiliacion"),
                            nivelpoder: reader.GetInt32("Nivel_Poder"),
                            fechaliveaction: reader.GetDateTime("Fecha_LiveAction")

                            );


                        heroes.Add(heroe);
                        count++;

                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return heroes;
        }
    }
}
