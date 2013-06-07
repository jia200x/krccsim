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
			//Ok, fallo... se hace lo que corresponde y se genera otra falla
			generar_siguiente_tiempo(20);
		}
		public Componente(Controlador c)
		{
			this.c = c;
			generar_siguiente_tiempo(30);
		}
    }
}
