using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;
using RNG;
namespace KRCCSim
{
	public class VaciadoBatch:Evento
	{
		private List<Faena> faenas;
		public VaciadoBatch (Controlador c, List<Faena> faenas)
		{
			this.c = c;
			this.faenas = faenas;	
			generar_siguiente_tiempo(RNGen.Expo(6)*24);
		}
		public override void realizar_cambio()
		{
			foreach(Faena f in faenas)
			{
				f.vaciar_batch();
			}
			generar_siguiente_tiempo(RNGen.Expo(6)*24);
		}
	}
}

