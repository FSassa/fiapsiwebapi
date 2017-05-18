using System;
using System.Collections.Generic;

namespace FIAP.SI.Web.API.Models
{
    public class Extrato
    {
        public Cliente Cliente { get; private set; }

        public List<ExtratoItem> Lancamentos
        { 
            get; 
            set;
        }

        public Extrato(Cliente cli)
        {
            Cliente     = cli;

            Lancamentos = new List<ExtratoItem>();
        }

        public void Carregar() {
            for (Int32 i = 10; i > 0; i--)
            {
                ExtratoItem item
                    = new ExtratoItem();

                item.Data      = DateTime.Today.AddDays(-i);
                item.Descricao = String.Format("Lançamento {0}", 10 - i);

                Int32 aleatorio
                    = new Random(i).Next(1, i);

                item.Valor =  1000m * aleatorio * (0 == aleatorio % 3 ? -1: 1);

                Lancamentos.Add(item);
            }
        }

        public class ExtratoItem
        {
            public DateTime Data      { get; set; }
            public String   Descricao { get; set; }
            public Decimal  Valor     { get; set; }
        }
    }
}