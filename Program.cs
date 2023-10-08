using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class Program
{
    private const string ProfesorFilePath = "profesores.json";

    public static void Main()
    {
        // Ejemplo de CRUD para el modelo Profesor con JSON
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
                        CrearClavesEncriptacion();
                        break;
                    case 1:
                        CrearProfesor();
                        break;
                    case 2:
                        LeerProfesores();
                        break;
                    case 3:
                        ActualizarProfesor();
                        break;
                    case 4:
                        EliminarProfesor();
                        break;
                    case 5:
                        CambiarPassword();
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

    private static void CrearClavesEncriptacion()
    {
        string claveFilePath = "clave.bin"; // Ruta del archivo para la clave
        string ivFilePath = "iv.bin"; // Ruta del archivo para el vector de inicialización

        // Genera una clave aleatoria y guárdala en el archivo
        byte[] clave = Profesor.GenerateRandomKey(16); // Por ejemplo, una clave de 16 bytes para AES-128
        File.WriteAllBytes(claveFilePath, clave);

        // Genera un vector de inicialización aleatorio y guárdalo en el archivo
        byte[] iv = Profesor.GenerateRandomKey(16); // Por ejemplo, un IV de 16 bytes para AES-128
        File.WriteAllBytes(ivFilePath, iv);

        Console.WriteLine("Clave y IV generados y almacenados en archivos.");
    }

    private static void CrearProfesor()
    {
        string nombreCompleto = string.Empty;
        while (string.IsNullOrWhiteSpace(nombreCompleto))
        {
            Console.Write("Nombre completo del profesor: ");
            nombreCompleto = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
            }
        }
        string nominEncriptada = string.Empty;
        while (string.IsNullOrWhiteSpace(nominEncriptada))
        {
            Console.Write("Nomina: ");
            nominEncriptada = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nominEncriptada))
            {
                Console.WriteLine("La nomina no puede estar vacía.");
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
        Console.Write("Materias: ");
        string materiasInput = Console.ReadLine() ?? string.Empty;
        string[] materiasQueImparte = materiasInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        string division = string.Empty;
        while (string.IsNullOrWhiteSpace(division))
        {
            Console.Write("Division: ");
            division = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(division))
            {
                Console.WriteLine("La division no puede estar vacía.");
            }
        }

        // Leer la lista de profesores desde el archivo JSON
        List<Profesor> profesores = LeerProfesoresDesdeJson();

        // Encontrar el ID más alto en la lista actual de profesores
        int nuevoId = profesores.Any() ? profesores.Max(p => p.ProfesorId) + 1 : 1;

        // Crear una instancia de Profesor con el nuevo ID
        Profesor nuevoProfesor = new Profesor(nombreCompleto, nominEncriptada, passwordEncriptada, materiasQueImparte, division)
        {
            ProfesorId = nuevoId
        };

        // Agregar el nuevo profesor a la lista
        profesores.Add(nuevoProfesor);

        // Guardar la lista actualizada en el archivo JSON
        GuardarProfesoresEnJson(profesores);

        Console.WriteLine("Profesor creado exitosamente con ID: " + nuevoId);
    }


    private static void LeerProfesores()
    {
        List<Profesor> profesores = LeerProfesoresDesdeJson();

        if (profesores.Any())
        {
            Console.WriteLine("Listado de Profesores:");
            foreach (var profesor in profesores)
            {
                string pass = Profesor.Desencriptar(profesor.PasswordEncriptada);
                string nomina = Profesor.Desencriptar(profesor.NominaEncriptada);
                Console.WriteLine($"ID: {profesor.ProfesorId}, Nomina: {nomina}, Password: {pass} , Nombre: {profesor.NombreCompleto}, Materias: {string.Join(", ", profesor.MateriasQueImparte)}, Division: {profesor.Division}");


                // Puedes mostrar otras propiedades aquí
            }
        }
        else
        {
            Console.WriteLine("No hay profesores registrados.");
        }
    }

    private static void ActualizarProfesor()
    {
        Console.Write("ID del profesor a actualizar: ");
        if (int.TryParse(Console.ReadLine(), out int idProfesor))
        {
            List<Profesor> profesores = LeerProfesoresDesdeJson();
            Profesor? profesorExistente = profesores.FirstOrDefault(p => p.ProfesorId == idProfesor);

            if (profesorExistente != null)
            {
                Console.Write("Nuevo nombre completo del profesor: ");
                string nuevoNombreCompleto = string.Empty;
                while (string.IsNullOrWhiteSpace(nuevoNombreCompleto))
                {
                    nuevoNombreCompleto = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(nuevoNombreCompleto))
                    {
                        Console.WriteLine("El nombre no puede estar vacío.");
                    }
                }

                // Puedes continuar solicitando otros datos que desees actualizar.

                // Actualiza los datos del profesor existente
                profesorExistente.NombreCompleto = nuevoNombreCompleto;
                // Actualiza otras propiedades si es necesario.

                // Guarda la lista actualizada en el archivo JSON
                GuardarProfesoresEnJson(profesores);

                Console.WriteLine("Profesor actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("Profesor no encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID de profesor no válido.");
        }
    }

    private static void EliminarProfesor()
    {
        Console.Write("ID del profesor a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int idProfesor))
        {
            List<Profesor> profesores = LeerProfesoresDesdeJson();
            Profesor? profesorExistente = profesores.FirstOrDefault(p => p.ProfesorId == idProfesor);

            if (profesorExistente != null)
            {
                // Elimina el profesor de la lista
                profesores.Remove(profesorExistente);

                // Guarda la lista actualizada en el archivo JSON
                GuardarProfesoresEnJson(profesores);

                Console.WriteLine("Profesor eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Profesor no encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID de profesor no válido.");
        }
    }

    private static void CambiarPassword()
    {
        Console.Write("ID del profesor a cambiar password: ");
        if (int.TryParse(Console.ReadLine(), out int idProfesor))
        {
            List<Profesor> profesores = LeerProfesoresDesdeJson();
            Profesor? profesorExistente = profesores.FirstOrDefault(p => p.ProfesorId == idProfesor);

            if (profesorExistente != null)
            {
                Console.Write("Nuevo password: ");
                string nuevoPassword = string.Empty;
                while (string.IsNullOrWhiteSpace(nuevoPassword))
                {
                    nuevoPassword = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(nuevoPassword))
                    {
                        Console.WriteLine("El password no puede estar vacío.");
                    }
                }

                // Actualiza los datos del profesor existente
                profesorExistente.PasswordEncriptada = Profesor.Encriptar(nuevoPassword);

                // Guarda la lista actualizada en el archivo JSON
                GuardarProfesoresEnJson(profesores);

                Console.WriteLine("Password actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("Profesor no encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID de profesor no válido.");
        }
    }


    private static List<Profesor> LeerProfesoresDesdeJson()
    {
        if (File.Exists(ProfesorFilePath))
        {
            string json = File.ReadAllText(ProfesorFilePath);
            return JsonConvert.DeserializeObject<List<Profesor>>(json) ?? new List<Profesor>();
        }
        else
        {
            return new List<Profesor>();
        }
    }

    private static void GuardarProfesoresEnJson(List<Profesor> profesores)
    {
        string json = JsonConvert.SerializeObject(profesores);
        File.WriteAllText(ProfesorFilePath, json);
    }
}
