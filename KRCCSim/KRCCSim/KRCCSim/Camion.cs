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
		public double tasa_trabajo;
		public string tipo_camion;
		public double tiempo_inicializacion;
		public double tiempo_restante;
		private double tiempo_creacion;
		public double tiempo_muerte;
		public bool muerto;
		public Dictionary<string, double[]> componente_camion;
		public Camion(Controlador c, Faena faena, string tipo_camion, double[] tiempos)
		{
			this.componentes = new List<Componente>();
			this.faena = faena;
			this.faena.agregar_camion(this);
			this.c = c;
			this.tiempo_creacion = this.c.T_simulacion;
			this.muerto = false;
			this.tipo_camion = tipo_camion;
			
			
			//También, aquí se crean los componentes asociados al camión
            //componente_camion = Input.tasa_falla_componentes[tipo_camion];
            //Alamos he cambiado la estructura del input, favor revísalo

			//Se crean los componentes, se les asignan los tiempos
			actualizar_tiempos(tiempos[0],tiempos[1], tiempos[2]);
			foreach (var par in Input.tasa_falla_componentes[tipo_camion])
			{
				for (int i=0;i<Input.componentes_por_camion[this.tipo_camion][par.Key];i++)
				{
					bool usado=false;
					if (this.tiempo_inicializacion > 0) usado=true;
					Componente componente = new Componente(this.c, this, par.Key,par.Value,usado);
					this.agregar_componente(componente);
				}
			}
			
			
		}
		public void actualizar_tiempos(double tasa_trabajo, double t_restante, double t_init)
		{
			this.tasa_trabajo = tasa_trabajo;
			this.tiempo_restante = t_restante;
			this.tiempo_inicializacion = t_init;
			//Aquí se suscribe el reemplazo de camión
			this.tiempo_muerte = this.tiempo_restante*(24*365)/tasa_trabajo;
			generar_siguiente_tiempo(this.tiempo_muerte);
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
            double probabilidad = Input.probabilidad_envio[this.faena.Nombre][defectuoso.tipo_componente];
			//Con probabilidad P se agrega al batch...
			if (RNGen.Unif(0,1) <= probabilidad) faena.agregar_a_batch(defectuoso);		
			
			//Agregar el componente nuevo
			agregar_componente(new Componente(this.c, this, defectuoso.tipo_componente, Input.tasa_falla_componentes[this.tipo_camion][defectuoso.tipo_componente]));
		}
		public double edad
		{
			get{
				return tiempo_inicializacion/tasa_trabajo+this.c.T_simulacion/(24*365);
			}
		}
    }
}
