using System;
using System.Collections.Generic;

namespace KRCCSim
{
	public class Controlador
	{
		public double T;
		private double t_max;
        public List<Evento> eventos;

		public Controlador ()
		{
			this.T = 0;
			eventos = new List<Evento>();
			

			//TESTING
			this.t_max = 4000;
		}
		public void Run()
		{
			while(this.T < this.t_max)
			{
                eventos.Sort(Ordenar);
                eventos[0].realizar_cambio();
			}
		}
        private int Ordenar(Evento a, Evento b)
		{
            if (a.tiempo_cambio < b.tiempo_cambio)
                return -1;

            else if (a.tiempo_cambio > b.tiempo_cambio)
                return 1;

            else
                return 0;
		}

		public void agregar_evento(Evento evento)
		{
            eventos.Add(evento);
		}


	}
}

