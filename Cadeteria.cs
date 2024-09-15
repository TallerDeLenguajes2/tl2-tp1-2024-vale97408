public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;

    private List<Pedido> listadoPedidos; // TP2

// Saque el listado de cadetes
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        //this.listadoCadetes = listadoCadetes;
        ListadoPedidos = new List<Pedido>(); // Inicializo
    }

    // No quiero que se admita modificación 
    public string Nombre { get => nombre; }
    public string Telefono { get => telefono; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value;}
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }


    // -----------METODOS 

    public void AsignarPedidos()
    {

        List<Pedido> pedidosPendientes = ListadoPedidos
       .Where(p => p.Estado == Estado.EnPreparacion && p.CadeteAsignado == null)
       .ToList(); // Solo pedidos en preparacion

        // Verificar si hay pedidos pendientes
        if (pedidosPendientes == null || pedidosPendientes.Count == 0)
        {
            Console.WriteLine("No hay pedidos pendientes para asignar.");
            return;
        }

        Console.WriteLine("------ ASIGNAR PEDIDOS -------");
        Console.WriteLine("Seleccione el cadete al que asignará el pedido:");

        // Mostrar listado de cadetes disponibles
        for (int i = 0; i < listadoCadetes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {listadoCadetes[i].Nombre} ");
        }

        int seleccion = 0;
        bool seleccionValida = false;

        // Validar selección de cadete
        do
        {
            Console.Write("Ingrese el número del cadete a seleccionar: ");
            string nro = Console.ReadLine();
            seleccionValida = int.TryParse(nro, out seleccion) && seleccion >= 1 && seleccion <= listadoCadetes.Count;

            if (!seleccionValida)
            {
                Console.WriteLine("Selección inválida. Intente de nuevo.");
            }

        } while (!seleccionValida);

        Cadete cadeteSeleccionado = listadoCadetes[seleccion - 1];

        Console.WriteLine("--------------PEDIDOS PENDIENTES--------:");

        for (int i = 0; i < pedidosPendientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}) Pedido Nro: {pedidosPendientes[i].NroPedido}");
        }

        int seleccionPedido = 0;
        bool pedidoValido = false;

        // Validar selección de pedido
        do
        {
            Console.Write("Seleccione el número del pedido a asignar: ");
            string nroPedido = Console.ReadLine();
            pedidoValido = int.TryParse(nroPedido, out seleccionPedido) && seleccionPedido >= 1 && seleccionPedido <= pedidosPendientes.Count;

            if (!pedidoValido)
            {
                Console.WriteLine("Selección de pedido inválida. Intente de nuevo.");
            }

        } while (!pedidoValido);

        Pedido pedidoAsignado = pedidosPendientes[seleccionPedido - 1];
       
        AsignarCadeteAPedido(cadeteSeleccionado.Id, pedidoAsignado.NroPedido);
        pedidosPendientes.Remove(pedidoAsignado); // Eliminar de la lista de pendientes

    }

    public void ReasignarPedidos()
    {

        Pedido pedidoAReasignar = null;
        Cadete cadeteOriginal = null;
        int nroPedido;

        bool hayPedidos = ListadoPedidos.Any(p => p.Estado != Estado.Entregado && p.Estado != Estado.EnPreparacion); // Siempre que haya pedidos que no hayan sido entregados aun, y hayan sido asignados


        if (!hayPedidos)
        {
            Console.WriteLine("No hay pedidos en el sistema para modificar.");
            return;
        }
        Console.WriteLine("");
        Console.WriteLine("------REASIGNAR PEDIDOS--------");
        Console.WriteLine("Ingrese el número del pedido a reasignar: ");
        if (!int.TryParse(Console.ReadLine(), out nroPedido))
        {
            Console.WriteLine("Número de pedido inválido.");
            return;
        }

        // Uso de funcion LINQ para encontrar el pedido a reasignar
        pedidoAReasignar = ListadoPedidos.FirstOrDefault(p => p.NroPedido == nroPedido);

        if (pedidoAReasignar != null)
        {
            if (pedidoAReasignar.Estado == Estado.Entregado)
            {
                Console.WriteLine($"El pedido Nro: {nroPedido} ya fue entregado y no puede ser reasignado.");
                return;
            }

            if (pedidoAReasignar.Estado == Estado.EnPreparacion)
            {
                Console.WriteLine($"El pedido Nro: {nroPedido} esta 'En Preparacion' no puede ser reasignado.");
                return;
            }

            cadeteOriginal = pedidoAReasignar.CadeteAsignado; // reasigno            
        }

        if (pedidoAReasignar == null)
        {
            Console.WriteLine($"No se encontró el pedido con el número {nroPedido} para ser reasignado.");
            return;
        }

        // Elimino el pedido de la lista del cadete original
        // cadeteOriginal.ListadoPedidos.Remove(pedidoAReasignar);

        Console.WriteLine("Seleccione el nuevo cadete al que reasignará el pedido:");
        for (int i = 0; i < listadoCadetes.Count; i++)
        {
            if (listadoCadetes[i] != cadeteOriginal)
            {
                Console.WriteLine($"{i + 1} )  {listadoCadetes[i].Nombre}");
            }
        }

        int seleccion = 0;
        bool seleccionValida = false;

        do
        {
            Console.Write("Ingrese el número del nuevo cadete: ");
            string input = Console.ReadLine();
            seleccionValida = int.TryParse(input, out seleccion) &&
                              seleccion >= 1 &&
                              seleccion <= listadoCadetes.Count &&
                              listadoCadetes[seleccion - 1] != cadeteOriginal;

            if (!seleccionValida)
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
            }

        } while (!seleccionValida);

        // Asigno el pedido al nuevo cadete
        pedidoAReasignar.CadeteAsignado = listadoCadetes[seleccion - 1];

        //listadoCadetes[seleccion - 1].ListadoPedidos.Add(pedidoAReasignar); // Ya no  Guardo en la lista

        Console.WriteLine($"Pedido {nroPedido} reasignado a {listadoCadetes[seleccion - 1].Nombre}.");
    }

    public void InformeFinalJornada()
    {
        Console.WriteLine("------ INFORME FINAL DE JORNADA -------");

        float montoGanado;
        int cantEnviosCadete, cantTotalEnvios = 0;
        float envioPromedio;

        foreach (var cadete in listadoCadetes)
        {
            cantEnviosCadete = EnviosCompletos(cadete.Id);
            montoGanado = JornalACobrar(cadete.Id);
            Console.WriteLine($"-Nombre Cadete: {cadete.Nombre} |- Cantidad de pedidos entregados: {cantEnviosCadete} |- Monto ganado: ${montoGanado}");

            cantTotalEnvios = cantTotalEnvios + cantEnviosCadete;
        }

        envioPromedio = cantTotalEnvios / (float)listadoCadetes.Count;

        Console.WriteLine("\n--- EN GENERAL:");
        Console.WriteLine($"Cantidad total de envios: {cantTotalEnvios}");
        Console.WriteLine($"Promedio de envios por cadete: {envioPromedio}");

    }

    public void ModificarEstadoPedido()
    {
        // No puedo modificar el estado de un pedido entregado, en preparacion

        Console.WriteLine("");
         Console.WriteLine("------CAMBIAR ESTADO DE PEDIDO--------");

        if (ListadoPedidos.Count == 0 || ListadoPedidos == null)
        {
            Console.WriteLine("No hay pedidos en el sistema para modificar.");
            return;
        }

        Console.WriteLine("Ingrese el número del pedido a modificar su estado: ");
        int nroPedido;
        if (!int.TryParse(Console.ReadLine(), out nroPedido))
        {
            Console.WriteLine("Número de pedido inválido.");
            return;
        }

        Pedido pedidoEncontrado = ListadoPedidos.FirstOrDefault(p => p.NroPedido == nroPedido);

        if (pedidoEncontrado == null)
        {
            Console.WriteLine($"No se encontró ningún pedido con el número {nroPedido}.");
            return;
        }

        if (pedidoEncontrado.Estado == Estado.EnPreparacion)
        {
            Console.WriteLine($"El pedido Nro: {pedidoEncontrado.NroPedido} está en 'En Preparación' , no fue asignado a un cadete y no se puede modificar.");
            return;
        }

        if (pedidoEncontrado != null && pedidoEncontrado.Estado != Estado.EnPreparacion)
        {

            Console.WriteLine($"Pedido Nro: {pedidoEncontrado.NroPedido} - Estado Actual: {pedidoEncontrado.Estado}");

            Console.WriteLine("Seleccione el nuevo estado del pedido:");
            Console.WriteLine("1. En Camino");
            Console.WriteLine("2. Entregado");

            int opcionEstado;
            if (int.TryParse(Console.ReadLine(), out opcionEstado))
            {
                switch (opcionEstado)
                {
                    case 1:
                        pedidoEncontrado.Estado = Estado.EnCamino;
                        Console.WriteLine("El pedido ha sido actualizado a 'En Camino'.");
                        break;
                    case 2:
                        pedidoEncontrado.Estado = Estado.Entregado;
                        Console.WriteLine("El pedido ha sido actualizado a 'Entregado'.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. El estado del pedido no ha cambiado.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida. El estado del pedido no ha cambiado.");
            }
        }
        else
        {
            Console.WriteLine($"No se encontró ningún pedido con el número {nroPedido}.");
        }
    }

    public void DarDeAltaPedidos(int nroPedido)
    {

        string observacionPedido, nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion;

         Console.WriteLine("------DAR DE ALTA PEDIDO--------");

        Console.WriteLine("Observación del pedido: ");
        observacionPedido = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(observacionPedido))
        {
            Console.WriteLine("La observación del pedido no puede estar vacía. Ingrese nuevamente: ");
            observacionPedido = Console.ReadLine();
        }

        Console.WriteLine(" ------------Datos del cliente----------- ");

        Console.WriteLine("Nombre: ");
        nombreCliente = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(nombreCliente))
        {
            Console.WriteLine("El nombre del cliente no puede estar vacío. Ingrese nuevamente: ");
            nombreCliente = Console.ReadLine();
        }

        Console.WriteLine("Teléfono del cliente: ");
        telefonoCliente = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(telefonoCliente))
        {
            Console.WriteLine("El teléfono del cliente no puede estar vacío. Ingrese nuevamente: ");
            telefonoCliente = Console.ReadLine();
        }

        Console.WriteLine("Dirección del cliente: ");
        direccionCliente = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(direccionCliente))
        {
            Console.WriteLine("La dirección del cliente no puede estar vacía. Ingrese nuevamente: ");
            direccionCliente = Console.ReadLine();
        }

        Console.WriteLine("Ingrese una referencia de la dirección (opcional): ");
        referenciaDireccion = Console.ReadLine();

        Pedido nuevoPedido = new Pedido(nroPedido, observacionPedido, Estado.EnPreparacion, nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion);

        Console.WriteLine("Pedido creado exitosamente:");
        Pedido.VerDatosPedido(nuevoPedido);

        ListadoPedidos.Add(nuevoPedido); // Tp2

    }


    // METODOS TP2

    public int JornalACobrar(int idCadete)
    {
        int pedidosEntregados = EnviosCompletos(idCadete);
        int cantidadCobrar = 500 * pedidosEntregados;
        return cantidadCobrar;
    }

    public void AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        Cadete cadeteSeleccionado = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);
        Pedido pedidoSeleccionado = listadoPedidos.FirstOrDefault(p => p.NroPedido == idPedido);

        pedidoSeleccionado.CadeteAsignado = cadeteSeleccionado;

        // Cambio estado de pedido a en camino cuando ya es asignado
        pedidoSeleccionado.Estado = Estado.EnCamino;

        Console.WriteLine($"===Pedido {pedidoSeleccionado.NroPedido} asignado al cadete {cadeteSeleccionado.Nombre}.===");
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
        // return ListadoPedidos.Count(p => p.CadeteAsignado.Id == id && p.Estado == Estado.Entregado);
    }

    public void MostrarListadoPedidos()
    {
        if (ListadoPedidos.Count == 0)
        {
            Console.WriteLine("No hay pedidos en el sistema.");
            return;
        }

        Console.WriteLine("------ LISTADO DE PEDIDOS ------");
        foreach (var pedido in ListadoPedidos)
        {
            string infoPedido = $"Número de Pedido: {pedido.NroPedido} | Estado: {pedido.Estado} ";
            if (pedido.Estado != Estado.EnPreparacion)
            {

                infoPedido += $"| Cadete: {pedido.CadeteAsignado.Nombre}";

            }
            else
            {
                infoPedido += "| Cadete: No Asignado";
            }
            Console.WriteLine(infoPedido);
        }
    }
}
