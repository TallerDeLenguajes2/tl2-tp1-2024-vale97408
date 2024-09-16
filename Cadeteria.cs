public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos; // TP2
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        //this.listadoCadetes = listadoCadetes;
        ListadoPedidos = new List<Pedido>(); // Inicializo
    }

    public string Nombre { get => nombre; }
    public string Telefono { get => telefono; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }


    // -----------METODOS 

    public string ReasignarPedidos( int nroPedido, int seleccionCadete)
    {

        Pedido pedidoAReasignar = null;
        Cadete cadeteOriginal = null;
       
        // Uso de funcion LINQ para encontrar el pedido a reasignar
        pedidoAReasignar = ListadoPedidos.FirstOrDefault(p => p.NroPedido == nroPedido);

        if (pedidoAReasignar != null)
        {
            if (pedidoAReasignar.Estado == Estado.Entregado)
            {
                return $"El pedido Nro: {nroPedido} ya fue entregado y no puede ser reasignado.";
            }

            if (pedidoAReasignar.Estado == Estado.EnPreparacion)
            {
                return $"El pedido Nro: {nroPedido} esta 'En Preparacion' no puede ser reasignado.";
           
            }

            cadeteOriginal = pedidoAReasignar.CadeteAsignado; // reasigno            
        }

        if (pedidoAReasignar == null)
        {
            return $"No se encontró el pedido con el número {nroPedido} para ser reasignado.";
            
        }


        pedidoAReasignar.CadeteAsignado = listadoCadetes[seleccionCadete - 1];

        return $"Pedido {nroPedido} reasignado a {listadoCadetes[seleccionCadete - 1].Nombre}.";
    }

    // METODOS TP2

    public int JornalACobrar(int idCadete)
    {
        int pedidosEntregados = EnviosCompletos(idCadete);
        int cantidadCobrar = 500 * pedidosEntregados;
        return cantidadCobrar;
    }

    public string AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        Cadete cadeteSeleccionado = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);
        Pedido pedidoSeleccionado = listadoPedidos.FirstOrDefault(p => p.NroPedido == idPedido);

        pedidoSeleccionado.CadeteAsignado = cadeteSeleccionado;

        // Cambio estado de pedido a en camino cuando ya es asignado
        pedidoSeleccionado.Estado = Estado.EnCamino;

        return $"===Pedido {pedidoSeleccionado.NroPedido} asignado al cadete {cadeteSeleccionado.Nombre}.===";
    }

    //---------------Extras-----------

    public int EnviosCompletos(int id)
    {
        // Control para pedidos sin cadete 
        int totalEnvios = 0;
        foreach (var pedido in ListadoPedidos)
        {
            if (pedido.CadeteAsignado != null && pedido.CadeteAsignado.Id == id && pedido.Estado == Estado.Entregado)
            {
                totalEnvios++;
            }
        }
        return totalEnvios;
    }

    
}
