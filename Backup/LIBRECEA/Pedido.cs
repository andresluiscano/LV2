using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    class Pedido
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public Double total { get; set; }
        public Double senia { get; set; }
        public Double debe { get; set; }
        public Boolean estado { get; set; }
        public DateTime fec { get; set; }
        public Boolean pagado { get; set; }
        public string desc { get; set; }
        public string mail { get; set; }


        public Pedido() { }

        public Pedido(int pId, string pNombre, Double pTotal, Double pSenia, Double pDebe, Boolean pEstado, DateTime pFec, Boolean pPagado, String pDesc, String pMail)
        {
            this.Id = pId;
            this.nombre = pNombre;
            this.total= pTotal;
            this.senia = pSenia;
            this.debe = pDebe;
            this.estado = pEstado;
            this.fec = pFec;
            this.pagado = pPagado;
            this.desc = pDesc;
            this.mail = pMail;
        }
    }
}
