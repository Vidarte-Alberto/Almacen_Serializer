using System.Security.Cryptography;
public class EncriptacionHelper
{
    public static byte[] GenerateRandomKey(int length)
    {
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] key = new byte[length];
            rng.GetBytes(key);
            return key;
        }
    }
    public void CrearClavesEncriptacion()
    {
        string claveFilePath = "clave.bin"; // Ruta del archivo para la clave
        string ivFilePath = "iv.bin"; // Ruta del archivo para el vector de inicialización

        // Genera una clave aleatoria y guárdala en el archivo
        byte[] clave = GenerateRandomKey(16); // Por ejemplo, una clave de 16 bytes para AES-128
        File.WriteAllBytes(claveFilePath, clave);

        // Genera un vector de inicialización aleatorio y guárdalo en el archivo
        byte[] iv = GenerateRandomKey(16); // Por ejemplo, un IV de 16 bytes para AES-128
        File.WriteAllBytes(ivFilePath, iv);

        Console.WriteLine("Clave y IV generados y almacenados en archivos.");
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
}