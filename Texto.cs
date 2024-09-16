using System;
public class Textos
{
    public static void MostrarTexto(string texto)
    {
        Console.WriteLine(texto);
    }

    public static int PedirInt(string mensaje)
    {
        int valor;
        MostrarTexto(mensaje);
        while (!int.TryParse(Console.ReadLine(), out valor) || valor < 0)
        {
            MostrarTexto("Por favor, ingrese un número válido.");
        }
        return valor;
    }

    public static string PedirString(string mensaje)
    {
        MostrarTexto(mensaje);
        return Console.ReadLine();
    }

    public static void MostrarTextoConEspacios(string texto)
    {
        MostrarTexto("");
        MostrarTexto(texto);
    }

    public static void MostrarTextoEnLinea(string texto)
    {
        Console.Write(texto);
    }

    public static void MostrarTextoConEncabezado(string encabezado, string texto)
    {
        MostrarTexto($"==== {encabezado} ====");
        MostrarTexto(texto);
    }




    //----------------------------------------------

    public static void MostrarListadoPedidos(List<Pedido> ListadoPedidos)
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


    public static Pedido DarDeAltaPedidos(int nroPedido)
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

        return nuevoPedido;
    }

    public static void ModificarEstadoPedido(List<Pedido> ListadoPedidos)
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

    public static void InformeFinalJornada(Cadeteria cadeteria)
    {
        Console.WriteLine("------ INFORME FINAL DE JORNADA -------");

        float montoGanado;
        int cantEnviosCadete, cantTotalEnvios = 0;
        float envioPromedio;

        foreach (var cadete in cadeteria.ListadoCadetes)
        {
            cantEnviosCadete = cadeteria.EnviosCompletos(cadete.Id);
            montoGanado = cadeteria.JornalACobrar(cadete.Id);
            Console.WriteLine($"-Nombre Cadete: {cadete.Nombre} |- Cantidad de pedidos entregados: {cantEnviosCadete} |- Monto ganado: ${montoGanado}");

            cantTotalEnvios = cantTotalEnvios + cantEnviosCadete;
        }

        envioPromedio = cantTotalEnvios / (float)cadeteria.ListadoCadetes.Count;

        Console.WriteLine("\n--- EN GENERAL:");
        Console.WriteLine($"Cantidad total de envios: {cantTotalEnvios}");
        Console.WriteLine($"Promedio de envios por cadete: {envioPromedio}");

    }

    public static void ReasignarPedidos(Cadeteria cadeteria)
    {
        Cadete cadeteOriginal = null;
        int nroPedido;

        Console.WriteLine("");
        Console.WriteLine("------REASIGNAR PEDIDOS--------");

         bool hayPedidos = cadeteria.ListadoPedidos.Any(p => p.Estado != Estado.Entregado && p.Estado != Estado.EnPreparacion); 

         if (!hayPedidos)
         {
             Console.WriteLine("No hay pedidos en el sistema para modificar.");
             return;
         }

        Console.WriteLine("Ingrese el número del pedido a reasignar: ");
        if (!int.TryParse(Console.ReadLine(), out nroPedido))
        {
            Console.WriteLine("Número de pedido inválido.");
            return;
        }

        Console.WriteLine("Seleccione el nuevo cadete al que reasignará el pedido:");
        for (int i = 0; i < cadeteria.ListadoCadetes.Count; i++)
        {
            if (cadeteria.ListadoCadetes[i] != cadeteOriginal)
            {
                Console.WriteLine($"{i + 1} )  {cadeteria.ListadoCadetes[i].Nombre}");
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
                              seleccion <= cadeteria.ListadoCadetes.Count &&
                              cadeteria.ListadoCadetes[seleccion - 1] != cadeteOriginal;

            if (!seleccionValida)
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
            }

        } while (!seleccionValida);


        string respuesta = cadeteria.ReasignarPedidos(nroPedido, seleccion);
        Console.WriteLine(respuesta);
    }



      public static void AsignarPedidos(Cadeteria cadeteria)
    {
        Console.WriteLine("------ ASIGNAR PEDIDOS -------");

        List<Pedido> pedidosPendientes = cadeteria.ListadoPedidos
       .Where(p => p.Estado == Estado.EnPreparacion && p.CadeteAsignado == null)
       .ToList(); // Solo pedidos en preparacion

        // Verificar si hay pedidos pendientes
        if (pedidosPendientes == null || pedidosPendientes.Count == 0)
        {
            Console.WriteLine("No hay pedidos pendientes para asignar.");
            return;
        }

        Console.WriteLine("Seleccione el cadete al que asignará el pedido:");

        // Mostrar listado de cadetes disponibles
        for (int i = 0; i < cadeteria.ListadoCadetes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cadeteria.ListadoCadetes[i].Nombre} ");
        }

        int seleccion = 0;
        bool seleccionValida = false;

        // Validar selección de cadete
        do
        {
            Console.Write("Ingrese el número del cadete a seleccionar: ");
            string nro = Console.ReadLine();
            seleccionValida = int.TryParse(nro, out seleccion) && seleccion >= 1 && seleccion <= cadeteria.ListadoCadetes.Count;

            if (!seleccionValida)
            {
                Console.WriteLine("Selección inválida. Intente de nuevo.");
            }

        } while (!seleccionValida);

        Cadete cadeteSeleccionado = cadeteria.ListadoCadetes[seleccion - 1];

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

        string respuesta = cadeteria.AsignarCadeteAPedido(cadeteSeleccionado.Id, pedidoAsignado.NroPedido);
        pedidosPendientes.Remove(pedidoAsignado); // Eliminar de la lista de pendientes
        Console.WriteLine(respuesta);

    }
}
