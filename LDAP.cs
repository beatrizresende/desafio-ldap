using System.Net;
using System.DirectoryServices.Protocols;
using System.Text.RegularExpressions;

namespace OpenConsult;

public class LDAP
{
    private const String Organizacao = "dc=openconsult,dc=com";
    private static void EnviarRequisicao(DirectoryRequest requisicao)
    {
        const String servidor = "localhost";
        const int porta = 389;
        const String usuario = "CN=admin,DC=openconsult,DC=com";
        const String senha = "openconsult";
            
        LdapConnection conexao = new LdapConnection(
            new LdapDirectoryIdentifier(servidor),
            new NetworkCredential()
            {
                UserName = usuario,
                Password = senha,
            },
            AuthType.Basic);
        try
        {
            conexao.SessionOptions.ProtocolVersion = 3;
            conexao.Bind();
            conexao.SendRequest(requisicao);
        }
        finally
        {
               conexao.Dispose();
        }
    }

    public static void AddContainer(String container)
    {
        String cabecalho = string.Format("ou={0},{1}", container, Organizacao);

        AddRequest requisicao = new AddRequest(cabecalho);

        requisicao.Attributes.AddRange(new DirectoryAttributeCollection() {
            new DirectoryAttribute("objectClass", "organizationalUnit"),
            new DirectoryAttribute("ou", container),
        });

        EnviarRequisicao(requisicao);
    }
    
    public static void AddUsuario(Usuario usuario)
    {
        const string regexTelefone = @"[^\d]";
        const string regexNomeELogin = "[^a-zA-Z0-9]";
        string[] nomes = usuario.Nome.Split(" ");
        int sn = nomes.Length - 1;
        
        String cabecalho = string.Format("uid={0},ou=Usuarios,{1}", Regex.Replace(usuario.Login, regexNomeELogin, String.Empty), Organizacao);

        AddRequest requisicao = new AddRequest(cabecalho);

        requisicao.Attributes.AddRange(new DirectoryAttributeCollection() {
            new DirectoryAttribute("objectClass", "inetOrgPerson"),
            new DirectoryAttribute("cn", Regex.Replace(usuario.Nome, regexNomeELogin, String.Empty)),
            new DirectoryAttribute("sn", nomes[sn]),
            new DirectoryAttribute("telephoneNumber", Regex.Replace(usuario.Telefone, regexTelefone, String.Empty)),
            new DirectoryAttribute("departmentNumber", usuario.Grupos),
            new DirectoryAttribute("uid", Regex.Replace(usuario.Login, regexNomeELogin, String.Empty)),
        });

        EnviarRequisicao(requisicao);
    }
    
    public static void AddGrupo(Grupo grupo)
    {
        String cabecalho = string.Format("uid={0},ou=Grupos,{1}", grupo.Identificador, Organizacao);

        AddRequest requisicao = new AddRequest(cabecalho);

        requisicao.Attributes.AddRange(new DirectoryAttributeCollection() {
            new DirectoryAttribute("objectClass", "account"),
            new DirectoryAttribute("description", grupo.Descricao),
            new DirectoryAttribute("userid", grupo.Identificador),
        });

        EnviarRequisicao(requisicao);
    }
    
    public static void ModifyUsuario(String login, String remover, String adicionar)
    {
        String cabecalho = string.Format("uid={0},{1}", login, Organizacao);

        ModifyRequest requisicao = new ModifyRequest();
        requisicao.DistinguishedName = cabecalho;

        DirectoryAttributeModification modificarAtributos = new DirectoryAttributeModification();
        modificarAtributos.Operation = DirectoryAttributeOperation.Replace;
        modificarAtributos.Name = "departmentNumber";
        modificarAtributos.Remove(remover);
        modificarAtributos.Add(adicionar);
        requisicao.Modifications.Add(modificarAtributos);

        EnviarRequisicao(requisicao);
    }
}