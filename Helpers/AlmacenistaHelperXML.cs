using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class AlmacenistaHelperXML
{
    private List<Almacenista> almacenistas;
    private const string fileName = "almacenistas.xml";

    public AlmacenistaHelperXML()
    {
        // Inicializa la lista de almacenistas desde el archivo XML si existe, o crea una nueva lista
        if (File.Exists(fileName))
        {
            using (var streamReader = new StreamReader(fileName))
            {
                var serializer = new XmlSerializer(typeof(List<Almacenista>));
                almacenistas = (List<Almacenista>)serializer.Deserialize(streamReader) ?? new List<Almacenista>();
            }
        }
        else
        {
            almacenistas = new List<Almacenista>();
        }
    }

    public void GuardarCambios()
    {
        // Guarda la lista de almacenistas en el archivo XML
        using (var streamWriter = new StreamWriter(fileName))
        {
            var serializer = new XmlSerializer(typeof(List<Almacenista>));
            serializer.Serialize(streamWriter, almacenistas);
        }
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
