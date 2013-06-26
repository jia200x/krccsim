using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;
using RNG;

namespace KRCCSim
{
    public class Componente:Evento
    {
        private Camion camion;
		private double[] tasas_falla;
		public string tipo_componente;
		public override void realizar_cambio()
		{
			//En este punto se puede preguntar si el camion sigue vivo o no
			if (camion.muerto == false)
			{
				camion.reemplazar_componente(this);
			}
		}
		public Componente(Controlador c, Camion camion, string tipo_componente, double[] tasas_falla)
		{
			this.c = c;
			this.camion = camion;
			this.tipo_componente = tipo_componente;
			this.tasas_falla = tasas_falla;
			//Console.WriteLine ("Se ha generado el siguiente tiempo de falla para el componente {0} en {1}",tipo_componente,30);
			Console.WriteLine (generar_tiempo_de_falla());
			Console.WriteLine("ASD");
			generar_siguiente_tiempo(generar_tiempo_de_falla());
		}
		private double generar_tiempo_de_falla()
		{
			//La falla depende de la edad del camión, del tipo de camión, de la faena y del componente en si.
			//Toda esa información es rescatable dado que el componente sabe en que camión está.
			//El componente tiene un arreglo de una dimensión que indica la tasa de falla beta con respecto a la edad del camión
			
			//Se tiene la varianza y la media en este punto
			//INCLUIR EL PONDERADOR DE LA FAENA!!
			int edad_camion = (int)camion.edad;
			if(edad_camion > 20) edad_camion = 20;
			double media_tiempos_anual = 1/tasas_falla[edad_camion+1];
			double varianza = tasas_falla[0]*media_tiempos_anual;
			
			double a = -media_tiempos_anual*(varianza+media_tiempos_anual*media_tiempos_anual-media_tiempos_anual)/varianza;
			double b = (varianza+media_tiempos_anual*media_tiempos_anual-media_tiempos_anual)*(media_tiempos_anual-1)/varianza;
			Console.WriteLine ("a={0}, b={1}",a,b);
			return (double) (24*365)*RNGen.Beta(a,b);
		}
    }
}
