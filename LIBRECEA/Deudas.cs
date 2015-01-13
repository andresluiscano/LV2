using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    class Deudas
    {
        public string id_material { get; set; }
        public Double monto { get; set; }
        public DateTime fec { get; set; }
        
        public Deudas() { }

        public Deudas(string pIdMat, Double pMonto, DateTime pFec)
        {
            this.id_material = pIdMat;
            this.monto = pMonto;
            this.fec = pFec;

        }

    }
}
