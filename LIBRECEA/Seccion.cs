using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    class Seccion
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public Seccion() { }

        public Seccion(int pId, string pTipo)
        {
            this.id = pId;
            this.tipo = pTipo;
        }
    }
}
