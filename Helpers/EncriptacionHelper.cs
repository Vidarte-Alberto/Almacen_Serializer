class EncriptacionHelper
{
    public void CrearClavesEncriptacion()
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
}