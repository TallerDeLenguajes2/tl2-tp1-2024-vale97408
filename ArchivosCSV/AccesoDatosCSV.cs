public class AccesoDatosCSV : AccesoADatos
{
    private const string CarpetaCSV = "ArchivosCSV/";

    public bool Existe(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaCSV, nombreArchivo);
          Console.WriteLine($"Ruta del archivo: {ruta}"); // Añadido para depuración
        return File.Exists(ruta);
    }

    // Lee los datos de la cadetería desde un archivo CSV (puede ser solo una línea o varias)
    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaCSV, nombreArchivo);
        List<Cadete> cadetes = new List<Cadete>();

        if (!Existe(nombreArchivo))
        {
            Console.WriteLine($"ACCESO DATOS CSV-El archivo {nombreArchivo} no existe.");
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
            Console.WriteLine($"Error al leer el archivo {nombreArchivo}: {ex.Message}");
        }

        return cadetes;
    }

    public Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaCSV, nombreArchivo);
        string datos;

        if (!Existe(nombreArchivo))
        {
            Console.WriteLine($"El archivo {nombreArchivo} no existe.");
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
                Console.WriteLine($"El archivo {nombreArchivo} no contiene información válida de la cadetería.");
                return null;
            }

            Cadeteria cadeteria = new Cadeteria(datosCadeteria[0], datosCadeteria[1]);

            return cadeteria;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo {nombreArchivo}: {ex.Message}");
            return null;
        }

    }

}

