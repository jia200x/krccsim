﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;

namespace KRCCSim
{
    public class Componente:Evento
    {
        private Camion camion;
		public string tipo_componente;
		public override void realizar_cambio()
		{
			//En este punto se puede preguntar si el camion sigue vivo o no
			if (camion.muerto == false)
			{
				camion.reemplazar_componente(this, new Componente(c,camion, "tipo"));
			}
		}
		public Componente(Controlador c, Camion camion, string tipo_componente)
		{
			this.c = c;
			this.camion = camion;
			this.tipo_componente = tipo_componente;
			generar_siguiente_tiempo(generar_tiempo_de_falla());
		}
		private double generar_tiempo_de_falla()
		{
			//La falla depende de la edad del camión, del tipo de camión, de la faena y del componente en si.
			//Toda esa información es rescatable dado que el componente sabe en que camión está.
			//El componente tiene un arreglo de una dimensión que indica la tasa de falla beta con respecto a la edad del camión
			return (double) 30;
		}
    }
}
