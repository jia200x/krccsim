using System;
using NSControlador;

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
                null, @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_ponderadores.csv",
                @"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_componentes_por_camion.csv");*/
			
			Input.Inicializar(ruta_root+"Input_Faenas.csv", 
                ruta_root+"input_probabilidad envio.csv",
                ruta_root+"Input_ingresos_programados.csv",
                ruta_root+"Input_reemplazos.csv",
                ruta_root+"Input_mortalidad.csv",
                ruta_root+"Input_falla_componentes.csv", ruta_root+"Input_ponderadores.csv",
                ruta_root+"Input_componentes_por_camion.csv");

			//Agregar una faena de prueba
			Controlador c = new Controlador(1000);
			foreach(var f in Input.ponderadores)
			{
				Console.Write (f.Key);
				Faena fa = new Faena(c,f.Key);
			}
			c.Run();
		}
	}
}
