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
		public int n_eventos
		{
			get{return eventos.Count;}
		}
		public double t_max;
        internal List<Evento> eventos;

		public Controlador (double t_max)
		{
			this.T = 0;
            this.t_max = t_max;
			eventos = new List<Evento>();
		}
		
		public void Run()
		{
			double last_T=0;
			while(true)
			{
				//La nueva optimizacion implementa un arbol binario
                //eventos.Sort(Ordenar);
				if(eventos.Count == 0)
				{
					Console.WriteLine ("Se acabaron los eventos");
					break;
				}
				this.T = eventos[0].tiempo_cambio;
				if (last_T+500 < this.T)
				{
					Console.WriteLine("\nTiempo actual: {0}",T);
					last_T = this.T;
				}
				int c1 = eventos.Count;
				if (this.T > t_max) break;
                eventos[0].realizar_cambio();
				if (eventos.Count-c1 == 0)
				{
					//Console.WriteLine("Paso");
				}
				eventos.RemoveAt(0);
				if (eventos.Count-c1 != 0)
				{
					//Console.WriteLine("Paso");
				}
			}
			Console.WriteLine("Ha finalizado la simulación");
		}
		public void agregar_evento(Evento evento)
		{
			//Se busca la posición en que se debe agregar el evento para respetar el árbol binario
			if(eventos.Count == 0)
			{
				eventos.Add(evento);
			}
			else if (eventos.Count == 1)
			{
				if (evento.tiempo_cambio > eventos[0].tiempo_cambio)
				{
					eventos.Add (evento);
				}
				else
				{
					eventos.Insert(0,evento);
				}
			}
			else
			{
				//Búsqueda binaria
				int p1 = 0;
				int p2 = eventos.Count-1;
				while(true)
				{
					if ((p2-p1) == 1)
					{
						if(evento.tiempo_cambio < eventos[p1].tiempo_cambio)
						{
							eventos.Insert(p1,evento);
						}
						else if (evento.tiempo_cambio > eventos[p2].tiempo_cambio)
						{
							eventos.Insert(p2+1,evento);
						}
						else
						{
							eventos.Insert(p1+1, evento);
						}
						break;
					}
					//Se busca el punto medio
					int pm = (p1+p2)/2;
					if (evento.tiempo_cambio > eventos[pm].tiempo_cambio)
					{
						p1 = pm;
					}
					else
					{
						p2 = pm;
					}
				}
			}
		}


	}
}