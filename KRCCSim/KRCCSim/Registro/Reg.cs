using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Registro
{
	public class Reg
	{
		private static List<string[]> registros = new List<string[]>();
		public Reg ()
		{
		}
		public static void agregar_registro(double tiempo, string faena, string camion, string componente, string status)
		{
			registros.Add (new string[]{tiempo.ToString(),faena,camion,componente,status});
		}
	}
}

