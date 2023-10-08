using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class Program
{
    public const string ProfesorFilePath = "profesores.json";
    private const string AlmacenistaFilePath = "almacenistas.json";

    public static void Main()
    {
        //Variables reutilizables
        int id = 0;
        string nombreCompleto = string.Empty;
        string nominaEncriptada = string.Empty;
        string passwordEncriptada = string.Empty;
        string materiasInput = string.Empty;
        string[] materiasQueImparte = new string[0];
        string division = string.Empty;
        string opcion2 = string.Empty;

        var profesoresHelperJSON = new ProfesorHelperJSON();
        var profesoresHelperXML = new ProfesorHelperXML();
        var encriptacionHelper = new EncriptacionHelper();
        var almacenistaHelperJSON = new AlmacenistaHelperJSON();
        var almacenistaHelperXML = new AlmacenistaHelperXML();
        bool auth = false;
        Console.Clear();
        while (auth == false)
        {
            Console.WriteLine("Bienvenido al sistema de almacen.");
            Console.WriteLine("Selecciona una opción:");
            Console.WriteLine("1. Registrar un almacenista");
            Console.WriteLine("2. Iniciar Sesion como almacenista");
            Console.WriteLine("3. Salir");

            int opcion;
            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {
                    case 1:
                        Console.Write("Nombre completo del Almacenista: ");
                        nombreCompleto = Console.ReadLine() ?? string.Empty;
                        nombreCompleto = VariableHelper.StringNotEmpty(nombreCompleto);
                        Console.Write("Password: ");
                        passwordEncriptada = Console.ReadLine() ?? string.Empty;
                        passwordEncriptada = VariableHelper.StringNotEmpty(passwordEncriptada);
                        almacenistaHelperJSON.CrearAlmacenista(nombreCompleto, passwordEncriptada);
                        almacenistaHelperXML.CrearAlmacenista(nombreCompleto, passwordEncriptada);
                        break;
                    case 2:
                        int Count = 0;
                        while (auth == false && Count < 3)
                        {
                            Console.Write("Nombre completo del Almacenista: ");
                            string nombre = Console.ReadLine() ?? string.Empty;
                            Console.Write("Password: ");
                            string password = Console.ReadLine() ?? string.Empty;
                            auth = almacenistaHelperJSON.IniciarSesion(nombre, password);
                            if (auth == false)
                            {
                                Console.WriteLine("Nombre o password incorrectos.");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"Bienvenido {nombre}");
                            }
                            Count++;
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opción no válida. Introduce un número del 1 al 5.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opción no válida. Introduce un número del 1 al 5.");
            }
        }
        while (true)
        {
            Console.WriteLine("Selecciona una opción:");
            Console.WriteLine("0. Generar claves de encriptación");
            Console.WriteLine("1. Crear Profesor");
            Console.WriteLine("2. Leer Profesores");
            Console.WriteLine("3. Actualizar Profesor");
            Console.WriteLine("4. Eliminar Profesor");
            Console.WriteLine("5. Cambiar Contraseña (Almacenista/Profesor)");
            Console.WriteLine("6. Reportes");
            Console.WriteLine("7. Salir");
            Console.Write("Opción: ");

            int opcion;
            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {
                    case 0:
                        encriptacionHelper.CrearClavesEncriptacion();
                        break;
                    case 1:
                        Console.Clear();
                        Console.Write("Nombre completo del profesor: ");
                        nombreCompleto = Console.ReadLine() ?? string.Empty;
                        nombreCompleto = VariableHelper.StringNotEmpty(nombreCompleto);
                        Console.Write("Nomina: ");
                        nominaEncriptada = Console.ReadLine() ?? string.Empty;
                        nominaEncriptada = VariableHelper.StringNotEmpty(nominaEncriptada);
                        Console.Write("Password: ");
                        passwordEncriptada = Console.ReadLine() ?? string.Empty;
                        passwordEncriptada = VariableHelper.StringNotEmpty(passwordEncriptada);
                        Console.Write("Materias: ");
                        materiasInput = Console.ReadLine() ?? string.Empty;
                        materiasQueImparte = materiasInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Console.Write("Division: ");
                        division = Console.ReadLine() ?? string.Empty;
                        division = VariableHelper.StringNotEmpty(division);
                        profesoresHelperJSON.CrearProfesor(nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
                        profesoresHelperXML.CrearProfesor(nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
                        Console.Clear();
                        break;
                    case 2:
                        //profesoresHelperJSON.LeerProfesores();
                        Console.Clear();
                        profesoresHelperXML.LeerProfesores();
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("ID del Profesor: ");
                        id = int.Parse(Console.ReadLine() ?? string.Empty);
                        Console.WriteLine("Nombre completo del Profesor: ");
                        nombreCompleto = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Nomina (NOMIX..X): ");
                        nominaEncriptada = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Password: ");
                        passwordEncriptada = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Materias: ");
                        materiasInput = Console.ReadLine() ?? string.Empty;
                        materiasQueImparte = materiasInput.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        Console.WriteLine("Division: ");
                        division = Console.ReadLine() ?? string.Empty;
                        profesoresHelperJSON.EditarProfesor(id, nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
                        profesoresHelperXML.EditarProfesor(id, nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
                        Console.Clear();
                        break;
                    case 4:
                        Console.Write("ID del Profesor: ");
                        id = int.Parse(Console.ReadLine() ?? string.Empty);
                        profesoresHelperJSON.EliminarProfesor(id);
                        profesoresHelperXML.EliminarProfesor(id);
                        Console.Clear();
                        break;
                    case 5:
                        opcion2 = string.Empty;
                        while (opcion2 != "1" && opcion2 != "2")
                        {
                            Console.Clear();
                            Console.WriteLine("Cambiar Contraseña");
                            Console.WriteLine("Selecciona una opción:");
                            Console.WriteLine("1. Cambiar contraseña de un profesor");
                            Console.WriteLine("2. Cambiar contraseña de un almacenista");
                            Console.Write("Opción: ");
                            opcion2 = Console.ReadLine() ?? string.Empty;
                            if (opcion2 != "1" && opcion2 != "2")
                            {
                                Console.WriteLine("Opción no válida. Introduce un número del 1 al 2.");
                            }
                        }
                        if (opcion2.Equals("1"))
                        {
                            Console.Write("ID del Profesor: ");
                            id = int.Parse(Console.ReadLine() ?? string.Empty);
                            Console.Write("Password: ");
                            passwordEncriptada = Console.ReadLine() ?? string.Empty;
                            passwordEncriptada = VariableHelper.StringNotEmpty(passwordEncriptada);
                            profesoresHelperJSON.EditarPassProfesor(id, passwordEncriptada);
                            profesoresHelperXML.EditarPassProfesor(id, passwordEncriptada);
                        }
                        else
                        {
                            Console.Write("ID del Almacenista: ");
                            id = int.Parse(Console.ReadLine() ?? string.Empty);
                            Console.Write("Password: ");
                            passwordEncriptada = Console.ReadLine() ?? string.Empty;
                            passwordEncriptada = VariableHelper.StringNotEmpty(passwordEncriptada);
                            almacenistaHelperJSON.EditarPassAlmacenista(id, passwordEncriptada);
                            almacenistaHelperXML.EditarPassAlmacenista(id, passwordEncriptada);
                        }
                        Console.Clear();
                        break;
                    case 6:
                        opcion2 = string.Empty;
                        while (opcion2 != "1" && opcion2 != "2")
                        {
                            Console.Clear();
                            Console.WriteLine("Selecciona una opción para tus reportes:");
                            Console.WriteLine("1. Reporte Almacenistas");
                            Console.WriteLine("2. Reporte Profesores");
                            Console.Write("Opción: ");
                            opcion2 = Console.ReadLine() ?? string.Empty;
                            if (opcion2 != "1" && opcion2 != "2")
                            {
                                Console.WriteLine("Opción no válida. Introduce un número del 1 al 2.");
                            }
                        }
                        if (opcion2.Equals("1"))
                        {
                            ReportesAlmacenistas();
                        }
                        else
                        {
                            ReportesProfesores();
                        }
                        Console.Clear();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Introduce un número del 1 al 5.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opción no válida. Introduce un número del 1 al 5.");
            }
        }
    }

    private static void ReportesAlmacenistas()
    {
        Console.Clear();
        Console.WriteLine("Reportes Almacenistas");
        Console.WriteLine("Selecciona una opción:");
        Console.WriteLine("1. Reporte de Almacenistas por Nombre");
        Console.Write("Opción: ");
        int opcion = int.Parse(Console.ReadLine() ?? string.Empty);
        switch (opcion)
        {
            case 1:
                break;
            default:
                Console.WriteLine("Opción no válida. Introduce un número del 1 al 2.");
                break;
        }

    }
    private static void ReportesProfesores()
    {
        var generadorJSON = new ReporteProfesorJSON();
        var generadorXML = new ReporteProfesorXML();
        Console.Clear();
        Console.WriteLine("Reportes Profesores");
        Console.WriteLine("Selecciona una opción:");
        Console.WriteLine("1. Reporte de Profesores por Nombre Ascendente");
        Console.WriteLine("2. Reporte de Profesores por Nomina Ascendente");
        Console.WriteLine("3. Reporte de Profesores por Division Ascendente");
        Console.WriteLine("4. Reporte de Profesores por Materias Ascendente");
        Console.WriteLine("5. Reporte de Profesores por ID Ascendente");
        Console.Write("Opción: ");
        int opcion = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Clear();
        switch (opcion)
        {
            case 1:
                generadorJSON.GenerarReportePorNombreAscendente();
                generadorXML.GenerarReportePorNombreAscendente();
                Console.Clear();
                break;
            case 2:
                generadorJSON.GenerarReportePorNominaAscendente();
                generadorXML.GenerarReportePorNominaAscendente();
                Console.Clear();
                break;
            case 3:
                generadorJSON.GenerarReportePorDivisionAscendente();
                generadorXML.GenerarReportePorDivisionAscendente();
                Console.Clear();
                break;
            case 4:
                generadorJSON.GenerarReportePorMateriasAscendente();
                generadorXML.GenerarReportePorMateriasAscendente();
                Console.Clear();
                break;
            case 5:
                generadorJSON.GenerarReportePorIDAscendente();
                generadorXML.GenerarReportePorIDAscendente();
                Console.Clear();
                break;
            default:
                Console.WriteLine("Opción no válida. Introduce un número del 1 al 2.");
                break;
        }
    }
}
