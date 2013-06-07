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
		public Camion(Controlador c, Faena faena)
		{
			this.componentes = new List<Componente>();
			this.faena = faena;
			this.c = c;
		}
		public override void realizar_cambio()
		{
		}
		public void agregar_componente(Componente componente)
		{
			componentes.Add (componente);
		}
		public void reemplazar_componente(Componente defectuoso, Componente nuevo)
		{
			componentes.Remove(defectuoso);
			faena.agregar_a_batch(defectuoso);		
			componentes.Add (nuevo);
		}
    }
}
