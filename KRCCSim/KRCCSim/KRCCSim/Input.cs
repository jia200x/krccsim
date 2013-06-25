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
        public static Dictionary<string, Dictionary<string,double[]>> tasa_falla_componentes; //string externo camion, interno componente double[varianza,esperanzas año i]
        public static Dictionary<string, Dictionary<string,int>> componentes_por_camion; //string externo camion interno componente double es la cantidad
        public static Dictionary<string, string> reemplazo; //tiene la info de que camion se reemplaza por cual
        public static Dictionary<string, Dato[]> tiempo_vida_camion; //string externo faena interno camion, double=[hrs restantes, hrs trabajo anuales,hrs trabajadas]
        public static Dictionary<string, Dictionary<string,double>> probabilidad_envio; //string interno faena, externo componente, double = probabilidad de envio a KRCC
        public static Dictionary<string, double> mortalidad; //tiene la info de la tasa de mortalidad de un componente
        public static Dictionary<string, double> ponderadores; //tiene la info del ponderador usado para cada faena
        public static Dictionary<string, Dato[]> ingresos_programados; //string externo faena, interno camion, double=[hrs restantes, hrs trabajo anuales,hora ingreso] 

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
            string ruta_mortalidad, string ruta_falla_componentes, string ruta_ponderadores, string ruta_componentes_camion)
        {
            reemplazo = gen_reemplazos(ruta_reemplazos);
            ponderadores = gen_ponderadores(ruta_ponderadores);
            tiempo_vida_camion = gen_t_vida(ruta_faenas);
            ingresos_programados = gen_ingreso_programado(ruta_ingresos_programados);
            mortalidad = gen_mortalidad(ruta_mortalidad);
            probabilidad_envio = gen_p_envio(ruta_probabilidad_envio);
            componentes_por_camion = gen_componentes_camion(ruta_componentes_camion);
            tasa_falla_componentes = gen_tasa_falla_componente(ruta_falla_componentes);
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

        private static Dictionary<string, Dato[]> gen_t_vida(string ruta_faenas)
		{
            Dictionary<string, Dato[]> externo = new Dictionary<string, Dato[]>();
            string ultima_dato_faena = null;
            List<Dato> lista = new List<Dato>();

            string[] input_file = File.ReadAllLines(ruta_faenas);

            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                if (i > 0)
                {
                    if (ultima_dato_faena != datos_entrada[0] && ultima_dato_faena != null)
                    {
                        externo.Add(ultima_dato_faena,lista.ToArray());
                        lista.Clear();
                    }

                    string[] aux_string = new string[1];
                    double[] aux_double = new double[3];

                    aux_string[0] = datos_entrada[1];

                    aux_double[0] = double.Parse(datos_entrada[2], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[1] = double.Parse(datos_entrada[3], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[2] = double.Parse(datos_entrada[4], System.Globalization.CultureInfo.InvariantCulture);

                    ultima_dato_faena = datos_entrada[0];
                    

                    lista.Add(new Dato(aux_string,aux_double));
                }

            }
            externo.Add(ultima_dato_faena, lista.ToArray());
            return externo;
		}

        private static Dictionary<string, Dato[]> gen_ingreso_programado(string ruta_faenas)
        {
            Dictionary<string, Dato[]> externo = new Dictionary<string, Dato[]>();
            List<Dato> lista = new List<Dato>();
            string[] input_file = File.ReadAllLines(ruta_faenas);

            string ultima_dato_faena = null;

            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                if (i > 0)
                {
                    if (ultima_dato_faena != datos_entrada[0] && ultima_dato_faena != null)
                    {
                        externo.Add(ultima_dato_faena, lista.ToArray());
                        lista.Clear();
                    }

                    string[] aux_string = new string[1];
                    double[] aux_double = new double[3];

                    aux_string[0] = datos_entrada[1];

                    aux_double[0] = double.Parse(datos_entrada[2], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[1] = double.Parse(datos_entrada[3], System.Globalization.CultureInfo.InvariantCulture);
                    aux_double[2] = double.Parse(datos_entrada[4], System.Globalization.CultureInfo.InvariantCulture);

                    ultima_dato_faena = datos_entrada[0];

                    lista.Add(new Dato(aux_string, aux_double));
                }

            }
            externo.Add(ultima_dato_faena, lista.ToArray());
            return externo;
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

        private static Dictionary<string, Dictionary<string,double>> gen_p_envio(string ruta_probabilidad_envio)
		{
            Dictionary<string, Dictionary<string, double>> externo = new Dictionary<string, Dictionary<string, double>>();

            Dictionary<string, double> dictionary_aux = null;

            string[] input_file = File.ReadAllLines(ruta_probabilidad_envio);
            string[] faenas = null;

            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                

                if (i > 0)
                {
                    for (int j = 1; j < datos_entrada.Length; j++)
                    {
                        double aux_double = double.Parse(datos_entrada[j], System.Globalization.CultureInfo.InvariantCulture);
                        dictionary_aux = externo[faenas[j - 1]];
                        dictionary_aux.Add(datos_entrada[0], aux_double);
                    }
                }
                else
                {
                    faenas = new string[datos_entrada.Length - 1];
                    for (int j = 1; j < datos_entrada.Length; j++)
                    {
                        faenas[j - 1] = datos_entrada[j];
                        externo.Add(datos_entrada[j], new Dictionary<string, double>());
                    }
                }

            }
            return externo;
		}

        private static Dictionary<string, Dictionary<string, int>> gen_componentes_camion(string ruta_componentes_camion)
        {
            Dictionary<string, Dictionary<string, int>> externo = new Dictionary<string, Dictionary<string,int>>();
            Dictionary<string, int> interno = new Dictionary<string,int>();
            string[] input_file = File.ReadAllLines(ruta_componentes_camion);
            string ultima_dato_faena = null;
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');
                if (i > 0)
                {
                    if (ultima_dato_faena != datos_entrada[0] && ultima_dato_faena != null)
                    {
                        externo.Add(ultima_dato_faena, interno);
                        interno = new Dictionary<string,int>();
                    }

                    int aux_int;

                    aux_int = int.Parse(datos_entrada[2], System.Globalization.CultureInfo.InvariantCulture);
                    ultima_dato_faena = datos_entrada[0];

                    interno.Add(datos_entrada[1], aux_int);
                }

            }
           externo.Add(ultima_dato_faena, interno);
           return externo;
        }

        private static Dictionary<string, Dictionary<string, double[]>> gen_tasa_falla_componente(string ruta_falla_componentes)
		{
            Dictionary<string, Dictionary<string, double[]>> externo = new Dictionary<string, Dictionary<string, double[]>>();
            Dictionary<string, double[]> interno = new Dictionary<string, double[]>();

            string[] input_file = File.ReadAllLines(ruta_falla_componentes);
            string ultima_dato_faena = null;
            for (int i = 0; i < input_file.Length; i++)
            {
                string[] datos_entrada = input_file[i].Split(',');


                if (i > 0)
                {
                    if (ultima_dato_faena != datos_entrada[0] && ultima_dato_faena != null)
                    {
                        externo.Add(ultima_dato_faena, interno);
                        interno = new Dictionary<string, double[]>();
                    }

                    double[] aux_double = new double[datos_entrada.Length-2];

                    for (int j = 2; j < datos_entrada.Length; j++)
                    {
                        aux_double[j-2] = double.Parse(datos_entrada[j], System.Globalization.CultureInfo.InvariantCulture);
                    }

                    ultima_dato_faena = datos_entrada[0];

                    interno.Add(datos_entrada[1],aux_double);

                }
            }
            externo.Add(ultima_dato_faena, interno);
            return externo;
		}

	}
}

