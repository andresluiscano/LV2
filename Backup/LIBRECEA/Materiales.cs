using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIBRECEA
{
    public class Materiales
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public Double precio { get; set; }
        
        public Materiales() { }

        public Materiales(int pId, string pNombre, Double pPrecio)
        {
            this.Id = pId;
            this.nombre = pNombre;
            this.precio = pPrecio;

        }
    }
}
