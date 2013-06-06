using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controlador;

namespace KRCCSim
{
    public abstract class Componente:Evento
    {
        public Camion camion;
        public Faena faena;
    }
}
