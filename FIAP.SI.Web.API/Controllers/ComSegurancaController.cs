using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Collections.Generic;

using System.Security.Cryptography;
using FIAP.SI.Crypt;

namespace FIAP.SI.Web.API.Controllers
{
    public class ComSegurancaController : ApiController
    {
        private const Int32 KEYSIZE = 1024;

        public static string _publicKey;
        public static string _publicAndPrivateKey;

        public static string _publicClientKey;

        static ComSegurancaController()
        {
            AsymmetricEncryption.GenerateKeys(KEYSIZE, out _publicKey, out _publicAndPrivateKey);
        }

        [Route("api/handShake")]
        [HttpGet]
        public IHttpActionResult HandShake(String ky)
        {
            _publicClientKey = ky;

            return Ok(new { PK = _publicKey });
        }

        [Route("api/validarComSeguranca")]
        [HttpGet]
        public IHttpActionResult Validar(String ag, String cc, String pw)
        {
            if (String.IsNullOrWhiteSpace(ag) || String.IsNullOrWhiteSpace(cc) || String.IsNullOrWhiteSpace(pw))
            {
                return BadRequest("Informações inválidas");
            }

            String agDecrypt = Decrypt(ag);
            String ccDecrypt = Decrypt(cc);
            String pwDecrypt = Decrypt(pw);

            Models.Cliente cli
                = Models.Cliente.Consultar(Int32.Parse(agDecrypt), Int32.Parse(ccDecrypt), pwDecrypt);

            var cliCrypt = new { Agencia = EncryptToClient(cli.Agencia.ToString()), Conta = EncryptToClient(cli.Conta.ToString()), Senha = EncryptToClient(cli.Senha), Nome = EncryptToClient(cli.Nome) };

            return Ok(cliCrypt);
        }

        [Route("api/extratoComSeguranca")]
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

        String Encrypt(String text)
        {
            String encrypted = AsymmetricEncryption.EncryptText(text, KEYSIZE, _publicKey);

            return encrypted;
        }

        String EncryptToClient(String text)
        {
            String encrypted = AsymmetricEncryption.EncryptText(text, KEYSIZE, _publicClientKey);

            return encrypted;
        }

        String Decrypt(String text)
        {
            String encrypted = AsymmetricEncryption.DecryptText(text, KEYSIZE, _publicAndPrivateKey);

            return encrypted;
        }
    }
}