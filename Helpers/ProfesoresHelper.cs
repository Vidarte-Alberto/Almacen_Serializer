using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
class ProfesoresHelper
{
    public void CrearProfesor(string ProfesorFilePath)
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
        List<Profesor> profesores = LeerProfesoresDesdeJson(ProfesorFilePath);

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
        GuardarProfesoresEnJson(profesores, ProfesorFilePath);

        Console.WriteLine("Profesor creado exitosamente con ID: " + nuevoId);
    }


    public void LeerProfesores(string ProfesorFilePath)
    {
        List<Profesor> profesores = LeerProfesoresDesdeJson(ProfesorFilePath);

        if (profesores.Any())
        {
            Console.WriteLine("Listado de Profesores:");
            foreach (var profesor in profesores)
            {
                string pass = Profesor.Desencriptar(profesor.PasswordEncriptada);
                string nomina = Profesor.Desencriptar(profesor.NominaEncriptada);
                Console.WriteLine($"ID: {profesor.ProfesorId}, Nomina: {nomina}, Password: {pass} , Nombre: {profesor.NombreCompleto}, Materias: {string.Join(", ", profesor.MateriasQueImparte)}, Division: {profesor.Division}");
            }
        }
        else
        {
            Console.WriteLine("No hay profesores registrados.");
        }
    }

    public void ActualizarProfesor(string ProfesorFilePath)
    {
        Console.Write("ID del profesor a actualizar: ");
        if (int.TryParse(Console.ReadLine(), out int idProfesor))
        {
            List<Profesor> profesores = LeerProfesoresDesdeJson(ProfesorFilePath);
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
                GuardarProfesoresEnJson(profesores, ProfesorFilePath);

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

    public void EliminarProfesor(string ProfesorFilePath)
    {
        Console.Write("ID del profesor a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int idProfesor))
        {
            List<Profesor> profesores = LeerProfesoresDesdeJson(ProfesorFilePath);
            Profesor? profesorExistente = profesores.FirstOrDefault(p => p.ProfesorId == idProfesor);

            if (profesorExistente != null)
            {
                // Elimina el profesor de la lista
                profesores.Remove(profesorExistente);

                // Guarda la lista actualizada en el archivo JSON
                GuardarProfesoresEnJson(profesores, ProfesorFilePath);

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

    public void CambiarPassword(string ProfesorFilePath)
    {
        Console.Write("ID del profesor a cambiar password: ");
        if (int.TryParse(Console.ReadLine(), out int idProfesor))
        {
            List<Profesor> profesores = LeerProfesoresDesdeJson(ProfesorFilePath);
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
                GuardarProfesoresEnJson(profesores, ProfesorFilePath);

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


    public List<Profesor> LeerProfesoresDesdeJson(string ProfesorFilePath)
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

    private void GuardarProfesoresEnJson(List<Profesor> profesores, string ProfesorFilePath)
    {
        string json = JsonConvert.SerializeObject(profesores);
        File.WriteAllText(ProfesorFilePath, json);
    }
}