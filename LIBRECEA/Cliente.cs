using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    public class Cliente
    {
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public Cliente() { }

        public Cliente(int pId, string pNombre, string pApellido)
        {
            this.ID = pId;
            this.NOMBRE = pNombre;
            this.APELLIDO = pApellido;
        }
    }
}
