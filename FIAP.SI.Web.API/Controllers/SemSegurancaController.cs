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
        [Route("api/validarSemSeguranca")]
        [HttpGet]
        public IHttpActionResult Validar(Int32 ag, Int32 cc, String pw)
        {
            if (0 == ag || 0 == cc || String.IsNullOrWhiteSpace(pw))
            {
                return BadRequest("Informações inválidas");
            }

            Models.Cliente cli
                = Models.Cliente.Consultar(ag, cc, pw);

            return Ok(cli);
        }

        [Route("api/extratoSemSeguranca")]
        [HttpPost]
        public IHttpActionResult Extrato(Models.Cliente cli)
        {
            if (null == cli)
            {
                return BadRequest("Informações inválidas");
            }

            Models.Consolidado consolidacao
                = new Models.Consolidado(cli);

            try
            {
                consolidacao.Preparar();

                return Ok(consolidacao);
            }
            catch (Exception uhEx)
            {
                return BadRequest(uhEx.Message);
            }
        }
    }
}