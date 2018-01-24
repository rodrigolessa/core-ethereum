using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace demo.xmlSearch
{
    class Program
    {
        static void Main(string[] args)
        {
/*
            <BPQL>
                <header resourceUse="0"></header>
                <body>
                    <processo databaseName="TJSP" tableName="PrimeiraInstancia">
                        <instancia>1</instancia>
                        <url_processo>https://esaj.tjsp.jus.br/cpopg/show.do?processo.codigo=2SZX268AZ0000&amp;processo.foro=100&amp;uuidCaptcha=sajcaptcha_f641bb54ad0c4a47a489dccfed95c582</url_processo>
                        <numero_processo>0033739-92.1999.8.26.0100</numero_processo>
                        <numero_antigo>583.00.1999.033739</numero_antigo>
                        <classe>Falência de Empresários, Sociedades Empresáriais, Microempresas e Empresas de Pequeno Porte</classe>
                        <area>Cível</area>
                        <acao>Recuperação judicial e Falência</acao>
                        <localizacao>Mesa do Diretor - CONFERÊNCIA</localizacao>
                        <vara>Vara de Falências e Recuperações Judiciais</vara>
                        <numero_vara>3</numero_vara>
                        <foro>Foro Central Cível</foro>
                        <valor_causa unidade_monetaria="R$">76.614,65</valor_causa>
                        <observacao>Juiz: Tiago Henriques Papaterra Limongi</observacao>
                        <advogados>
                            <advogado parte="Casa Anglo Brasileira S.a">Dacier Martins de Almeida</advogado>
                        </advogados>
                        <andamentos>
                            <andamento>
                                <descricao>Expedição de documento M.L.J. (GUIA)</descricao>
                                <data hash="f27ec0f808e9ef24d681bfc601e5be83" format="d/m/Y">23/01/2018</data>
                                <instancia>1</instancia>
                                <acao>Recuperação judicial e Falência</acao>
                                <classe>Falência de Empresários, Sociedades Empresáriais, Microempresas e Empresas de Pequeno Porte</classe>
                            </andamento>
                        </andamentos>
                        <partes>
                            <parte tipo="Requerente">.</parte>
                            <parte tipo="Requerido">Casa Anglo Brasileira S.a</parte>
                            <parte tipo="Requerido">Mappin Telecomunicações Ltda</parte>
                        </partes>
                        <tags>
                            <tag tipo="andamentos">ativo</tag>
                        </tags>
                    </processo>
*/
            
            var doc = XDocument.Load("processo.xml");
/*
            var processos = from processo in doc.Root.Elements("processo")
                            where processo.Element("observacao").Value.Contains("Limongi")
                            select processo;
*/
//where processo.Element("numero_processo").Value.Contains("9")
            IEnumerable<string> tribunais = from processo in doc.Root.Elements("processo")
                select (string)processo.Attribute("filter");

            foreach (string s in tribunais)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("End!");
        }
    }
}
