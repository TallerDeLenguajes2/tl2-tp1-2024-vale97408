public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string? datosreferenciaDireccion; // Puede ser nulo


  //  Para inicializar los atributos de la clase con los valores que se pasan como parámetros
    public Cliente (string nombre, string direccion, string telefono, string? datosreferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion=direccion;
        this.telefono= telefono;
        this.datosreferenciaDireccion= datosreferenciaDireccion; 
    }

  // Para que no admita modificaciones...
    public string Nombre { get => nombre;}
    public string Direccion { get => direccion;}
    public string Telefono { get => telefono; }
    public string? DatosreferenciaDireccion { get => datosreferenciaDireccion; }
}

