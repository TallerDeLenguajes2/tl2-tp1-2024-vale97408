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

    public void asignarPedidos(Pedido pedido)
    {

        Console.WriteLine("------ ASIGNAR PEDIDOS -------");
        Console.WriteLine("Seleccione el cadete al que asignará el pedido:");

        for (int i = 0; i < listadoCadetes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {listadoCadetes[i].Nombre}");
        }

        int seleccion = 0;
        bool seleccionValida = false;

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

        // Asignar el pedido al cadete seleccionado
        listadoCadetes[seleccion - 1].ListadoPedidos.Add(pedido);
        Console.WriteLine($"Pedido asignado a CADETE : [{listadoCadetes[seleccion - 1].Nombre}]. ");
    }

    public void reasignarPedidos(int nroPedido)
    {
        Console.WriteLine("------REASIGNAR PEDIDOS--------");
        Pedido pedidoAReasignar = null;
        Cadete cadeteOriginal = null;

        // Buscar el pedido que quiero reasignar en la lista de todos los cadetes
        foreach (var cadete in listadoCadetes)
        {
            // Uso de funcion LINQ para encontrar al cadete que tenga el pedido a reasignar
            pedidoAReasignar = cadete.ListadoPedidos.FirstOrDefault(p => p.NroPedido == nroPedido);

            if (pedidoAReasignar != null)
            {
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

    public Pedido DarDeAltaPedidos(int nroPedido)
    {
        // Creo el pedido y lo cargo como pedido en estado pendiente 
        string observacionPedido, nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion;

        // Solicitar datos del pedido y cliente
        do
        {
            Console.WriteLine("=== Dar de Alta Pedido ===");

            Console.Write("Observación del pedido: ");
            observacionPedido = Console.ReadLine();

            Console.Write(" ------------Datos del cliente----------- ");
            Console.Write("Nombre: ");
            nombreCliente = Console.ReadLine();

            Console.Write("Teléfono del cliente: ");
            telefonoCliente = Console.ReadLine();

           
            Console.Write("Dirección del cliente: ");
            direccionCliente = Console.ReadLine();


            Console.Write("Ingrese una referencia de la dirección (opcional): ");
            referenciaDireccion = Console.ReadLine();

            // Validación de los campos obligatorios
            if (string.IsNullOrWhiteSpace(observacionPedido) || string.IsNullOrWhiteSpace(nombreCliente) ||
                string.IsNullOrWhiteSpace(direccionCliente) || string.IsNullOrWhiteSpace(telefonoCliente))
            {
                Console.WriteLine("Debe completar todos los campos obligatorios.");
            }

        } while (string.IsNullOrWhiteSpace(observacionPedido) || string.IsNullOrWhiteSpace(nombreCliente) ||
                 string.IsNullOrWhiteSpace(direccionCliente) || string.IsNullOrWhiteSpace(telefonoCliente));

        // Crear un nuevo pedido con los datos ingresados
        Pedido nuevoPedido = new Pedido(nroPedido, observacionPedido, Estado.EnPreparacion, nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion);

        // Mostrar la confirmación de creación
        Console.WriteLine("Pedido creado exitosamente:");
        Pedido.VerDatosPedido(nuevoPedido);

        return nuevoPedido; // Devuelve el pedido creado 

    }




}
