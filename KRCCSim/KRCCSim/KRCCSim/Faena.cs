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
		private Dictionary<string, string> rmp;
		//Ponderador de condiciones de la mina, por faena
		public double ponderador;
		public Faena(Controlador c)
		{
			this.c = c;
			this.camiones = new List<Camion>();
			this.batch = new List<Componente>();
			
			//La faena sabe su información de reemplazo de camiones. En este clase se genera el vector de cambios 
			generar_reemplazos();
			//Solo por testing, la faena se reemplazará cada 70 unidades de tiempo
			generar_siguiente_tiempo(70);
		}
		public void agregar_camion(Camion camion)
		{
			camiones.Add(camion);
		}
		public override void realizar_cambio()
		{
			//Se vacía el batch y se vuelve a agregar el siguiente tiempo en que se manda el batch
			vaciar_batch ();
			generar_siguiente_tiempo(70);
		}
		public void agregar_a_batch(Componente defectuoso)
		{
			this.batch.Add(defectuoso);
			Console.WriteLine("Ahora hay {0} componentes en el batch",batch.Count);
		}
		public void vaciar_batch()
		{
			int n_componentes = batch.Count;
			batch = new List<Componente>();
			Console.WriteLine ("Se han enviado {0} componentes a KRCC", n_componentes);
		}
		/// <summary>
		/// Genera la tabla de reemplazo de camiones para la faena
		/// </summary>
		private void generar_reemplazos()
		{
			rmp = new Dictionary<string, string>();
			rmp.Add ("E803","E903");
		}
		public string get_reemplazo(string entrada)
		{
			return rmp[entrada];
		}
	}
}
