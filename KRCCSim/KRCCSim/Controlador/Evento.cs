using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NSControlador
{
public abstract class Evento
    {
        private double _tiempo_cambio;
		public double tiempo_cambio
		{
			get{return this._tiempo_cambio;}
		}
		public Controlador c;
		public Evento()
		{
		}
        public abstract void realizar_cambio();
		protected void generar_siguiente_tiempo(double tiempo)
		{
			Debug.Write("Se genera un siguiente tiemo de falla");
			this._tiempo_cambio = c.T_simulacion+tiempo;
			c.agregar_evento(this);
		}
    }
}
