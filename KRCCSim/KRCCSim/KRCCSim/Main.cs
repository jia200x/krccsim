using System;
using NSControlador;

namespace KRCCSim
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Agregar una faena de prueba
			Controlador c = new Controlador(100);
			Faena RT = new Faena(c);
			Camion E903 = new Camion();
			RT.agregar_camion(E903);
			c.agregar_evento(RT);
			c.Run();
		}
	}
}
