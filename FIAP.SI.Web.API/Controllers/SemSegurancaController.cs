using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FIAP.SI.Web.API.Controllers
{
    public class SemSegurancaController : ApiController
    {
        private static readonly String FAKE_CONTA = "12345";
        private static readonly String FAKE_TOKEN = "852";

        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("api/validarSemSeguranca")]
        [HttpGet]
        public IHttpActionResult Validar(String usr, String pwd)
        {
            if (String.IsNullOrWhiteSpace(usr) || String.IsNullOrWhiteSpace(pwd))
            {
                return BadRequest("Informações inválidas");
            }

            return Ok(new Models.ModeloSemSeguranca { Conta = FAKE_CONTA, Token = FAKE_TOKEN });
        }

        [Route("api/extratoSemSeguranca")]
        [HttpPost]
        public IHttpActionResult Extrato(Models.ModeloSemSeguranca mdl)
        {
            if (null == mdl)
            {
                return BadRequest("Informações inválidas");
            }

            Models.Extrato result
                = new Models.Extrato();

            result.Conta
                = FAKE_CONTA;

            for (Int32 i = 0; i < 10; i++)
            {
                Models.Extrato.ExtratoItem item
                    = new Models.Extrato.ExtratoItem();

                item.Data = DateTime.Today.AddDays(-i);
                item.Descricao = String.Format("Lançamento {0}", i);
                item.Valor = i * 1000m + 1000m;

                result.Lancamentos.Add(item);
            }

            return Ok(result);
        }
    }
}