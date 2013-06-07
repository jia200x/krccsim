using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;

namespace KRCCSim
{
    public class Camion:Evento
    {
        public List<Componente> componentes;
        public double tiempo_actual, tiempo_max;
        public Faena faena;
		public Camion(Controlador c)
		{
			this.componentes = new List<Componente>();
			this.c = c;
		}
		public override void realizar_cambio()
		{
		}
		public void agregar_componente(Componente componente)
		{
			componentes.Add (componente);
		}
    }
}
