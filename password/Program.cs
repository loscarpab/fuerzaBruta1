using System.Security.Cryptography;
using System.Text;

bool encontrado = false;

try
{
    String archivoGrande = "2151220-passwords.txt";
    String archivoPequeño = "prueba-passwords.txt";
    String ruta = "..\\..\\..\\" + archivoGrande;
    int numHilos = 8;
    
    //Leer todo el archivo
    Encoding codificacion = Encoding.UTF8;
    string[] todasLasLineasDelArchivo = File.ReadAllLines(ruta, codificacion);
    Console.WriteLine("El archivo tiene: {0} líneas", todasLasLineasDelArchivo.Length);
    //escoger linea aleatoria para la contraseña a encontrar
    Random lineaElegida = new Random();
    int lineaAleatoriaEscogida = lineaElegida.Next(todasLasLineasDelArchivo.Length);
    //encriptar la contraseña aleatoria
    String contraseñaEncriptada;
    contraseñaEncriptada = Encrypt(todasLasLineasDelArchivo[lineaAleatoriaEscogida]);
    Console.WriteLine("Texto Encriptado: " + contraseñaEncriptada);
    

    for (int iterator = 0; iterator < numHilos; iterator++)
    {
        int divisionLineas = (todasLasLineasDelArchivo.Length-1)/(numHilos+1);
         
        Thread hilo = new Thread(() => { FuerzaBruta(todasLasLineasDelArchivo, contraseñaEncriptada, divisionLineas*(iterator), divisionLineas*(iterator+1)); });
        hilo.Start();
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
bool FuerzaBruta(string[] lineasArchivo, string contraseñaEncriptada, int minimoLineas, int maximoLineas)
{
    for (int i = minimoLineas; i <= maximoLineas; i++)
    {
        var lineaEncrypt = Encrypt(lineasArchivo[i]);
        if (encontrado)
        {
            return true;
        }
        if (lineaEncrypt == contraseñaEncriptada)
        {
            Console.WriteLine("La contraseña era " + lineasArchivo[i]);
            Console.WriteLine("Se encontraba en la linea {0}", i + 1);
            encontrado = true;
            return true;
        }
    }
    return false;
}