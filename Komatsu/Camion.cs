using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komatsu
{
    public class Camion : Elemento
    {
        public enum Clase { C730E, C830E17AC, C830E1AC, C830EAC, C830EDC, C930E1 , C930E2, C930E2SE, C930E3, C930E3SE, C930E4, C930E4AT, C930E4SE, C960E1, C960E2, C960E2K };
        public List<Componente> componentes = new List<Componente>();
        public double antiguedad;
        public Clase clase;
        public Faena faena;

        public Camion()
        {
        }

        public Camion(Clase clase, double antiguedad, Faena faena)
        {
            this.clase = clase;
            this.antiguedad = antiguedad;
            this.faena = faena;
        }

        public override void realizar_cambio(double tiempo)
        {
            throw new NotImplementedException();
        }
    }
}
