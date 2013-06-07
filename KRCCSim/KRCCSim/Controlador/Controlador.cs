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
			while(true)
			{
                eventos.Sort(Ordenar);
				if(eventos.Count == 0)
				{
					Console.WriteLine ("Se acabaron los eventos");
					break;
				}
				this.T = eventos[0].tiempo_cambio;
				Console.WriteLine("\nTiempo actual: {0}",T);
				if (this.T > t_max) break;
                eventos[0].realizar_cambio();
				eventos.RemoveAt(0);
			}
			Console.WriteLine("Ha finalizado la simulación");
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