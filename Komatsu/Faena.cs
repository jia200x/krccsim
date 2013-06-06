using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komatsu
{
    public class Faena : Elemento
    {
        public string nombre;
        public List<Camion> camiones = new List<Camion>();
        public List<Componente> batch = new List<Componente>();

        public event Action<Componente,Componente> cambio_componente;

        public Faena():base()
        {
        }

        public void Agregar_Camion(Camion.Clase clase,double antiguedad)
        {
            Camion camion = new Camion(clase, antiguedad,this);
            camiones.Add(camion);
        }

        public void Agregar_Camion(Camion camion)
        {
            camiones.Add(camion);
        }

        public void Cambiar_Componente(double tiempo, Componente componente)
        {
            Componente nuevo_componente = componente.replicate();
            batch.Add(componente);
            nuevo_componente.camion.componentes.Remove(componente);
            nuevo_componente.camion.componentes.Add(nuevo_componente);
            nuevo_componente.generar_falla(tiempo);
            cambio_componente(componente, nuevo_componente);
        }

        public override void realizar_cambio(double tiempo)
        {
            throw new NotImplementedException();
        }
    }

}
