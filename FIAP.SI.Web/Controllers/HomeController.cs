using FIAP.SI.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIAP.SI.Web.Controllers
{
    public class HomeController : Controller
    {
        private const Int32 KEYSIZE = 1024;

        public static string _publicKey;
        public static string _publicAndPrivateKey;

        public static string _publicServerKey;

        static HomeController()
        {
            AsymmetricEncryption.GenerateKeys(KEYSIZE, out _publicKey, out _publicAndPrivateKey);
        }

        public ActionResult Index()
        {
            ViewBag.PublicKey = _publicKey;

            return View();
        }


        public JsonResult Teste(String ag, String ky)
        {
            _publicServerKey = ky;

            return Json(new { Ag = EncryptToServer(ag) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ValidateInput(false)]
        public JsonResult HandShake(String ky)
        {
            _publicServerKey = ky;

            return Json(new { PK = _publicKey }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Crypt(String ag, String cc, String pw)
        {
            return Json(new { Ag = EncryptToServer(ag), Cc = EncryptToServer(cc), Pw = EncryptToServer(pw) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        String Encrypt(String text)
        {
            String encrypted = AsymmetricEncryption.EncryptText(text, KEYSIZE, _publicKey);

            return encrypted;
        }

        String EncryptToServer(String text)
        {
            String encrypted = AsymmetricEncryption.EncryptText(text, KEYSIZE, _publicServerKey);

            return encrypted;
        }

        String Decrypt(String text)
        {
            String encrypted = AsymmetricEncryption.DecryptText(text, KEYSIZE, _publicAndPrivateKey);

            return encrypted;
        }
    }
}