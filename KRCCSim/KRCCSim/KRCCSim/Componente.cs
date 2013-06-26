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
        public Camion camion;
		private double[] tasas_falla;
		public string tipo_componente;
		public override void realizar_cambio()
		{
			//En este punto se puede preguntar si el camion sigue vivo o no
			if (camion.muerto == false)
			{
				camion.reemplazar_componente(this);
			}
			else
			{
				Console.WriteLine("Camion muerto");
			}
		}
		public Componente(Controlador c, Camion camion, string tipo_componente, double[] tasas_falla, bool usado=false)
		{
			this.c = c;
			this.camion = camion;
			this.tipo_componente = tipo_componente;
			this.tasas_falla = tasas_falla;
			//Si el camion esta usado, se debe obtener cuando es el punto en que falla
			//Console.WriteLine ("Se ha generado el siguiente tiempo de falla para el componente {0} en {1}",tipo_componente,30);
			if(usado == false)
			{
				double sgte_tiempo = generar_tiempo_de_falla((int)camion.edad);
				//Si el siguiente tiempo de falla es mayor a la hora en que muere el camión, no agregarlo a eventos
				if(this.c.T_simulacion+sgte_tiempo < this.camion.tiempo_muerte)
					generar_siguiente_tiempo(generar_tiempo_de_falla((int)camion.edad));
			}
			else
			{
				//Paradoja de inspección
				double horas_funcionamiento = this.camion.tiempo_inicializacion;
				double tasa_trabajo = this.camion.tasa_trabajo;
				double a_func = horas_funcionamiento/tasa_trabajo;
				int edad_camion = 0;
				double t=0;
				double r;
				while(true)
				{
					t += generar_tiempo_de_falla(edad_camion);
					r = t/(365*24);
					if(r <= a_func)
					{
						edad_camion+=(int)r;
					}
					else
					{
						double sgte_tiempo = t-a_func*(365*24);
						if(this.c.T_simulacion+sgte_tiempo < this.camion.tiempo_muerte)
							generar_siguiente_tiempo(t-a_func*(365*24));
						break;
					}
				}
			}
		}
		private double generar_tiempo_de_falla(int edad_camion)
		{
			//Genera un tiempo de falla valido... Si el camion es usado, encuentra cual es su fecha de falla
			//La falla depende de la edad del camión, del tipo de camión, de la faena y del componente en si.
			//Toda esa información es rescatable dado que el componente sabe en que camión está.
			//El componente tiene un arreglo de una dimensión que indica la tasa de falla beta con respecto a la edad del camión
			
			//Se tiene la varianza y la media en este punto
			if(edad_camion > 19) edad_camion = 19;
			double tasa_falla_promedio=0;
			for (int i=edad_camion;i<20;i++)
			{
				tasa_falla_promedio+=tasas_falla[i+1];
			}
			tasa_falla_promedio = tasa_falla_promedio/(20-edad_camion);
			if (tasa_falla_promedio == 0) return c.t_max+1;
			double media_tiempos_anual = 1/tasa_falla_promedio;
			double arcsin = RNGen.Arcsin();
			double varianza = media_tiempos_anual/tasas_falla[0];
			double b = Math.Sqrt(8*varianza);
			double a = media_tiempos_anual-b/2;
			//a+b/2=media, b^2/8=varianza
			double instancia_tiempo = this.camion.faena.ponderador*(24*365)*(a+b*arcsin);
			return instancia_tiempo;
		}
    }
}
