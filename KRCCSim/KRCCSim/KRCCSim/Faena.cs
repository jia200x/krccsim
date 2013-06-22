﻿using System;
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
		//Ponderador de condiciones de la mina, por faena
		public double ponderador;
		public Faena(Controlador c)
		{
			this.c = c;
			this.camiones = new List<Camion>();
			this.batch = new List<Componente>();
			
			//Solo por testing, la faena se reemplazará cada 70 unidades de tiempo
			generar_siguiente_tiempo(70);
		}
		public void agregar_camion(Camion camion)
		{
			camiones.Add(camion);
		}
		public void reemplazar_camion(Camion original)
		{
			camiones.Remove(original);
			Camion camion_nuevo = new Camion(this.c, this, Input.reemplazo[original.tipo_camion]);
			agregar_camion(camion_nuevo);
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
	}
}
