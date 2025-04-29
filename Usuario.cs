namespace OpenConsult;

public class Usuario
{
    public String Nome { get; private set; }
    public String Login { get; private set; }
    public String Telefone { get; private set; }
    public String Grupos { get; private set; }

    public Usuario(String nome, String login, String telefone, String grupos)
    {
        Nome = nome;
        Login = login;
        Telefone = telefone;
        Grupos = grupos;
    }
}