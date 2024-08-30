public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string? datosreferenciaDireccion; // Puede ser nulo

    public Cliente (string nombre, string direccion, string telefono, string? datosreferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion=direccion;
        this.telefono= telefono;
        this.datosreferenciaDireccion= datosreferenciaDireccion; 
    }

}

