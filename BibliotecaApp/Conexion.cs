using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BibliotecaApp
{
    internal class Conexion
    {
        private static string cadena = "Server=DESKTOP-UG7RE3Q\\SQLEXPRESS;Database=Biblioteca;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}
