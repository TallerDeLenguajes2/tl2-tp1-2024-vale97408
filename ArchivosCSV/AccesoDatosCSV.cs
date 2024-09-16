public class AccesoDatosCSV : AccesoADatos
{
    private const string CarpetaCSV = "ArchivosCSV/";

    
    private string archivoCadeteriaCSV;

    private string archivoCadetesCSV;

    public AccesoDatosCSV()
    {
        archivoCadeteriaCSV = "cadeteria.csv";
        archivoCadetesCSV = "cadetes.csv";
    }


    public bool Existe(string ruta)
    {
        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes()
    {
        string ruta = Path.Combine(CarpetaCSV, archivoCadetesCSV);
        List<Cadete> cadetes = new List<Cadete>();

        if (!Existe(ruta))
        {
            Console.WriteLine($"ACCESO DATOS CSV-El archivo {archivoCadetesCSV} no existe.");
            return cadetes; // Devuelve una lista vacía si el archivo no existe
        }

        try
        {
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    string linea;
                    while ((linea = strReader.ReadLine()) != null)
                    {
                        var datos = linea.Split(';');

                        if (datos.Length >= 4) // Verifica las  columnas para evitar errores
                        {
                            var cadete = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
                            cadetes.Add(cadete);
                        }
                        else
                        {
                            Console.WriteLine($"ACCESO DATOS CSV-Línea inválida en el archivo CADETES: {linea}");
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo {archivoCadetesCSV}: {ex.Message}");
        }

        return cadetes;
    }

    public Cadeteria LeerCadeteria()
    {
        string ruta = Path.Combine(CarpetaCSV, archivoCadeteriaCSV);
        string datos;

        if (!Existe(ruta))
        {
            Console.WriteLine($"El archivo {archivoCadeteriaCSV} no existe.");
            return null; // Devuelve una cadena vacía si el archivo no existe
        }

        try
        {
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    datos = strReader.ReadToEnd();

                }
            }
            string[] datosCadeteria = datos.Split(';');

            if (datosCadeteria.Length < 2)
            {
                Console.WriteLine($"El archivo {archivoCadeteriaCSV} no contiene información válida de la cadetería.");
                return null;
            }

            Cadeteria cadeteria = new Cadeteria(datosCadeteria[0], datosCadeteria[1]);

            return cadeteria;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo {archivoCadeteriaCSV}: {ex.Message}");
            return null;
        }

    }

}

