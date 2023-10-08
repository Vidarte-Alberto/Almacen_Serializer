using System;
using System.Security.Cryptography;
using System.Text;

public class Almacenista
{
    public string NombreCompleto { get; set; }
    public string PasswordEncriptada { get; set; }

    // Constructor para crear una instancia de Almacenista
    public Almacenista(string nombreCompleto, string password)
    {
        NombreCompleto = nombreCompleto;
        PasswordEncriptada = EncriptarPassword(password);
    }

    // Método para encriptar la contraseña
    private string EncriptarPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }

    // Método para verificar si una contraseña coincide con la contraseña encriptada
    public bool VerificarPassword(string password)
    {
        string passwordEncriptadaIngresada = EncriptarPassword(password);
        return passwordEncriptadaIngresada == PasswordEncriptada;
    }
}
