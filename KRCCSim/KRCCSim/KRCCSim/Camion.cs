using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;
using RNG;

namespace KRCCSim
{
    public class Camion:Evento
    {
        public List<Componente> componentes;
        public double tiempo_actual, tiempo_max;
        public Faena faena;
		private double tiempo_vida;
		public string tipo_camion;
		public double tiempo_creacion;
		public bool muerto;
		public Dictionary<string, double[]> componente_camion;
		public Camion(Controlador c, Faena faena, string tipo_camion)
		{
			this.componentes = new List<Componente>();
			this.faena = faena;
			this.faena.agregar_camion(this);
			this.c = c;

            string[] auxiliar = new string[2];
            auxiliar[0] = faena.Nombre;
            auxiliar[1] = tipo_camion;
            double[] aux = Input.tiempo_vida_camion[auxiliar];
			//Sumarle el tiempo actual
			this.tiempo_vida = aux[0];
			//Corregir!!
			this.tiempo_creacion = 0;
            //cambie los dictrionaries
            throw new NotFiniteNumberException();
            //double[] aux = Input.tiempo_vida_camion[auxiliar];
            //this.tiempo_vida = aux[0];

			this.muerto = false;
			this.tipo_camion = tipo_camion;
			
			//Aquí se suscribe el reemplazo de camión
			generar_siguiente_tiempo(this.tiempo_vida);
			
			//También, aquí se crean los componentes asociados al camión
            //componente_camion = Input.tasa_falla_componentes[tipo_camion];
            //Alamos he cambiado la estructura del input, favor revísalo

			//Se crean los componentes, se les asignan los tiempos
			foreach (var par in Input.tasa_falla_componentes[tipo_camion])
			{
				for (int i=0;i<Input.componentes_por_camion[this.tipo_camion][par.Key];i++)
				{
					Componente componente = new Componente(this.c, this, par.Key,par.Value);
					this.agregar_componente(componente);
				}
			}
			
			
		}
		public override void realizar_cambio()
		{
			//Aquí se puede programar el cambio de camión. Esto es llamado automaticamente al ser suscrito
			this.muerto = true;
			//Se le indica a la faena que cree otro camión
			faena.reemplazar_camion(this);
			
			
		}
		public void agregar_componente(Componente componente)
		{
			componentes.Add (componente);
		}
		public void reemplazar_componente(Componente defectuoso)
		{
			componentes.Remove(defectuoso);
			

            string[] auxiliar = new string[2];
            auxiliar[0] = faena.Nombre;
            auxiliar[1] = tipo_camion;
            double[] aux = Input.tiempo_vida_camion[auxiliar];
            double probabilidad = aux[0];
			//Con probabilidad P se agrega al batch...
			if (RNGen.Unif(0,1) <= probabilidad) faena.agregar_a_batch(defectuoso);		
            //cambie los dictrionaries
            throw new NotFiniteNumberException();
			
			//Agregar el componente nuevo
			agregar_componente(new Componente(this.c, this, defectuoso.tipo_componente, Input.tasa_falla_componentes[new string[]{this.tipo_camion,defectuoso.tipo_componente}]));
		}
		public double edad
		{
			get{
				return c.T_simulacion-tiempo_creacion;
			}
		}
    }
}
