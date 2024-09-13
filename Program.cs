Console.WriteLine("======= SISTEMA DE GESTIÓN DE PEDIDOS =======");

// Trabajo con el archivo CSV

string nombreArchivoCadetes = "cadetes.csv";
string nombreArchivoCadeteria = "cadeteria.csv";

// Instancio la clase 
manejoAchivos metodoCSV = new manejoAchivos();

// Controlo que los archivos existan. 
if (metodoCSV.Existe(nombreArchivoCadetes) && metodoCSV.Existe(nombreArchivoCadeteria))
{
    // Cargo los datos de Cadetes y Cadeteria
    List<Cadete> listaCadetes = metodoCSV.LeerCadetes(nombreArchivoCadetes);
    string[] cadeterias = metodoCSV.LeerCadeteria(nombreArchivoCadeteria).Split(";");

    // Instancio mi cadeteria y asigno lo leido del CSV
    Cadeteria cadeteria = new Cadeteria(cadeterias[0], cadeterias[1], listaCadetes);



    // ----------MENU

    // Mostrar el menú y manejar las opciones
    int opcion;  // No se si esta bien esto
    do
    {
        Console.Clear();
        Console.WriteLine("======= SISTEMA DE GESTIÓN DE PEDIDOS =======");
        Console.WriteLine("1. Dar de alta pedidos");
        Console.WriteLine("2. Asignar pedidos a cadetes");
        Console.WriteLine("3. Cambiar estado de un pedido");
        Console.WriteLine("4. Reasignar pedido a otro cadete");
        Console.WriteLine("5. Mostrar informe de pedidos");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");

        if (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Por favor, ingrese un número válido.");
            continue;
        }
        
        int nroPedido=0;
        Pedido pedidoNuevo = null; 
        Pedido pedidoNoAsignado = null; 
        
       
        switch (opcion)
        {
            case 1:
                cadeteria.DarDeAltaPedidos(nroPedido);

                break;
            case 2:
                cadeteria.asignarPedidos(pedidoNuevo);
                break;
            case 3:
                //CambiarEstadoDePedido(cadeteria);
                break;
            case 4:
                cadeteria.reasignarPedidos(nroPedido);
                break;
            case 5:
                cadeteria.informeFinalJornada();
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

