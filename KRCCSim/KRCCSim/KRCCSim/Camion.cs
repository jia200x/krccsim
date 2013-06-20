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
		private double tiempo_creacion;
		public bool muerto;
		public Camion(Controlador c, Faena faena)
		{
			this.componentes = new List<Componente>();
			this.faena = faena;
			this.faena.agregar_camion(this);
			this.c = c;
			this.tiempo_creacion = c.T_simulacion;
			this.muerto = false;
			
			//Aquí se suscribe el reemplazo de camión
			//También, aquí se crean los componentes asociados al camión
		}
		public override void realizar_cambio()
		{
			//Aquí se puede programar el cambio de camión. Esto es llamado automaticamente al ser suscrito
			this.muerto = true;
			//Se le indica a la faena que cree otro camión
			Camion nuevo_camion = new Camion(this.c, this.faena);
			faena.agregar_camion(nuevo_camion);
			
			//Se borra el camion actual de la faena
			faena.camiones.Remove(this);
			
			
		}
		public void agregar_componente(Componente componente)
		{
			componentes.Add (componente);
		}
		public void reemplazar_componente(Componente defectuoso, Componente nuevo)
		{
			componentes.Remove(defectuoso);
			faena.agregar_a_batch(defectuoso);		
			componentes.Add (nuevo);
		}
		public double edad
		{
			get{
				return c.T_simulacion-tiempo_creacion;
			}
		}
		/// <summary>
		/// Se encarga de generar el arreglo bidimensional con la tasa beta de falla, por tipo de componente y edad del camión,
		/// para este camión en específico. Le aplica la ponderación de la faena.
		/// </summary>
		private void generar_lista_fallas()
		{

		}
    }
}
