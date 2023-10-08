using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class Program
{
    public const string ProfesorFilePath = "profesores.json";

    public static void Main()
    {
        var profesoresHelper = new ProfesoresHelper();
        var encriptacionHelper = new EncriptacionHelper();
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
