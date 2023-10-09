using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ProfesorHelperJSON
{
    private List<Profesor> profesores;
    private string fileName = "profesores.json";

    public ProfesorHelperJSON(string filePath)
    {
        fileName = filePath;
        // Inicializa la lista de profesores desde el archivo JSON si existe, o crea una nueva lista
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            profesores = JsonConvert.DeserializeObject<List<Profesor>>(json) ?? new List<Profesor>();
        }
        else
        {
            profesores = new List<Profesor>();
        }
    }

    public ProfesorHelperJSON()
    {
        // Inicializa la lista de profesores desde el archivo JSON si existe, o crea una nueva lista
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            profesores = JsonConvert.DeserializeObject<List<Profesor>>(json) ?? new List<Profesor>();
        }
        else
        {
            profesores = new List<Profesor>();
        }
    }

    public void GuardarCambios()
    {
        // Guarda la lista de profesores en el archivo JSON
        string json = JsonConvert.SerializeObject(profesores);
        File.WriteAllText(fileName, json);
    }

    public void CrearProfesor(string nombreCompleto, string nominaEncriptada, string passwordEncriptada, string[] materiasQueImparte, string division)
    {
        // Encontrar el ID más alto en la lista actual de profesores
        int nuevoId = profesores.Any() ? profesores.Max(p => p.ProfesorId) + 1 : 1;
        // Crea un nuevo profesor
        var profesor = new Profesor(nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division)
        {
            ProfesorId = nuevoId
        };
        profesores.Add(profesor);
        GuardarCambios();
    }

    public void EditarProfesor(int id, string? nombreCompleto = null, string? nominaEncriptada = null, string? passwordEncriptada = null, string[]? materiasQueImparte = null, string? division = null)
    {
        // Edita un profesor por id
        var profesor = profesores.Find(a => a.ProfesorId == id);
        if (profesor != null)
        {
            if (nombreCompleto != null && nombreCompleto != string.Empty)
                profesor.NombreCompleto = nombreCompleto;

            if (nominaEncriptada != null && nominaEncriptada != string.Empty)
                profesor.NominaEncriptada = nominaEncriptada;

            if (passwordEncriptada != null && passwordEncriptada != string.Empty)
                profesor.PasswordEncriptada = passwordEncriptada;

            if (materiasQueImparte != null && materiasQueImparte.Length > 0)
                profesor.MateriasQueImparte = materiasQueImparte;

            if (division != null && division != string.Empty)
                profesor.Division = division;

            GuardarCambios();
        }
    }


    public void EditarPassProfesor(int id, string newPassword)
    {
        // Edita un profesor por nombre
        var profesor = profesores.Find(a => a.ProfesorId == id);
        if (profesor != null)
        {
            profesor.PasswordEncriptada = EncriptacionHelper.Encriptar(newPassword);
            GuardarCambios();
        }
    }

    public void EliminarProfesor(int id)
    {
        // Elimina un profesor por id
        var profesor = profesores.Find(a => a.ProfesorId == id);
        if (profesor != null)
        {
            profesores.Remove(profesor);
            GuardarCambios();
        }
    }

    public List<Profesor> ListarProfesores()
    {
        return profesores;
    }

    public void LeerProfesores()
    {
        if (profesores.Any())
        {
            Console.WriteLine("Listado de Profesores:");
            foreach (var profesor in profesores)
            {
                string pass = EncriptacionHelper.Desencriptar(profesor.PasswordEncriptada);
                string nomina = EncriptacionHelper.Desencriptar(profesor.NominaEncriptada);
                Console.WriteLine($"ID: {profesor.ProfesorId}, Nomina: {nomina}, Password: {pass} , Nombre: {profesor.NombreCompleto}, Materias: {string.Join(", ", profesor.MateriasQueImparte)}, Division: {profesor.Division}");
            }
        }
        else
        {
            Console.WriteLine("No hay profesores registrados.");
        }
    }

    public Profesor BuscarAlmacenista(string nombreCompleto)
    {
        // Busca un profesor por nombre
        return profesores.Find(a => a.NombreCompleto == nombreCompleto) ?? new Profesor();
    }

}
