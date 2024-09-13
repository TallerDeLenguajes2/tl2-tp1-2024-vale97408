public class manejoAchivos
{
    private const string CarpetaCSV = "ArchivosCSV/";

    public bool Existe(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaCSV, nombreArchivo);
        return File.Exists(ruta);
    }




    // Lee los datos de la cadetería desde un archivo CSV (puede ser solo una línea o varias)
    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaCSV, nombreArchivo);
        List<Cadete> cadetes = new List<Cadete>();


        if (!Existe(nombreArchivo))
        {
            Console.WriteLine($"El archivo {nombreArchivo} no existe.");
            return cadetes; // Devuelve una lista vacía si el archivo no existe
        }

        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                string linea;
                while ((linea = strReader.ReadLine()) != null)
                {
                    // Supongamos que los datos están separados por punto y coma ';'
                    var datos = linea.Split(';');

                    if (datos.Length >= 4) // Asegurarse de que hay suficientes columnas para evitar errores
                    {
                        // Crear un nuevo cadete con una lista de pedidos vacía
                        var cadete = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
                        cadetes.Add(cadete);
                    }
                    else
                    {
                        Console.WriteLine($"Línea inválida en el archivo: {linea}");
                    }
                }
            }
        }
        return cadetes;
    }

    public string LeerCadeteria(string nombreArchivo)
    {
        string ruta = Path.Combine(CarpetaCSV, nombreArchivo);
        string infoCadeteria;

        // Verificar si el archivo existe antes de intentar abrirlo
        if (!File.Exists(ruta))
        {
            Console.WriteLine($"El archivo {nombreArchivo} no existe.");
            return string.Empty; // Devuelve una cadena vacía si el archivo no existe
        }

        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                infoCadeteria = strReader.ReadToEnd();
                // No es necesario cerrar explícitamente archivoOpen aquí, ya que 'using' lo maneja
            }
        }

        return infoCadeteria;

    }

}

