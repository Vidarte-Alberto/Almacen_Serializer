using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class AlmacenistaHelperJSON
{
    private List<Almacenista> almacenistas;
    private const string fileName = "almacenistas.json";

    public AlmacenistaHelperJSON()
    {
        // Inicializa la lista de almacenistas desde el archivo JSON si existe, o crea una nueva lista
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            almacenistas = JsonConvert.DeserializeObject<List<Almacenista>>(json) ?? new List<Almacenista>();
        }
        else
        {
            almacenistas = new List<Almacenista>();
        }
    }

    public void GuardarCambios()
    {
        // Guarda la lista de almacenistas en el archivo JSON
        string json = JsonConvert.SerializeObject(almacenistas);
        File.WriteAllText(fileName, json);
    }

    public void CrearAlmacenista(string nombreCompleto, string password)
    {
        // Encontrar el ID más alto en la lista actual de profesores
        int nuevoId = almacenistas.Any() ? almacenistas.Max(p => p.AlmacenistaId) + 1 : 1;
        // Crea un nuevo almacenista
        var almacenista = new Almacenista(nombreCompleto, password)
        {
            AlmacenistaId = nuevoId
        };
        almacenistas.Add(almacenista);
        GuardarCambios();
    }

    public void EditarAlmacenista(string nombreCompleto, string newPassword)
    {
        // Edita un almacenista por nombre
        var almacenista = almacenistas.Find(a => a.NombreCompleto == nombreCompleto);
        if (almacenista != null)
        {
            almacenista.PasswordEncriptada = EncriptacionHelper.Encriptar(newPassword);
            GuardarCambios();
        }
    }

    public void EditarPassAlmacenista(int id, string newPassword)
    {
        // Edita contraseña de un almacenista por id
        var almacenista = almacenistas.Find(a => a.AlmacenistaId == id);
        if (almacenista != null)
        {
            almacenista.PasswordEncriptada = EncriptacionHelper.Encriptar(newPassword);
            GuardarCambios();
        }
    }

    public void EliminarAlmacenista(string nombreCompleto)
    {
        // Elimina un almacenista por nombre
        var almacenista = almacenistas.Find(a => a.NombreCompleto == nombreCompleto);
        if (almacenista != null)
        {
            almacenistas.Remove(almacenista);
            GuardarCambios();
        }
    }

    public List<Almacenista> ListarAlmacenistas()
    {
        return almacenistas;
    }

    public Almacenista BuscarAlmacenista(string nombreCompleto)
    {
        // Busca un almacenista por nombre
        return almacenistas.Find(a => a.NombreCompleto == nombreCompleto) ?? new Almacenista("", "");
    }

    public bool IniciarSesion(string nombreCompleto, string password)
    {
        // Buscar un almacenista por nombre
        var almacenista = almacenistas.Find(a => a.NombreCompleto == nombreCompleto);

        if (almacenista != null)
        {
            // Verificar si la contraseña proporcionada coincide con la contraseña encriptada almacenada
            string contraseñaEncriptada = EncriptacionHelper.Encriptar(password);
            if (almacenista.PasswordEncriptada == contraseñaEncriptada)
            {
                // Las credenciales son válidas, el usuario ha iniciado sesión correctamente
                return true;
            }
        }

        // Las credenciales son inválidas o el usuario no existe
        return false;
    }
}
