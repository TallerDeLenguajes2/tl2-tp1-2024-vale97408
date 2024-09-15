
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    //private List<Pedido> listadoPedidos; (TP2)

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        //this.ListadoPedidos = listadoPedidos;
        //listadoPedidos = new List<Pedido>();
    }

    public Cadete () // Para la deserialización
    {

    }

    public int Id { get => id; }
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public string Telefono { get => telefono; }
    //public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; } // Si necesito modificar la lista de pedidos y sus datos 


   /*
    public int JornalACobrar()
    {
        int cantidadCobrar = 500 * CantidadPedidosCompletados();
        return cantidadCobrar;
    } 


    public int CantidadPedidosCompletados()
    {
        // Recorro la lista de pedidos con Linq
        return listadoPedidos.Count(pedido => pedido.Estado == Estado.Entregado);

    }

    public int EnviosCompletos()
    {
        return ListadoPedidos.Count(p => p.Estado == Estado.Entregado);
    }*/

}
