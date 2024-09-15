Console.Clear();
Console.WriteLine("======= SISTEMA DE GESTIÓN DE PEDIDOS =======");

int tipo;

string nombreCadetesCSV = "cadetes.csv";
string nombreCadeteriaCSV = "cadeteria.csv";
AccesoDatosCSV metodoCSV = new AccesoDatosCSV();

string nombreCadetesJSON = "cadetes.json";
string nombreCadeteriaJSON = "cadeteria.json";
AccesoDatosJSON metodoJSON = new AccesoDatosJSON();

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
        if (metodoCSV.Existe(nombreCadetesCSV) && metodoCSV.Existe(nombreCadeteriaCSV))
        {
            listaCadetes = metodoCSV.LeerCadetes(nombreCadetesCSV);
            cadeteria = metodoCSV.LeerCadeteria(nombreCadeteriaCSV);
            cadeteria.ListadoCadetes = listaCadetes;
        }
        else
        {
            Console.WriteLine("No existen los archivos CSV");
        }

        break;
    case 2:
        if (metodoJSON.Existe(nombreCadetesJSON) && metodoJSON.Existe(nombreCadeteriaJSON))
        {
            listaCadetes = metodoJSON.LeerCadetes(nombreCadetesJSON);
            cadeteria = metodoJSON.LeerCadeteria(nombreCadeteriaJSON);
            cadeteria.ListadoCadetes = listaCadetes;
        }
        else
        {
            Console.WriteLine("No existen  los archivos JSON");
        }

        break;
    default:
        Console.WriteLine("Opción no válida. Intente de nuevo.");
        break;
}



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
            cadeteria.DarDeAltaPedidos(nroPedido);
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

