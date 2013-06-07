using System;
using NSControlador;

namespace KRCCSim
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Agregar una faena de prueba
			Controlador c = new Controlador(1000);
			Faena RT = new Faena(c);
			Camion E903 = new Camion(c, RT);
			Componente tarjeta104 = new Componente(c, E903);
			E903.agregar_componente(tarjeta104);
			
			RT.agregar_camion(E903);
			c.Run();
		}
	}
}
