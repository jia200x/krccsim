using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KRCCSim
{
	//Singleton Pattern with static initialization!!
	public sealed class Input
	{
		private static readonly Input instancia = new Input();
        public static Dictionary<string, Dictionary<string[], double[]>> tasa_falla_componentes; //string=[camion,componente] double[varianza,esperanzas año i]
        public static Dictionary<string[], double> componentes_por_camion; //string=[camion,componente] double es la cantidad
        public static Dictionary<string, string> reemplazo; //tiene la info de que camion se reemplaza por cual
        public static Dictionary<string[], double[]> tiempo_vida_camion; //string=[faena,camion], double=[hrs restantes, hrs trabajo anuales]
        public static Dictionary<string[], double> probabilidad_envio; //string=[faena,componente], double = probabilidad de envio a KRCC
        public static Dictionary<string, double> mortalidad; //tiene la info de la tasa de mortalidad de un componente
        public static Dictionary<string, double> ponderadores; //tiene la info del ponderador usado para cada faena
        public static Dictionary<string[], double[]> ingresos_programados; //string=[faena,camion], double=[hrs restantes, hrs trabajo anuales,hora ingreso] 
        
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

        public static void Inicializar(string ruta_faenas, string ruta_probabilidad_envio, string ruta_ingresos_programados, string ruta_reemplazos,
            string ruta_mortalidad, string ruta_componentes, string ruta_ponderadores, string ruta_componentes_camion)
        {
            reemplazo = gen_reemplazos(ruta_reemplazos);
            ponderadores = gen_ponderadores(ruta_ponderadores);
            tiempo_vida_camion = gen_t_vida(ruta_faenas);
            ingresos_programados = gen_ingreso_programado(ruta_ingresos_programados);
            mortalidad = gen_mortalidad(ruta_mortalidad);
            probabilidad_envio = gen_p_envio(ruta_probabilidad_envio);
            componentes_por_camion = gen_componentes_camion(ruta_componentes_camion);
        }



		private static Dictionary<string, string> gen_reemplazos(string ruta_reemplazos)
		{
			//Leer CSV
            Dictionary<string, string> r = new Dictionary<string, string>();

            string [] input_file = File.ReadAllLines(ruta_reemplazos);
            for (int i=0;i<input_file.Length;i++)
            {
                string[] original_cambio = input_file[i].Split(',');
                if (i > 0)
                {
                    r.Add(original_cambio[0], original_cambio[1]);
                }
            }
			return r;
		}

        private static Dictionary<string, double> gen_ponderadores(string ruta_ponderadores)
        {
            Dictionary<string, double> r = new Dictionary<string, double>();
            string[] input_file = File.ReadAllLines(ruta_ponderadores);
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] faena_ponderador = input_file[i].Split(',');
                if (i > 0)
                {
                    r.Add(faena_ponderador[0], double.Parse(faena_ponderador[1], System.Globalization.CultureInfo.InvariantCulture));
                }
            }
            return r;
        }

		private static Dictionary<string[], double[]> gen_t_vida(string ruta_faenas)
		{
            Dictionary<string[], double[]> r = new Dictionary<string[], double[]>();
            string[] input_file = File.ReadAllLines(ruta_faenas);
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                if (i > 0)
                {
                    string[] aux_string = new string[2];
                    double[] aux_double = new double[2];

                    aux_string[0] = datos_entrada[0];
                    aux_string[1] = datos_entrada[1];

                    aux_double[0] = double.Parse(datos_entrada[2], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[1] = double.Parse(datos_entrada[3], System.Globalization.CultureInfo.InvariantCulture);

                    r.Add(aux_string,aux_double);
                }

            }
            return r;
		}

        private static Dictionary<string[], double[]> gen_ingreso_programado(string ruta_faenas)
        {
            Dictionary<string[], double[]> r = new Dictionary<string[], double[]>();
            string[] input_file = File.ReadAllLines(ruta_faenas);
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                if (i > 0)
                {
                    string[] aux_string = new string[2];
                    double[] aux_double = new double[3];

                    aux_string[0] = datos_entrada[0];
                    aux_string[1] = datos_entrada[1];

                    aux_double[0] = double.Parse(datos_entrada[2], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[1] = double.Parse(datos_entrada[3], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[2] = double.Parse(datos_entrada[4], System.Globalization.CultureInfo.InvariantCulture);

                    r.Add(aux_string, aux_double);
                }

            }
            return r;
        }

        private static Dictionary<string, double> gen_mortalidad(string ruta_mortalidad)
        {
            Dictionary<string, double> r = new Dictionary<string, double>();
            string[] input_file = File.ReadAllLines(ruta_mortalidad);
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] faena_ponderador = input_file[i].Split(',');
                if (i > 0)
                {
                    r.Add(faena_ponderador[0], double.Parse(faena_ponderador[1], System.Globalization.CultureInfo.InvariantCulture));
                }
            }
            return r;
        }

		private static Dictionary<string[], double> gen_p_envio(string ruta_probabilidad_envio)
		{
            Dictionary<string[], double> r = new Dictionary<string[], double>();
            string[] input_file = File.ReadAllLines(ruta_probabilidad_envio);
            string[] faenas = null;
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                

                if (i > 0)
                {
                    for (int j = 1; j < faenas.Length; j++)
                    {
                        string[] aux_string = new string[2];

                        aux_string[0] = faenas[j];
                        aux_string[1] = datos_entrada[0];

                        double aux_double = double.Parse(datos_entrada[j], System.Globalization.CultureInfo.InvariantCulture);

                        r.Add(aux_string, aux_double);
                    }
                }
                else
                {
                    faenas = datos_entrada;
                }

            }
            return r;
		}

        private static Dictionary<string[], double> gen_componentes_camion(string ruta_componentes_camion)
        {
            Dictionary<string[], double> r = new Dictionary<string[], double>();
            string[] input_file = File.ReadAllLines(ruta_componentes_camion);
            string[] faenas = null;
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');


                if (i > 0)
                {
                    for (int j = 1; j < faenas.Length; j++)
                    {
                        string[] aux_string = new string[2];

                        aux_string[0] = faenas[j];
                        aux_string[1] = datos_entrada[0];

                        double aux_double = double.Parse(datos_entrada[j], System.Globalization.CultureInfo.InvariantCulture);

                        r.Add(aux_string, aux_double);
                    }
                }
                else
                {
                    faenas = datos_entrada;
                }

            }
            return r;
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

