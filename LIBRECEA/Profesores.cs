using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    class Profesores
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int sec { get; set; }
        public Profesores() { }

        public Profesores(int pId, string pNombre, string pApellido, int pSec)
        {
            this.id = pId;
            this.nombre = pNombre;
            this.apellido = pApellido;
            this.sec = pSec;
        }
    }
}

