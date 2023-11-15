using System.Security.Cryptography;
using System.Text;

String archivoGrande = "2151220-passwords.txt";
String archivoPequeño = "prueba-passwords.txt";
String ruta = "..\\..\\..\\" + archivoGrande;




try
{
    //Leer todo el archivo
    Encoding codificacion = Encoding.UTF8;
    string[] lines = File.ReadAllLines(ruta, codificacion);
    Console.WriteLine("El archivo tiene: {0} líneas", lines.Length);
    //escoger linea aleatoria para la contraseña a encontrar
    Random lineaElegida = new Random();
    int numLinea = lineaElegida.Next(lines.Length);
    //encriptar la contraseña aleatoria
    String encrypt;
    encrypt = Encrypt(lines[numLinea]);
    Console.WriteLine("Texto Encriptado: " + encrypt);


    for (int i = 0; i < lines.Length; i++)
    {
        var lineaEncrypt = Encrypt(lines[i]);
        if (lineaEncrypt == encrypt)
        {
            Console.WriteLine("La contraseña era " + lines[i]);
            Console.WriteLine("Se encontraba en la linea {0}", i+1);
            break;
        }
    }

}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    Console.WriteLine("Executing finally block.");
}

static string Encrypt(string contraseña)
{
    var resultado = "";
    var convert = SHA256.Create();

    var hashValue = convert.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
    foreach (byte b in hashValue)
    {
        resultado += $"{b:X2}";
    }

    return resultado;
}