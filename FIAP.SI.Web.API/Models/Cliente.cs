using System;

namespace FIAP.SI.Web.API.Models
{
    public class Cliente
    {
        public Int32  Agencia { get; set; }

        public Int32  Conta   { get; set; }

        public String Nome    { get; set; }

        public String Senha   { get; set; }

        public static Cliente Consultar(Int32 ag, Int32 cc, String pw) {
           return new Cliente { Agencia = ag, Conta = cc, Senha = pw, Nome = "Teste" } ;
        }
    }
}