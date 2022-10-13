using Npgsql;
using pruebaConexionPostgreSQLV.Models.DTOs;
using System.Data;

namespace pruebaConexionPostgreSQLV.Models.Consultas
{
    /* ConsultasPostgreSQL - Clase que contiene los métodos que definen las diferentes consultas a BD
    * PostgreSQL que se pueden llevar a cabo.
    * @author garfe
    * 06/10/2022
    */
    public class ConsultasPostgreSQL
    {
        /* ConsultaSelectPostgreSQL - Método que consulta la tabla alumnos completa
        * 02/10/2022
        */
        public static List<AlumnoDTO> ConsultaSelectPostgreSQL(NpgsqlConnection conexionGenerada)
        {
            List<AlumnoDTO> listAlumnos = new List<AlumnoDTO>();
            try
            {
                //Se define y ejecuta la consulta Select
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT * FROM \"proyectoEclipse\".\"alumnos\"", conexionGenerada);
                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

                //Paso de DataReader a lista de alumnoDTO
                //Al ser el método estático no es necesario instanciar un objeto de la
                //clase para llamar al método
                listAlumnos = DTOs.ADTO.ReaderAListDTO.ReaderAListAlumnoDTO(resultadoConsulta);
                int cont = listAlumnos.Count();
                System.Console.WriteLine("[INFORMACIÓN-ConsultasPostgreSQL-ConsultaSelectPostgreSQL] Lista compuesta por: " + cont + " alumnos");

                System.Console.WriteLine("[INFORMACIÓN-ConsultasPostgreSQL-ConsultaSelectPostgreSQL] Cierre conjunto de datos");
                resultadoConsulta.Close();

            }
            catch (Exception e)
            {

                System.Console.WriteLine("[ERROR-ConsultasPostgreSQL-ConsultaSelectPostgreSQL] Error al ejecutar consulta: " + e);
                conexionGenerada.Close();

            }
            return listAlumnos;
        }
        /* insertNuevoAlumno - Método que inserta un nuevo alumno en la tabla alumnos.
        * 02/10/2022
        */
        public static void insertNuevoAlumno(String consultaGenerada, NpgsqlConnection conexionGenerada)
        {
            System.Console.WriteLine("[ERROR-ConsultasPostgreSQL-insertNuevoAlumno] Entra en insertNuevoAlumno");

            try
            {

                NpgsqlCommand consulta = new NpgsqlCommand();
                consulta.Connection = conexionGenerada;
                consulta.CommandText = consultaGenerada;
                consulta.ExecuteNonQuery();
          
            }
            catch (Exception e)
            {
                System.Console.WriteLine("[ERROR-ConsultasPostgreSQL-insertNuevoAlumno] Error al insertar: " + e);
            }
        }
    }
}
