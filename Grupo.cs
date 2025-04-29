namespace OpenConsult;

public class Grupo
{
    public String Identificador { get; private set; }
    public String Descricao { get; private set; }

    public Grupo(String id, String descricao)
    {
        Identificador = id;
        Descricao = descricao;
    }
}