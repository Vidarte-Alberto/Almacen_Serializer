using System;
using System.Security.Cryptography;
using System.Text;

public class Almacenista
{
    public int AlmacenistaId { get; set; }
    public string NombreCompleto { get; set; }
    public string PasswordEncriptada { get; set; }

    // Constructor para crear una instancia de Almacenista
    public Almacenista(string nombreCompleto, string password)
    {
        NombreCompleto = nombreCompleto;
        PasswordEncriptada = EncriptacionHelper.Encriptar(password);
    }
}
