using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class Profesor
{
    public int ProfesorId { get; set; }
    public string NombreCompleto { get; set; }
    public string NominaEncriptada { get; set; }
    public string PasswordEncriptada { get; set; }
    public string[] MateriasQueImparte { get; set; }
    public string Division { get; set; }
    public List<ClaseProfesor> ClasesProfesor { get; set; }

    public Profesor()
    {
        ClasesProfesor = new List<ClaseProfesor>();
    }


    // Constructor para la clase Profesor
    public Profesor(string nombreCompleto, string nomina, string password, string[] materias, string division)
    {
        NombreCompleto = nombreCompleto;
        NominaEncriptada = Encriptar(nomina);
        PasswordEncriptada = Encriptar(password);
        MateriasQueImparte = materias;
        Division = division;
    }
    // Método para encriptar una cadena
    public static string Encriptar(string texto)
    {
        using (Aes aesAlg = Aes.Create())
        {
            string claveFilePath = "./clave.bin";
            string ivFilePath = "./iv.bin";
            aesAlg.Key = File.ReadAllBytes(claveFilePath);
            aesAlg.IV = File.ReadAllBytes(ivFilePath);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(texto);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // Método para desencriptar una cadena
    public static string Desencriptar(string textoEncriptado)
    {
        using (Aes aesAlg = Aes.Create())
        {
            string claveFilePath = "./clave.bin";
            string ivFilePath = "./iv.bin";
            aesAlg.Key = File.ReadAllBytes(claveFilePath);
            aesAlg.IV = File.ReadAllBytes(ivFilePath);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(textoEncriptado)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public static byte[] GenerateRandomKey(int length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] key = new byte[length];
            rng.GetBytes(key);
            return key;
        }
    }
}
