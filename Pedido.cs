public enum Estado
{
    EnPreparacion,
    EnCamino,
    Entregado
}

public class Pedido
{
    private int nroPedido;
    private string obs;
    private Cliente cliente;
    private Estado estado;

    private Cadete cadeteAsignado; // Referencia a Cadete (TP2)



    public Pedido(int nroPedido, string obs, Estado estado, string nombre, string direccion, string telefono, string datosreferenciaDireccion)
    {
        this.nroPedido = nroPedido;
        this.obs = obs;
        this.estado = Estado.EnPreparacion;

        // Creo un cliente 
        cliente = new Cliente(nombre, direccion, telefono, datosreferenciaDireccion);

    }

    public int NroPedido { get => nroPedido; }
    public string Obs { get => obs; }
    public Estado Estado { get => estado; set => estado = value; }
    public Cadete CadeteAsignado { get => cadeteAsignado; set => cadeteAsignado = value; }

    public void VerDireccionCliente()
    {
        Console.WriteLine($"--DIRECCION CLIENTE: {cliente.Direccion} ");

        // Si es que hay alguna referencia 
        if (cliente.DatosreferenciaDireccion != null)
        {
            Console.WriteLine($"- Referencia: {cliente.DatosreferenciaDireccion} ");
        }
    }

    public void VerDatosCliente()
    {
        Console.WriteLine("DATOS DEL CLIENTE");
        Console.WriteLine($"- Nombre: {cliente.Nombre} ");
        Console.WriteLine($"--Telefono: {cliente.Telefono} ");
        VerDireccionCliente();
    }

    public static void VerDatosPedido(Pedido pedido)
    {

        Console.WriteLine("----DATOS DEL PEDIDO CARGADO------");
        Console.WriteLine($"--Nro Pedido: {pedido.nroPedido} ");
        Console.WriteLine($"--Obervación: {pedido.obs} ");
        Console.WriteLine($"--Cliente: {pedido.cliente.Nombre} ");
        Console.WriteLine($"--Teléfono: {pedido.cliente.Telefono} ");
        Console.WriteLine($"--Referencia: {pedido.cliente.DatosreferenciaDireccion} ");

        Console.WriteLine($"--Estado: {pedido.Estado} ");
    }

}

