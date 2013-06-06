using System;
using System.Collections.Generic;

namespace KRCCSim
{
	public class Controlador
	{
		public double T;
		private double t_max; 
		public List<Tuple> eventos;
		public Controlador ()
		{
			this.T = 0;
			eventos = new List<Tuple>();
			
			//TESTING
			this.t_max = 4000;
		}
		public void Run()
		{
			while(this.T < this.t_max)
			{
				
			}
		}
		private int Ordenar(Tuple a, Tuple b)
		{
			throw NotImplementedException();
		}

		public void agregar_evento(double tiempo, Void funcion)
		{
			throw NotImplementedException();
		}
	}
}

