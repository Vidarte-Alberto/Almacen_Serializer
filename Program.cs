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

        var profesoresHelper = new ProfesorHelper();
        var encriptacionHelper = new EncriptacionHelper();
        var almacenistaHelper = new AlmacenistaHelper();
        bool auth = false;
        while (auth == false)
        {
            Console.Clear();
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
                        nombreCompleto = string.Empty;
                        while (string.IsNullOrWhiteSpace(nombreCompleto))
                        {
                            Console.Write("Nombre completo del Almacenista: ");
                            nombreCompleto = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(nombreCompleto))
                            {
                                Console.WriteLine("El nombre no puede estar vacío.");
                            }
                        }
                        passwordEncriptada = string.Empty;
                        while (string.IsNullOrWhiteSpace(passwordEncriptada))
                        {
                            Console.Write("Password: ");
                            passwordEncriptada = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(passwordEncriptada))
                            {
                                Console.WriteLine("El password no puede estar vacío.");
                            }
                        }
                        almacenistaHelper.CrearAlmacenista(nombreCompleto, passwordEncriptada);
                        break;
                    case 2:
                        int Count = 0;
                        while (auth == false && Count < 3)
                        {
                            Console.Write("Nombre completo del Almacenista: ");
                            string nombre = Console.ReadLine() ?? string.Empty;
                            Console.Write("Password: ");
                            string password = Console.ReadLine() ?? string.Empty;
                            auth = almacenistaHelper.IniciarSesion(nombre, password);
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
            Console.WriteLine("5. Cambiar Contraseña");
            Console.WriteLine("6. Salir");
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
                        nombreCompleto = string.Empty;
                        while (string.IsNullOrWhiteSpace(nombreCompleto))
                        {
                            Console.Write("Nombre completo del profesor: ");
                            nombreCompleto = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(nombreCompleto))
                            {
                                Console.WriteLine("El nombre no puede estar vacío.");
                            }
                        }
                        nominaEncriptada = string.Empty;
                        while (string.IsNullOrWhiteSpace(nominaEncriptada))
                        {
                            Console.Write("Nomina: ");
                            nominaEncriptada = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(nominaEncriptada))
                            {
                                Console.WriteLine("La nomina no puede estar vacía.");
                            }
                        }
                        passwordEncriptada = string.Empty;
                        while (string.IsNullOrWhiteSpace(passwordEncriptada))
                        {
                            Console.Write("Password: ");
                            passwordEncriptada = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(passwordEncriptada))
                            {
                                Console.WriteLine("El password no puede estar vacío.");
                            }
                        }
                        Console.Write("Materias: ");
                        materiasInput = Console.ReadLine() ?? string.Empty;
                        materiasQueImparte = materiasInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        division = string.Empty;
                        while (string.IsNullOrWhiteSpace(division))
                        {
                            Console.Write("Division: ");
                            division = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(division))
                            {
                                Console.WriteLine("La division no puede estar vacía.");
                            }
                        }
                        profesoresHelper.CrearProfesor(nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
                        break;
                    case 2:
                        profesoresHelper.LeerProfesores(ProfesorFilePath);
                        break;
                    case 3:
                        Console.Write("ID del Profesor: ");
                        id = int.Parse(Console.ReadLine() ?? string.Empty);
                        Console.WriteLine("Nombre completo del Profesor: ");
                        nombreCompleto = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Nomina: ");
                        nominaEncriptada = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Password: ");
                        passwordEncriptada = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Materias: ");
                        materiasInput = Console.ReadLine() ?? string.Empty;
                        materiasQueImparte = materiasInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Console.WriteLine("Division: ");
                        division = Console.ReadLine() ?? string.Empty;
                        profesoresHelper.EditarProfesor(id, nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
                        break;
                    case 4:
                        Console.Write("ID del Profesor: ");
                        id = int.Parse(Console.ReadLine() ?? string.Empty);
                        profesoresHelper.EliminarProfesor(id);
                        break;
                    case 5:
                        Console.Write("ID del Profesor: ");
                        id = int.Parse(Console.ReadLine() ?? string.Empty);
                        Console.Write("Password: ");
                        passwordEncriptada = Console.ReadLine() ?? string.Empty;
                        profesoresHelper.EditarPassProfesor(id, passwordEncriptada);
                        break;
                    case 6:
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
}
