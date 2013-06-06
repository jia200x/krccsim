using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komatsu
{
    class Program
    {
        public static List<Faena> faenas = new List<Faena>();
        public static List<Componente> componentes = new List<Componente>();

        static void Main(string[] args)
        {
            double tiempo = 0;
            double fin = 100;

            Faena faena = new Faena();
            faena.nombre = "Radomiro Tomic";
            Camion camion = new Camion(Camion.Clase.C730E, 0,faena);

            Temperature_Card t = new Temperature_Card(faena, camion);
            Power_Converter p = new Power_Converter(faena, camion);
            FDP_Panel f = new FDP_Panel(faena, camion);

            t.tiempo_cambio = 3;
            p.tiempo_cambio = 8;
            f.tiempo_cambio = 4;

            camion.componentes.Add(t);
            camion.componentes.Add(p);
            camion.componentes.Add(f);

            faena.Agregar_Camion(camion);
            faena.cambio_componente += new Action<Componente,Componente>(Cambio_compontente);

            foreach (Componente i in camion.componentes)
                componentes.Add(i);

            while (tiempo < fin)
            {
                componentes.Sort(ordenar);
                tiempo = componentes[0].tiempo_cambio;
                componentes[0].realizar_cambio(tiempo);
            }



        }

        public static int ordenar(Elemento x, Elemento y)
        {
            if (x.tiempo_cambio < y.tiempo_cambio)
                return -1;

            else if (x.tiempo_cambio > y.tiempo_cambio)
                return 1;

            else
                return 0;
        }

        public static void Cambio_compontente(Componente viejo, Componente nuevo)
        {
            componentes.Remove(viejo);
            componentes.Add(nuevo);
        }

    }
}
