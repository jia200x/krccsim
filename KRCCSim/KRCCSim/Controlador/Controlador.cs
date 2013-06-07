using System;
using System.Collections.Generic;

namespace NSControlador
{
	public class Controlador
	{
		private double T;
		public double T_simulacion
		{
			get{return T;}
		}
		private double t_max;
        internal List<Evento> eventos;

		public Controlador (double t_max)
		{
			this.T = 0;
            this.t_max = t_max;
			eventos = new List<Evento>();
		}
		
		public void Run()
		{
			while(T < t_max)
			{
                eventos.Sort(Ordenar);
				this.T = eventos[0].tiempo_cambio;
                eventos[0].realizar_cambio();
				//eventos.RemoveAt(0);
				Console.WriteLine(T);
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