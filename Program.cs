Console.WriteLine("======= SISTEMA DE GESTIÓN DE PEDIDOS =======");

// Trabajo con el archivo CSV

string nombreArchivoCadetes = "cadetes.csv";
string nombreArchivoCadeteria = "cadeteria.csv";

AccesoDatosCSV metodoCSV = new AccesoDatosCSV();

// Controlo que los archivos existan. 
if (metodoCSV.Existe(nombreArchivoCadetes) && metodoCSV.Existe(nombreArchivoCadeteria))
{
    // Cargo los datos de Cadetes y Cadeteria
    List<Cadete> listaCadetes = metodoCSV.LeerCadetes(nombreArchivoCadetes);
    string[] cadeterias = metodoCSV.LeerCadeteria(nombreArchivoCadeteria).Split(";");

    // Instancio mi cadeteria y asigno lo leido del CSV
    Cadeteria cadeteria = new Cadeteria(cadeterias[0], cadeterias[1], listaCadetes);



    // ----------MENU

    int opcion;  // No se si esta bien esto
    int nroPedido = 0;

    do
    {
        Console.Clear();
        Console.WriteLine($"====CADETERIA : {cadeteria.Nombre}=====");

        Console.WriteLine("======= SISTEMA DE GESTIÓN DE PEDIDOS =======");

        Console.WriteLine("1. Dar de alta pedidos");
        Console.WriteLine("2. Asignar pedidos a cadetes");
        Console.WriteLine("3. Cambiar estado de un pedido");
        Console.WriteLine("4. Reasignar pedido a otro cadete");
        Console.WriteLine("5. Mostrar informe de pedidos");
        Console.WriteLine("6. Listado de Pedidos");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");

        if (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Por favor, ingrese un número válido.");
            continue;
        }
        // Pedido pedidoNuevo = null;
        switch (opcion)
        {
            case 1:
                nroPedido++;
                cadeteria.DarDeAltaPedidos(nroPedido);
                //pedidoNuevo = cadeteria.DarDeAltaPedidos(nroPedido);
                break;
            case 2:
                cadeteria.AsignarPedidos();
                break;
            case 3:
                cadeteria.ModificarEstadoPedido();
                break;
            case 4:
                cadeteria.ReasignarPedidos();
                break;
            case 5:
                cadeteria.InformeFinalJornada();
                break;
            case 6:
                cadeteria.MostrarListadoPedidos();
                break;

            case 0:
                Console.WriteLine("Saliendo del sistema...");
                break;
            default:
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                break;
        }

        if (opcion != 0)
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    } while (opcion != 0);

}
else
{
    // si no existen los archivos
    Console.WriteLine("No se encontraron los archivos necesarios para cargar los datos de la cadetería.");
    return;

}

