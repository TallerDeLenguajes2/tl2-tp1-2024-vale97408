
Console.Clear();
Console.WriteLine("======= SISTEMA DE GESTIÓN DE PEDIDOS =======");

int tipo;
AccesoADatos AccesoDatos = null;
List<Cadete> listaCadetes = null;
Cadeteria cadeteria = null;

Console.WriteLine(" Seleccione el Tipo de Acceso a los Datos que usará:");
Console.WriteLine(" 1. Acceso CSV");
Console.WriteLine(" 2. Acceso JSON");
Console.Write("RESPUESTA: ");
if (!int.TryParse(Console.ReadLine(), out tipo))
{
    Console.WriteLine("Por favor, ingrese un número válido.");
    return;
}

switch (tipo)
{
    case 1:
        AccesoDatos = new AccesoDatosCSV();


        break;
    case 2:
        AccesoDatos = new AccesoDatosJSON();


        break;
    default:
        Console.WriteLine("Opción no válida. Intente de nuevo.");
        break;
}

listaCadetes = AccesoDatos.LeerCadetes();
cadeteria = AccesoDatos.LeerCadeteria();
cadeteria.ListadoCadetes = listaCadetes;


// ----------MENU

int opcion;  // No se si esta bien esto
int nroPedido = 0;

do
{
    Console.Clear();
    Console.WriteLine($"_________________________________CADETERIA : {cadeteria.Nombre} _____________________________");
    Console.WriteLine($"Tipo de Acceso a Datos elegido (1: CSV | 2:JSON ):   [ {tipo} ]");
    Console.WriteLine("");
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

    switch (opcion)
    {
        case 1:
            nroPedido++;
            Pedido pedidoNuevo = Textos.DarDeAltaPedidos(nroPedido);
            cadeteria.ListadoPedidos.Add(pedidoNuevo); // Tp2
            break;
        case 2:
            Textos.AsignarPedidos(cadeteria);
            break;
        case 3:
            Textos.ModificarEstadoPedido(cadeteria.ListadoPedidos);
            break;
        case 4:
            Textos.ReasignarPedidos(cadeteria);
            break;
        case 5:
            Textos.InformeFinalJornada(cadeteria);
            break;
        case 6:
            Textos.MostrarListadoPedidos(cadeteria.ListadoPedidos);
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

