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
        public static Dictionary<string, Dictionary<string, double[]>> tasa_falla_componentes; // tiene la info de la tasa de falla por componente
        public static Dictionary<string, string> reemplazo; //tiene la info de que camion se reemplaza por cual
        public static Dictionary<string, double> tiempo_vida_camion; //cuantas horas de vida la quedan al camion
        public static Dictionary<string, double> probabilidad_envio; //tiene la info de la probabilidad de envio a KRCC
        public static Dictionary<string, double> mortalidad; //tiene la info de la tasa de mortalidad de un componente
        public static Dictionary<string, double> ponderadores; //tiene la info del ponderador usado para cada faena

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

        public void Inicializar(string ruta_faenas, string ruta_probabilidad_envio, string ruta_ingresos_programados, string ruta_reemplazos,
            string ruta_mortalidad, string ruta_componentes)
        {
            reemplazo = gen_reemplazos(ruta_reemplazos);
        }



		private static Dictionary<string, string> gen_reemplazos(string a)
		{
			//Leer XLS de reemplazos (o definirlos extensivamente)
			Dictionary<string, string> r = new Dictionary<string, string>();
			r.Add ("E903","E903");
			return r;
		}
		private static Dictionary<string, double> gen_t_vida(string a)
		{
			Dictionary<string, double> t = new Dictionary<string, double>();
			t.Add ("E903", 20000);
			t.Add ("E803", 2000);
			return t;
		}
		private static Dictionary<string, double> gen_p_envio(string a)
		{
			Dictionary<string, double> p = new Dictionary<string, double>();
			p.Add ("Tarjeta 104", 0.5);
			p.Add ("Parrilla", 1);
			return p;
		}
		private static Dictionary<string, Dictionary<string, double[]>> leer_xls(string a)
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

