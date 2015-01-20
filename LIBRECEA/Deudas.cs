using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    class Deudas
    {
        public string MATERIAL { get; set; }
        public Decimal MONTO { get; set; }
        public DateTime FECHA { get; set; }
        
        public Deudas() { }

        public Deudas(string pIdMat, Decimal pMonto, DateTime pFec)
        {
            this.MATERIAL = pIdMat;
            this.MONTO = pMonto;
            this.FECHA = pFec;

        }

    }
}
