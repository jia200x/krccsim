using System;
using NSControlador;

namespace KRCCSim
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Input.Inicializar(null,null,null,@"E:\Dropbox\Proyecto KOMATSU (1)\Input\nuevas estructuras\Input_reemplazos.csv",null,null);

			//Agregar una faena de prueba
			Controlador c = new Controlador(1000);
			Faena RT = new Faena(c);
			Camion E903 = new Camion(c, RT, "E903");			
			c.Run();
		}
	}
}
