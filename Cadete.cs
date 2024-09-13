
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> listadoPedidos;

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        //this.ListadoPedidos = listadoPedidos;
        listadoPedidos = new List<Pedido>();
    }

    public int Id { get => id; }
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public string Telefono { get => telefono; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; } // Si necesito modificar la lista de pedidos y sus datos 

    public int JornalACobrar()
    {
        int cantidadCobrar = 500 * cantidadPedidosCompletados();
        return cantidadCobrar;
    }


    public int cantidadPedidosCompletados()
    {
        // Recorro la lista de pedidos con Linq
        return listadoPedidos.Count(pedido => pedido.Estado == Estado.Entregado);

    }

    public void buscaPedido(int nroPedido)
    {
        Pedido pedidoEncontrado = null;

        foreach (var pedido in listadoPedidos)
        {
            if (pedido.NroPedido == nroPedido)
            {
                pedidoEncontrado = pedido; // Guarda el pedido encontrado
                break; // Sale del bucle una vez que se encuentra el pedido
            }
        }

        if (pedidoEncontrado != null)
        {
            pedidoEncontrado.Estado = Estado.EnCamino; // Cambia el estado 
            Console.WriteLine($"--El pedido NRO  [{nroPedido}] ahora está en camino.");
        }
        else
        {
            Console.WriteLine($"No se encontró el pedido con el número {nroPedido}.");
        }

    }

    public void entregaPedido(int nroPedido)
    {

        Pedido pedidoEntregado = null;

        foreach (var pedido in listadoPedidos)
        {
            if (pedido.NroPedido == nroPedido)
            {
                pedidoEntregado = pedido; // Guarda el pedido encontrado
                break; // Sale del bucle una vez que se encuentra el pedido
            }
        }

        if (pedidoEntregado != null)
        {
            pedidoEntregado.Estado = Estado.EnCamino; // Cambia el estado 
            Console.WriteLine($"--El pedido NRO [{nroPedido}] ahora fue entregado con éxito.");
        }
        else
        {
            Console.WriteLine($"No se entrego el pedido con el número {nroPedido}.");
        }

    }

    public int enviosCompletos()
    {
        return ListadoPedidos.Count(p => p.Estado == Estado.Entregado);
    }

}
