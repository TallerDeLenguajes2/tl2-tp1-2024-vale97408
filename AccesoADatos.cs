public interface AccesoADatos
{
    public bool Existe(string nombreArchivo);
    public List<Cadete> LeerCadetes();
    public Cadeteria LeerCadeteria();
}