using System;
using NSControlador;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace KRCCSim
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string ruta_root = "/home/jia200x/Dropbox/Proyecto KOMATSU/Input/nuevas estructuras/";
            /*Input.Inicializar(@"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_Faenas.csv", 
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\input_probabilidad envio.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_ingresos_programados.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_reemplazos.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_mortalidad.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_falla_componentes.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_ponderadores.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_componentes_por_camion.csv");*/
			
			Input.Inicializar(ruta_root+"Input_Faenas.csv", 
                ruta_root+"input_probabilidad envio.csv",
                ruta_root+"In_ingresos_prog_proyec1.csv",
                ruta_root+"Input_reemplazos.csv",
                ruta_root+"Input_mortalidad.csv",
                ruta_root+"Input_falla_componentes.csv", ruta_root+"Input_ponderadores.csv",
                ruta_root+"Input_componentes_por_camion.csv");

			//Agregar una faena de prueba
			Controlador c = new Controlador((365*24*20));
			List<Faena> lf= new List<Faena>();
			foreach(var f in Input.ponderadores)
			{
				Console.Write (f.Key);
				Faena fa = new Faena(c,f.Key);
				lf.Add (fa);
			}
			Console.WriteLine("Comienza la simulación");
			//Validar estructura
			foreach(Faena f in lf)
			{
				foreach(Camion camion in f.camiones)
				{
					foreach(Componente comp in camion.componentes)
					{
						
					}
				}
			}
			VaciadoBatch vb = new VaciadoBatch(c,lf);
			c.Run();
		}
	}
}
