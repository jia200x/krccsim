using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;
using RNG;
using Registro;

namespace KRCCSim
{
	public class Faena:Evento
	{
        public string Nombre;
        public List<Camion> camiones;
		public List<Componente> batch;
		//Ponderador de condiciones de la mina, por faena
		public double ponderador;
		public Faena(Controlador c, string Nombre)
		{
			this.c = c;
			this.camiones = new List<Camion>();
			this.batch = new List<Componente>();
			this.Nombre = Nombre;
			this.ponderador = Input.ponderadores[this.Nombre];
			
			//Se agregan los camiones por faena...
			try
			{
				foreach (var dato in Input.tiempo_vida_camion[this.Nombre])
				{
					Camion cam = new Camion(this.c, this, dato.arr_string[0], new double[]{dato.arr_double[1], dato.arr_double[0], dato.arr_double[2]});
				}
			}
			catch
			{
			}
			
		}
		public void agregar_camion(Camion camion)
		{
			camiones.Add(camion);
		}
		public void reemplazar_camion(Camion original)
		{
			camiones.Remove(original);
			//CAMBIAR!!
			Camion camion_nuevo = new Camion(this.c, this, Input.reemplazo[original.tipo_camion],new double[]{100000,5000,0});
			agregar_camion(camion_nuevo);
		}
		public override void realizar_cambio()
		{
			/*//Se vacía el batch y se vuelve a agregar el siguiente tiempo en que se manda el batch
			vaciar_batch ();
			generar_siguiente_tiempo(24*365);*/
		}
		public void agregar_a_batch(Componente defectuoso)
		{
			this.batch.Add(defectuoso);
			//Console.WriteLine("Ahora hay {0} componentes en el batch de {1}",batch.Count, this.Nombre);
		}
		public void vaciar_batch()
		{
			//int n_componentes = batch.Count;
			int status;
			foreach(Componente comp in batch)
			{
				if (Input.mortalidad[comp.tipo_componente] <= RNGen.Unif(0,1))
				{
					status = 0;
				}
				else
				{
					status = 1;
				}
				//Reg.agregar_registro(this.c.T_simulacion,this.Nombre,comp.camion.tipo_camion,comp.tipo_componente,status);
			}
			batch = new List<Componente>();
			//Console.WriteLine ("Se han enviado {0} componentes a KRCC", n_componentes);
		}
		/// <summary>
		/// Genera la tabla de reemplazo de camiones para la faena
		/// </summary>
	}
}
