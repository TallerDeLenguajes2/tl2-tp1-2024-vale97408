using System.Text.Json;
public class AccesoDatosJSON : AccesoADatos
{
    private const string CarpetaJSON = "ArchivosJSON/";

    public bool Existe(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaJSON, nombreArchivo);
        return File.Exists(ruta);
    }

    // Lee los datos de la cadetería desde un archivo JSOn
    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaJSON, nombreArchivo);
        List<Cadete> cadetes = new List<Cadete>();

        if (!Existe(nombreArchivo))
        {
            Console.WriteLine($"El archivo {nombreArchivo} no existe.");
            return cadetes; // Devuelve una lista vacía si el archivo no existe
        }

         try
        {
            // Lee todo el contenido 
            string json = File.ReadAllText(ruta);
            // Deserializa el contenido JSON en una lista de objetos Cadete
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al leer el archivo JSON: {ex.Message}");
        }
        
        return cadetes;
    }

    public Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaJSON, nombreArchivo);
        Cadeteria cadeteria; 
        
        if (!File.Exists(ruta))
        {
            Console.WriteLine($"El archivo {nombreArchivo} no existe.");
            return null; // Devuelve una cadena vacía si el archivo no existe
        }

         try
        { 
            string json = File.ReadAllText(ruta);
             cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
            return cadeteria;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al leer el archivo JSON: {ex.Message}");
            return null;
        }
    }
}

