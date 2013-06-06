using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controlador;

namespace KRCCSim
{
    public abstract class Camion:Evento
    {
        public List<Componente> componentes;
        public double tiempo_actual, tiempo_max;
        public Faena faena;
    }
}
