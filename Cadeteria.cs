using System.Runtime;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;


    public Cadeteria(string nombre, string telefono, List<Cadete> listadoCadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listadoCadetes = listadoCadetes;
    }

    // No quiero que se admita modificación 
    public string Nombre { get => nombre; }
    public string Telefono { get => telefono; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; }


    // -----------METODOS 

    public void asignarPedidos(List<Pedido> pedidosPendientes)
    {
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
            Console.WriteLine($"{i + 1}. {listadoCadetes[i].Nombre} (Pedidos asignados: {listadoCadetes[i].ListadoPedidos.Count})");
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

        // Verificar si el cadete tiene capacidad para recibir más pedidos
        Cadete cadeteSeleccionado = listadoCadetes[seleccion - 1];
        int maxPedidosPorCadete = 5; // Puedes definir un límite de pedidos por cadete

        if (cadeteSeleccionado.ListadoPedidos.Count >= maxPedidosPorCadete)
        {
            Console.WriteLine($"El cadete {cadeteSeleccionado.Nombre} ya tiene el máximo de {maxPedidosPorCadete} pedidos asignados.");
            return;
        }

        // Seleccionar el pedido pendiente para asignar
        Console.WriteLine("Pedidos pendientes:");
        for (int i = 0; i < pedidosPendientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Pedido Nro: {pedidosPendientes[i].NroPedido}");
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

        // Asigno el pedido al cadete 
        Pedido pedidoAsignado = pedidosPendientes[seleccionPedido - 1];
        cadeteSeleccionado.ListadoPedidos.Add(pedidoAsignado);
        pedidosPendientes.Remove(pedidoAsignado); // Eliminar de la lista de pendientes
        // Cambio estado de pedido a en camino cuando ya es asignado
        pedidoAsignado.Estado = Estado.EnCamino;

        Console.WriteLine($"Pedido {pedidoAsignado.NroPedido} asignado al cadete {cadeteSeleccionado.Nombre}.");
    }

    public void reasignarPedidos()
    {

        Pedido pedidoAReasignar = null;
        Cadete cadeteOriginal = null;
        int nroPedido;

        bool hayPedidos = listadoCadetes.Any(c => c.ListadoPedidos.Any(p => p.Estado != Estado.Entregado)); // Siempre que haya pedidos y no hayan sido entregados aun


        if (!hayPedidos)
        {
            Console.WriteLine("No hay pedidos en el sistema para modificar.");
            return;
        }

        Console.WriteLine("------REASIGNAR PEDIDOS--------");
        Console.WriteLine("Ingrese el número del pedido a modificar: ");
        if (!int.TryParse(Console.ReadLine(), out nroPedido))
        {
            Console.WriteLine("Número de pedido inválido.");
            return;
        }


        // Buscar el pedido que quiero reasignar en la lista de todos los cadetes
        foreach (var cadete in listadoCadetes)
        {
            // Uso de funcion LINQ para encontrar al cadete que tenga el pedido a reasignar
            pedidoAReasignar = cadete.ListadoPedidos.FirstOrDefault(p => p.NroPedido == nroPedido);

            if (pedidoAReasignar != null)
            {

                // Si fue 'Entregado' no lo reasgina
                if (pedidoAReasignar.Estado == Estado.Entregado)
                {
                    Console.WriteLine($"El pedido Nro: {nroPedido} ya fue entregado y no puede ser reasignado.");
                    return;
                }

                cadeteOriginal = cadete; // reasigno
                break;
            }
        }

        if (pedidoAReasignar == null)
        {
            Console.WriteLine($"No se encontró el pedido con el número {nroPedido} para ser reasignado.");
            return;
        }

        // Elimino el pedido de la lista del cadete original
        cadeteOriginal.ListadoPedidos.Remove(pedidoAReasignar);

        // Mostrar la lista de cadetes para seleccionar uno nuevo
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

        // Asignar el pedido al nuevo cadete
        listadoCadetes[seleccion - 1].ListadoPedidos.Add(pedidoAReasignar); // Guardo en la lista

        Console.WriteLine($"Pedido {nroPedido} reasignado a {listadoCadetes[seleccion - 1].Nombre}.");
    }

    public void informeFinalJornada()
    {
        Console.WriteLine("------ INFORME FINAL DE JORNADA -------");

        float montoGanado;
        int cantEnviosCadete, cantTotalEnvios = 0;
        float envioPromedio;

        foreach (var cadete in listadoCadetes)
        {
            cantEnviosCadete = cadete.enviosCompletos();
            montoGanado = cadete.JornalACobrar();
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
        bool hayPedidos = listadoCadetes.Any(c => c.ListadoPedidos.Any());
        if (!hayPedidos)
        {
            Console.WriteLine("No hay pedidos en el sistema para modificar.");
            return;
        }


        Console.WriteLine("Ingrese el número del pedido a modificar: ");
        int nroPedido;
        if (!int.TryParse(Console.ReadLine(), out nroPedido))
        {
            Console.WriteLine("Número de pedido inválido.");
            return;
        }

        // Localizo al cadete con el nro del pedido, sino devuelve null
        Cadete cadeteConPedido = listadoCadetes.FirstOrDefault(c => c.ListadoPedidos.Any(p => p.NroPedido == nroPedido));

        if (cadeteConPedido != null)
        {
            Pedido pedidoEncontrado = cadeteConPedido.ListadoPedidos.FirstOrDefault(p => p.NroPedido == nroPedido);

            if (pedidoEncontrado != null)
            {

                Console.WriteLine($"Pedido Nro: {pedidoEncontrado.NroPedido} - Estado Actual: {pedidoEncontrado.Estado}");

                Console.WriteLine("Seleccione el nuevo estado del pedido:");
                //Console.WriteLine("1. En Preparación");
                Console.WriteLine("1. En Camino");
                Console.WriteLine("2. Entregado");

                int opcionEstado;
                if (int.TryParse(Console.ReadLine(), out opcionEstado))
                {
                    switch (opcionEstado)
                    {
                        /*case 1:
                            pedidoEncontrado.Estado = Estado.EnPreparacion;
                            Console.WriteLine("El pedido ha sido actualizado a 'En Preparación'.");
                            break;*/
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
        else
        {
            Console.WriteLine($"No se encontró ningún cadete con un pedido con el número {nroPedido}.");
        }
    }







    public Pedido DarDeAltaPedidos(int nroPedido)
    {

        string observacionPedido, nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion;

        Console.WriteLine("=== Dar de Alta Pedido ===");

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

        return nuevoPedido;
    }

}
