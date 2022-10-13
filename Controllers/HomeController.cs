using Microsoft.AspNetCore.Mvc;
using Npgsql;
using pruebaConexionPostgreSQLV.Models.Conexiones;
using pruebaConexionPostgreSQLV.Models;
using pruebaConexionPostgreSQLV.Util;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using pruebaConexionPostgreSQLV.Models.DTOs;
using pruebaConexionPostgreSQLV.Models.Consultas;
using Microsoft.AspNetCore.Components.Routing;
using proyectoMultiConsultasMVCV.Models;

namespace pruebaConexionPostgreSQLV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(ConexionPostgreSQL conexionPostgreSQL)
        {
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Entra en Index");

            //CONSTANTES
            const string HOST = VariablesConexionPostgreSQL.HOST;
            const string PORT = VariablesConexionPostgreSQL.PORT;
            const string USER = VariablesConexionPostgreSQL.USER;
            const string PASS = VariablesConexionPostgreSQL.PASS;
            const string DB = VariablesConexionPostgreSQL.DB;

            //Se genera una conexión a PostgreSQL y validamos que esté abierta fuera del método
            var estadoGenerada = "";
            NpgsqlConnection conexionGenerada = new NpgsqlConnection();
            List<AlumnoDTO> listAlumnoDTO = new List<AlumnoDTO>();
            //Al no tratarse de un método estático es necesario instanciar la clase
            // para llamarlo
            conexionGenerada = conexionPostgreSQL.GeneraConexion(HOST, PORT, DB, USER, PASS);
            estadoGenerada = conexionGenerada.State.ToString();
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Estado conexión generada: " + estadoGenerada);

            //Se realiza insert en la tabla alumnos
            ConsultasPostgreSQL.insertNuevoAlumno("INSERT INTO \"proyectoEclipse\".\"alumnos\" (id_alumno,nombre,apellidos,email) VALUES(21,'Paco','Fernández','pf@altair.es')", conexionGenerada);

            //Se realiza la consulta y se guarda una lista de alumnosDTO
            listAlumnoDTO = ConsultasPostgreSQL.ConsultaSelectPostgreSQL(conexionGenerada);
            int cont = listAlumnoDTO.Count();
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Lista compuesta por: " + cont + " alumnos");
            foreach (AlumnoDTO alumno in listAlumnoDTO)
            {

                System.Console.WriteLine(alumno.id_alumno + " " + alumno.nombre + " " +
                    alumno.apellidos + " " + alumno.email);

            }
            //Se cierra la conexión
            conexionPostgreSQL.CierraConexion(conexionGenerada);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}