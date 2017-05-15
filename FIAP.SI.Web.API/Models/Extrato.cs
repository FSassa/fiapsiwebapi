using System;
using System.Collections.Generic;

namespace FIAP.SI.Web.API.Models
{
    public class Extrato
    {
        public String Conta
        {
            get;
            set;
        }
        public List<ExtratoItem> Lancamentos
        {
            get;
            set;
        }

        public Extrato()
        {
            Lancamentos = new List<ExtratoItem>();
        }

        public class ExtratoItem
        {
            public DateTime Data
            {
                get;
                set;
            }
            public String Descricao
            {
                get;
                set;
            }
            public Decimal Valor
            {
                get;
                set;
            }
        }
    }
}