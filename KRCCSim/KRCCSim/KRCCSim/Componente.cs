using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;

namespace KRCCSim
{
    public class Componente:Evento
    {
        private Camion camion;
		public override void realizar_cambio()
		{
			camion.reemplazar_componente(this, new Componente(c,camion));
		}
		public Componente(Controlador c, Camion camion)
		{
			this.c = c;
			this.camion = camion;
			generar_siguiente_tiempo(30);
		}
    }
}
