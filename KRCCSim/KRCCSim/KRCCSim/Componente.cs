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
			//En este punto se puede preguntar si el camion sigue vivo o no
			camion.reemplazar_componente(this, new Componente(c,camion));
		}
		public Componente(Controlador c, Camion camion)
		{
			this.c = c;
			this.camion = camion;
			generar_siguiente_tiempo(generar_tiempo_de_falla());
		}
		private double generar_tiempo_de_falla()
		{
			//La falla depende de la edad del camión, del tipo de camión, de la faena y del componente en si.
			//Toda esa información es rescatable dado que el componente sabe en que camión está.
			return (double) 30;
		}
    }
}
