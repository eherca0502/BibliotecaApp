using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaApp
{
    public class PrestamoDAL
    {
        public static void Agregar(int libroId, int usuarioId)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Prestamos (LibroId, UsuarioId, Estado) 
                                 VALUES (@LibroId, @UsuarioId, 'Prestado');
                                 UPDATE Libros SET Disponible = 0 WHERE Id = @LibroId;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LibroId", libroId);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Devolver(int prestamoId, int libroId)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"UPDATE Prestamos 
                                 SET Estado='Devuelto', FechaDevolucion=GETDATE() 
                                 WHERE Id=@Id;
                                 UPDATE Libros SET Disponible = 1 WHERE Id=@LibroId;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", prestamoId);
                cmd.Parameters.AddWithValue("@LibroId", libroId);
                cmd.ExecuteNonQuery();
            }
        }
        public static List<Prestamo> Listar()
        {
            List<Prestamo> lista = new List<Prestamo>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT P.Id, L.Titulo, U.Nombre, P.FechaPrestamo, 
                                        P.FechaDevolucion, P.Estado, P.LibroId, P.UsuarioId
                                 FROM Prestamos P
                                 INNER JOIN Libros L ON P.LibroId = L.Id
                                 INNER JOIN Usuarios U ON P.UsuarioId = U.Id";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Prestamo p = new Prestamo
                    {
                        Id = reader.GetInt32(0),
                        LibroTitulo = reader.GetString(1),
                        UsuarioNombre = reader.GetString(2),
                        FechaPrestamo = reader.GetDateTime(3),
                        FechaDevolucion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        Estado = reader.GetString(5),
                        LibroId = reader.GetInt32(6),
                        UsuarioId = reader.GetInt32(7)
                    };
                    lista.Add(p);
                }
            }
            return lista;
        }
    }
}
