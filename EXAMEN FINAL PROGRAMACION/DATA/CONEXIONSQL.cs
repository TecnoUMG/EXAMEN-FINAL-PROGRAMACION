using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EXAMEN_FINAL_PROGRAMACION.DATA
{
    internal class CONEXIONSQL
    {
        private string connectionString = "server=localhost; database=armas_cyberpunk; Uid=root; password=Saulin362023";
        MySqlConnection connection;

        public CONEXIONSQL()
        {
            connection = new MySqlConnection(connectionString);
        }

        public DataTable LeerArmas()
        {
            DataTable armas = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Armas";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(armas);
                    }
                }
            }

            return armas;
        }

        public void Insertar(string nombre, string tipo, int daño, int cadencia, float presicion, string descripcion)
        {
            try
            {
                string query = "INSERT INTO Armas (Nombre, Tipo, Daño, Cadencia, Presicion, Descripcion) " +
                               "VALUES (@Nombre, @Tipo, @Daño, @Cadencia, @Presicion, @Descripcion)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Daño", daño);
                cmd.Parameters.AddWithValue("@Cadencia", cadencia);
                cmd.Parameters.AddWithValue("@Presicion", presicion);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ActualizarArma(int id, string nombre, string tipo, int daño, int cadencia, float presicion, string descripcion)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE Armas SET Nombre = @Nombre, Tipo = @Tipo, Daño = @Daño, Cadencia = @Cadencia, Presicion = @Presicion, Descripcion = @Descripcion WHERE Id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Tipo", tipo);
                        cmd.Parameters.AddWithValue("@Daño", daño);
                        cmd.Parameters.AddWithValue("@Cadencia", cadencia);
                        cmd.Parameters.AddWithValue("@Presicion", presicion);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void EliminarArma(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Armas WHERE Id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado == 0)
                        {
                            MessageBox.Show("No se encontró ningún registro con el ID proporcionado.");
                        }
                        else
                        {
                            MessageBox.Show("El arma fue eliminada correctamente.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message);
            }
        }

        public ArmasModels BuscarPorId(int id)
        {
            ArmasModels arma = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Armas WHERE Id = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                arma = new ArmasModels
                                {
                                    Id = reader.GetInt32("Id"),
                                    Nombre = reader.GetString("Nombre"),
                                    Tipo = reader.GetString("Tipo"),
                                    Daño = reader.GetInt32("Daño"),
                                    Cadencia = reader.GetInt32("Cadencia"),
                                    Presicion = reader.GetFloat("Presicion"),
                                    Descripcion = reader.GetString("Descripcion")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar por ID: " + ex.Message);
            }

            return arma;
        }


    }
}



