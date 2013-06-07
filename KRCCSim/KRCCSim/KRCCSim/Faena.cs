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
		public Faena(Controlador c)
		{
			this.c = c;
			this.camiones = new List<Camion>();
		}
		public void agregar_camion(Camion camion)
		{
			camiones.Add(camion);
		}
		public override void realizar_cambio()
		{
		}
		
	}
}
