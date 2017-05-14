using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Collections.Generic;

namespace FIAP.SI.Web.API.Controllers
{
    public class ComSegurancaController : ApiController
    {
        private static readonly String FAKE_CONTA = "12345";

        private static readonly Dictionary<String, Models.ModeloSemSeguranca> Session
            = new Dictionary<string, Models.ModeloSemSeguranca>();

        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("api/validarComSeguranca")]
        [HttpGet]
        public IHttpActionResult Validar(String usr, String pwd)
        {
            if (String.IsNullOrWhiteSpace(usr) || String.IsNullOrWhiteSpace(pwd))
            {
                return BadRequest("Informações inválidas");
            }

            Models.ModeloSemSeguranca mdl = new Models.ModeloSemSeguranca();

            mdl.Conta = FAKE_CONTA;
            mdl.Token = Guid.NewGuid().ToString();

            Session.Add(usr, mdl);

            return Ok(new Models.ModeloSemSeguranca { Conta = Encrypt(mdl.Conta), Token = Encrypt(mdl.Token) });
        }

        [Route("api/extratoComSeguranca")]
        [HttpPost]
        public IHttpActionResult Extrato(Models.ModeloSemSeguranca mdl)
        {
            String conta = null;
            String token = null;

            if (null == mdl)
            {
                return BadRequest("Informações inválidas");
            }

            try
            {
                conta = Decrypt(mdl.Conta);
                token = Decrypt(mdl.Token);
            }
            catch
            {
                return BadRequest("Informações inválidas");
            }

            Models.ModeloSemSeguranca chaveUsuario
                = Session.Values.FirstOrDefault(v => conta.Equals(v.Conta) && token.Equals(v.Token));

            if (null == chaveUsuario)
            {
                return BadRequest("Informações inválidas");
            }

            Models.Extrato result
                = new Models.Extrato();

            result.Conta
                = chaveUsuario.Conta;

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

        String Encrypt(String text)
        {
            byte[] base64Array = Encoding.Default.GetBytes( text);
            
            return Convert.ToBase64String(base64Array.Reverse().ToArray());
        }

        String Decrypt(String text)
        {
            byte[] base64Array = Convert.FromBase64String(text);

            return Encoding.Default.GetString(base64Array.Reverse().ToArray());
        }
    }
}