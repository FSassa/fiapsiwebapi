using System;

namespace FIAP.SI.Web.API.Models
{
    public class Consolidado
    {
        public Cliente Cliente      { get; private set; }

        public Extrato Extrato      { get; private set; }

        public Decimal FaturaCartao { get; private set; }

        public Consolidado(Cliente cli)
        {
            Cliente = cli;
        }

        public void Preparar() {

            FaturaCartao = 1000m;

            Extrato      = new Extrato(Cliente);

            Extrato.Carregar();
        }
    }
}