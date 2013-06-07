using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;

namespace KRCCSim
{
	public class Faena:Evento
	{
        public List<Camion> camiones;
		public List<Componente> batch;
		public Faena(Controlador c)
		{
			this.c = c;
			this.camiones = new List<Camion>();
			this.batch = new List<Componente>();
		}
		public void agregar_camion(Camion camion)
		{
			camiones.Add(camion);
		}
		public override void realizar_cambio()
		{
		}
		public void agregar_a_batch(Componente defectuoso)
		{
			this.batch.Add(defectuoso);
			Console.WriteLine("Ahora hay {0} componentes en el batch",batch.Count);
		}
		
	}
}
