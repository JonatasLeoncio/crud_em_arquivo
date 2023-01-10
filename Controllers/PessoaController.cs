using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using crud_em_arquivo.Models;


namespace crud_em_arquivo.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpGet("api")]
        public ActionResult<List<Pessoa>> listar()
        {
            Pessoa md = new Pessoa();
            var ret = md.ListarPessoaModel();
            return ret;
        }

        [HttpPost("api")]
        public ActionResult<Pessoa> salvar([FromBody] Pessoa pessoa)
        {
            Pessoa model = new Pessoa();
            var cod = model.salvarPessoaModel(pessoa);
            return cod;
        }
    }
}
