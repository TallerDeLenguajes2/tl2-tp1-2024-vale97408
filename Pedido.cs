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

    public Pedido(int nroPedido, string obs, Estado estado, string nombre, string direccion , string telefono, string datosreferenciaDireccion )
    {
        this.nroPedido = nroPedido;
        this.obs = obs;
        this.estado = Estado.EnPreparacion;

        // Creo un cliente 
        cliente = new Cliente (nombre, direccion, telefono, datosreferenciaDireccion);
       
    }

    public int NroPedido { get => nroPedido;  }
    public string Obs { get => obs;}
    public Estado Estado { get => estado; set => estado = value; }

    public void VerDireccionCliente()
    {         
      Console.WriteLine ($"--DIRECCION CLIENTE: {cliente.Direccion} ");

     // Si es que hay alguna referencia 
     if(cliente.DatosreferenciaDireccion != null)
     {
      Console.WriteLine ($"- Referencia: {cliente.DatosreferenciaDireccion} "); 
      }

        
    }

    public void VerDatosCliente()
    {
        Console.WriteLine ("DATOS DEL CLIENTE");
        Console.WriteLine($"--Nombre: {cliente.Nombre} ");
        Console.WriteLine($"--Telefono: {cliente.Telefono} ");
         VerDireccionCliente(); 
    }

    public static void VerDatosPedido(Pedido pedido)
    {
        

    }

}

