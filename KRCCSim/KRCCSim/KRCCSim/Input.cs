using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRCCSim
{
	//Singleton Pattern with static initialization!!
	public sealed class Input
	{
		private static readonly Input instancia = new Input();
		private static readonly Dictionary<string, Dictionary<string, double[]>> datos = leer_xls();
		private Input ()
		{
			
		}
		public static Input Instancia
		{
			get
			{
				return instancia;
			}
		}
		/// <summary>
		/// Aqui va la magia de leer el XLS
		/// </summary>
		private static Dictionary<string, Dictionary<string, double[]>> leer_xls()
		{
			//Datos de prueba
			//Se crean las tasas de falla para el camion E903
			Dictionary<string, double[]> componente_edad_E903 = new Dictionary<string, double[]>();
			double [] fe_E903_T104 = new double[] {2,2,1.8, 1.5};
			componente_edad_E903.Add ("Tarjeta 104",fe_E903_T104);
			double [] fe_E903_parrilla = new double[] {3,2,7, 0.1,0.7};
			componente_edad_E903.Add ("Parrilla", fe_E903_parrilla);
			Dictionary<string, Dictionary<string, double[]>> camiones = new Dictionary<string, Dictionary<string, double[]>>();
			camiones.Add ("E903",componente_edad_E903);
			return camiones;
		}
		
	}
}

