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
		public static readonly Dictionary<string, Dictionary<string, double[]>> datos = leer_xls();
		public static readonly Dictionary<string, string> reemplazo = gen_reemplazos();
		public static readonly Dictionary<string, double> tiempo_vida_camion = gen_t_vida();
		public static readonly Dictionary<string, double> probabilidad_envio = gen_p_envio();
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
		private static Dictionary<string, string> gen_reemplazos()
		{
			//Leer XLS de reemplazos (o definirlos extensivamente)
			Dictionary<string, string> r = new Dictionary<string, string>();
			r.Add ("E903","E903");
			return r;
		}
		private static Dictionary<string, double> gen_t_vida()
		{
			Dictionary<string, double> t = new Dictionary<string, double>();
			t.Add ("E903", 20000);
			t.Add ("E803", 2000);
			return t;
		}
		private static Dictionary<string, double> gen_p_envio()
		{
			Dictionary<string, double> p = new Dictionary<string, double>();
			p.Add ("Tarjeta 104", 0.5);
			p.Add ("Parrilla", 1);
			return p;
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

