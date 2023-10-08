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
        var profesoresHelper = new ProfesoresHelper();
        var encriptacionHelper = new EncriptacionHelper();
        var almacenistaHelper = new AlmacenistaHelper();
        bool auth = false;
        while (auth == false)
        {
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
                        string nombreCompleto = string.Empty;
                        while (string.IsNullOrWhiteSpace(nombreCompleto))
                        {
                            Console.Write("Nombre completo del Almacenista: ");
                            nombreCompleto = Console.ReadLine() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(nombreCompleto))
                            {
                                Console.WriteLine("El nombre no puede estar vacío.");
                            }
                        }
                        string passwordEncriptada = string.Empty;
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
                        profesoresHelper.CrearProfesor(ProfesorFilePath);
                        break;
                    case 2:
                        profesoresHelper.LeerProfesores(ProfesorFilePath);
                        break;
                    case 3:
                        profesoresHelper.ActualizarProfesor(ProfesorFilePath);
                        break;
                    case 4:
                        profesoresHelper.EliminarProfesor(ProfesorFilePath);
                        break;
                    case 5:
                        profesoresHelper.CambiarPassword(ProfesorFilePath);
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
