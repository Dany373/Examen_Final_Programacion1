using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Final_Progra.Data.Modelos
{
    public class Heroes
    {
        private string connectionString = "Server=localhost; Database=personajes_marvel;Uid=root;Pwd = MATEOHERNANDEZ0909/h ";
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
        public string SuperPoder { get; set; }
        public string Afiliacion { get; set; }
        public int NivelPoder { get; set; }
        public DateTime FechaLiveAction { get; set; }

        public Heroes() { }

        public Heroes(int id, string nombre, string alias, string superpoder, string afiliacion, int nivelpoder, DateTime fechaliveaction)
        {
            ID = id;
            Nombre = nombre;
            Alias = alias;
            SuperPoder = superpoder;
            Afiliacion = afiliacion;
            NivelPoder = nivelpoder;
            FechaLiveAction = fechaliveaction;
        }

        public Heroes(string nombre, string alias, string superpoder, string afiliacion, int nivelpoder, DateTime fechaliveaction)
        {
            Nombre = nombre;
            Alias = alias;
            SuperPoder = superpoder;
            Afiliacion = afiliacion;
            NivelPoder = nivelpoder;
            FechaLiveAction = fechaliveaction;

        }

        public bool Probar()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public void InsertarPersonaje(Heroes heroe)
        {


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO heroes (Nombre, Alias, Super_Poder, Afiliacion, Nivel_Poder, Fecha_LiveAction) " +
                           "VALUES (@Nombre, @Alias, @SuperPoder, @Afiliacion, @NivelPoder, @FechaLiveAction)";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Nombre", heroe.Nombre);
                cmd.Parameters.AddWithValue("@Alias", heroe.Alias);
                cmd.Parameters.AddWithValue("@SuperPoder", heroe.SuperPoder);
                cmd.Parameters.AddWithValue("@Afiliacion", heroe.Afiliacion);
                cmd.Parameters.AddWithValue("@NivelPoder", heroe.NivelPoder);
                cmd.Parameters.AddWithValue("@FechaLiveAction", heroe.FechaLiveAction);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }

        }
        public void ElimiarPersonaje(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM heroes WHERE ID = @ID";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                    throw;
                }
            }
        }
        public bool ActualizarPersonaje(Heroes heroe)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE heroes SET Nombre = @Nombre, Alias = @Alias, Super_Poder = @SuperPoder, Afiliacion = @Afiliacion, Nivel_Poder = @NivelPoder, Fecha_LiveAction = @FechaLiveAction WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("ID", heroe.ID);
                cmd.Parameters.AddWithValue("@Nombre", heroe.Nombre);
                cmd.Parameters.AddWithValue("@Alias", heroe.Alias);
                cmd.Parameters.AddWithValue("@SuperPoder", heroe.SuperPoder);
                cmd.Parameters.AddWithValue("@Afiliacion", heroe.Afiliacion);
                cmd.Parameters.AddWithValue("@NivelPoder", heroe.NivelPoder);
                cmd.Parameters.AddWithValue("@FechaLiveAction", heroe.FechaLiveAction);

                try
                {
                    connection.Open();
                    int affectedRows = cmd.ExecuteNonQuery();
                    return affectedRows > 0; // Retorna true si se actualizó al menos una fila
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }
    }
}

        
    


