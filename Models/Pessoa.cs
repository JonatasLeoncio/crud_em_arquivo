using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using System.Linq;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection.Metadata.Ecma335;

namespace crud_em_arquivo.Models
{
    class Banco
    {
        public int autoIncremente = 0;
        public List<Pessoa> pessoaList;
    }
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string tel { get; set; }
        public string sexo { get; set; }

        private const string V = "";
        private string caminho = Path.GetFullPath(@"Models\dadosPessoa.json");

        public List<Pessoa> ListarPessoaModel()
        {
           
            if (!File.Exists(caminho)|| new FileInfo(caminho).Length == 0)
            {
                //File.Create(caminho).Close();
                using var file = File.AppendText(caminho);
                file.WriteLine("{\r\n  \"autoIncremente\": 1,\r\n  \"pessoaList\": []\r\n}");
                file.Close();
            }                          

            var json = File.ReadAllText(caminho);
            var banco = JsonConvert.DeserializeObject<Banco>(json);
          
            List<Pessoa> listaPessoa = banco.pessoaList.ToList();

            return listaPessoa;           
        }
        public bool RescreverArquivo(List<Pessoa> pessoaList, bool autoIncremente)
        {
            Banco banco = new Banco();
            if (autoIncremente)
            {               
                banco.autoIncremente = buscarAutoIncremente()+1;
            }
            else
            {
                banco.autoIncremente = buscarAutoIncremente();
            }          
           
            banco.pessoaList = pessoaList;
            var json = JsonConvert.SerializeObject(banco, Formatting.Indented);
            File.WriteAllText(caminho, json);
            return true;
        }
        public int buscarAutoIncremente()
        {
            var json = File.ReadAllText(caminho);
            var banco = JsonConvert.DeserializeObject<Banco>(json);

            int idAutoIncremente = banco.autoIncremente;
            Console.WriteLine("ttttt="+idAutoIncremente);
            return idAutoIncremente;
        }

        public Pessoa salvarPessoaModel(Pessoa pessoa)
        {            
            List<Pessoa> listaPessoa = new List<Pessoa>();
            listaPessoa = ListarPessoaModel();
            pessoa.Id = buscarAutoIncremente();
            listaPessoa.Add(pessoa);
            RescreverArquivo(listaPessoa,true);
            return pessoa;
        }
    }
}
