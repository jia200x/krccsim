using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSControlador;

namespace KRCCSim
{
    public class Camion:Evento
    {
        public List<Componente> componentes;
        public double tiempo_actual, tiempo_max;
        public Faena faena;
		private double tiempo_vida;
		private double tiempo_creacion;
		public string tipo_camion;
		public bool muerto;
		public Dictionary<string, double[]> componente_camion;
		public Camion(Controlador c, Faena faena, string tipo_camion)
		{
			this.componentes = new List<Componente>();
			this.faena = faena;
			this.faena.agregar_camion(this);
			this.c = c;
			this.tiempo_creacion = c.T_simulacion;

            string[] auxiliar = new string[2];
            auxiliar[0] = faena.Nombre;
            auxiliar[1] = tipo_camion;
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
            componente_camion = null;
            throw new NotImplementedException();
            //Alamos he cambiado la estructura del input, favor revísalo

			//Se crean los componentes, se les asignan los tiempos
			foreach (var par in componente_camion)
			{
				Componente componente = new Componente(this.c, this, par.Key,par.Value);
				this.agregar_componente(componente);
			}
			
			
		}
		public override void realizar_cambio()
		{
			//Aquí se puede programar el cambio de camión. Esto es llamado automaticamente al ser suscrito
			this.muerto = true;
			//Se le indica a la faena que cree otro camión
			Camion nuevo_camion = new Camion(this.c, this.faena, "E903");
			faena.agregar_camion(nuevo_camion);
			
			//Se borra el camion actual de la faena
			faena.camiones.Remove(this);
			
			
		}
		public void agregar_componente(Componente componente)
		{
			componentes.Add (componente);
		}
		public void reemplazar_componente(Componente defectuoso)
		{
			componentes.Remove(defectuoso);
			//Con probabilidad P se agrega al batch...

            string[] auxiliar = new string[2];
            auxiliar[0] = faena.Nombre;
            auxiliar[1] = tipo_camion;
            //cambie los dictrionaries
            throw new NotFiniteNumberException();
            //double[] aux = Input.tiempo_vida_camion[auxiliar];
            //double probabilidad = aux[0];

			faena.agregar_a_batch(defectuoso);		
			
			//Agregar el componente nuevo
			agregar_componente(new Componente(this.c, this, defectuoso.tipo_componente, componente_camion[defectuoso.tipo_componente]));
		}
		public double edad
		{
			get{
				return c.T_simulacion-tiempo_creacion;
			}
		}
    }
}
