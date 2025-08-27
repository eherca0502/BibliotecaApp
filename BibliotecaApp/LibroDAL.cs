using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BibliotecaApp
{
    public class LibroDAL
    {
        public static void Agregar(Libro libro)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "INSERT INTO Libros (Titulo, Autor, Anio, Disponible) VALUES (@Titulo, @Autor, @Anio, @Disponible)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                cmd.Parameters.AddWithValue("@Disponible", libro.Disponible);
                cmd.ExecuteNonQuery();
            }
        }
        public static void Editar(Libro libro)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "UPDATE Libros SET Titulo=@Titulo, Autor=@Autor, Anio=@Anio, Disponible=@Disponible WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", libro.Id);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                cmd.Parameters.AddWithValue("@Disponible", libro.Disponible);
                cmd.ExecuteNonQuery();
            }
        }
        public static void Eliminar(int id)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "DELETE FROM Libros WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public static List<Libro> Listar()
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Libros";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Libro libro = new Libro
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Autor = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Anio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        Disponible = reader.GetBoolean(4)
                    };
                    lista.Add(libro);
                }
            }
            return lista;
        }
    }
}
