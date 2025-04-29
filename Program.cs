using System.Xml;

namespace OpenConsult;

public static class Program
{
    public static void Main()
    {
        String caminhoArquivos = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        const String addGrupo1 = "AddGrupo1.xml";
        const String addGrupo2 = "AddGrupo2.xml";
        const String addGrupo3 = "AddGrupo3.xml";
        const String addUsuario1 = "AddUsuario1.xml";
        const String modifyUsuario = "ModifyUsuario.xml";
        const String addAttr = "add/add-attr";
        
        XmlDocument documento = new XmlDocument();
        
        LDAP.AddContainer("Grupos");
        LDAP.AddContainer("Usuarios");
        
        //AddGrupo1
        documento.Load(Path.Combine(caminhoArquivos, addGrupo1));
        var nodes = documento.SelectNodes(addAttr);
        Grupo grupo1 = new Grupo(nodes[0].InnerText, nodes[1].InnerText);
        LDAP.AddGrupo(grupo1);
        
        //AddGrupo2
        documento.Load(Path.Combine(caminhoArquivos, addGrupo2));
        nodes = documento.SelectNodes(addAttr);
        Grupo grupo2 = new Grupo(nodes[0].InnerText, nodes[1].InnerText);
        LDAP.AddGrupo(grupo2);
        
        //AddGrupo3
        documento.Load(Path.Combine(caminhoArquivos, addGrupo3));
        nodes = documento.SelectNodes(addAttr);
        Grupo grupo3 = new Grupo(nodes[0].InnerText, nodes[1].InnerText);
        LDAP.AddGrupo(grupo3);
        
        //AddUsuario1
        documento.Load(Path.Combine(caminhoArquivos, addUsuario1));
        nodes = documento.SelectNodes(addAttr);
        Usuario usuario1 = new Usuario(nodes[0].InnerText, nodes[1].InnerText, nodes[2].InnerText, nodes[3].InnerText);
        LDAP.AddUsuario(usuario1);
        
        //ModifyUsuario
        documento.Load(Path.Combine(caminhoArquivos, modifyUsuario));
        var login = documento.SelectSingleNode("modify/association");
        var remover = documento.SelectSingleNode("modify/modify-attr/remove-value");
        var adicionar = documento.SelectSingleNode("modify/modify-attr/add-value");
        // LDAP.ModifyUsuario(login.InnerText, remover.InnerText, adicionar.InnerText);
    }
}