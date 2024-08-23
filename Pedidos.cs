public class Pedidos
{
    private int nroPedido;
    private string obs;
    private Cliente cliente;
    private string estado;

    public int NroPedido { get => nroPedido; set => nroPedido = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public string Estado { get => estado; set => estado = value; }
}

