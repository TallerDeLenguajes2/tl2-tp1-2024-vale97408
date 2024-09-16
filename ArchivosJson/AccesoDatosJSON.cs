using System.Text.Json;
public class AccesoDatosJSON : AccesoADatos
{
    private const string CarpetaJSON = "ArchivosJson/";

    private string archivoCadeteriaJson;

    private string archivoCadetesJson;

    public AccesoDatosJSON()
    {
        archivoCadeteriaJson = "cadeteria.json";
        archivoCadetesJson = "cadetes.json";
    }

    public bool Existe(string ruta)
    {
        return File.Exists(ruta);
    }

    // Lee los datos de la cadetería desde un archivo JSOn
    public List<Cadete> LeerCadetes( )
    {
        string ruta = Path.Combine(CarpetaJSON, archivoCadetesJson);
        string json;

        if (!Existe(ruta))
        {
            Console.WriteLine($"El archivo {archivoCadetesJson} no existe.");
            return null;
        }

        try
        {
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    json = strReader.ReadToEnd();

                }
            }

            var cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
            return cadetes;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al leer el archivo JSON- CADETE: {ex.Message}");
            return null;
        }

    }

    public Cadeteria LeerCadeteria( )
    {
        string ruta = Path.Combine(CarpetaJSON, archivoCadeteriaJson);
        string json;

        if (!Existe(ruta))
        {
            Console.WriteLine($"El archivo {archivoCadeteriaJson} no existe.");
            return null; // Devuelve una cadena vacía si el archivo no existe
        }

        try
        {
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                      json = strReader.ReadToEnd();
                }
            }

            var cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
            return cadeteria;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al leer el archivo JSON- CADETERIA: {ex.Message}");
            return null;
        }
    }
}

