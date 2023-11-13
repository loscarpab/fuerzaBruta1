using System.IO;
using System.Security.Cryptography;
using System.Text;

String archivoGrande = "2151220-passwords.txt";
String archivoPequeño = "prueba-passwords.txt";
String ruta = "..\\..\\..\\"+archivoPequeño;



try
{
    int cont_line = 0;
    Encoding codificacion = Encoding.UTF8;
    string[] lines = File.ReadAllLines(ruta, codificacion);
    cont_line = lines.Length;
    Random lineaElegida = new Random();
    int numLinea = lineaElegida.Next(cont_line);
    byte[] encrypt;
    String decrypt;
    using (Aes aesAlg = Aes.Create())
    {
        encrypt = EncryptStringToBytes(lines[numLinea], aesAlg.Key, aesAlg.IV);
        decrypt = DecryptStringFromBytes(encrypt, aesAlg.Key, aesAlg.IV);
    }
        

    Console.WriteLine("El archivo tiene: {0} líneas", cont_line);
    Console.WriteLine(numLinea);
    Console.WriteLine("Texto Encriptado: " + codificacion.GetString(encrypt));
    Console.WriteLine("Texto Desencriptado: " + decrypt);
    Console.ReadLine();


}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    Console.WriteLine("Executing finally block.");
}
static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
{
    byte[] encrypted;

    // Create an Aes object with the specified key and IV.
    using (Aes aes = Aes.Create())
    {
        aes.Key = key;
        aes.IV = iv;

        // Create a new MemoryStream object to contain the encrypted bytes.
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Create a CryptoStream object to perform the encryption.
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                // Encrypt the plaintext.
                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                encrypted = memoryStream.ToArray();
            }
        }
    }

    return encrypted;
}
static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
{
    string decrypted;

    // Create an Aes object with the specified key and IV.
    using (Aes aes = Aes.Create())
    {
        aes.Key = key;
        aes.IV = iv;

        // Create a new MemoryStream object to contain the decrypted bytes.
        using (MemoryStream memoryStream = new MemoryStream(cipherText))
        {
            // Create a CryptoStream object to perform the decryption.
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                // Decrypt the ciphertext.
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                {
                    decrypted = streamReader.ReadToEnd();
                }
            }
        }
    }

    return decrypted;
}