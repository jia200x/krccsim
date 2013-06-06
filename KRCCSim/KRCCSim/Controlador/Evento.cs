using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controlador
{
    public abstract class Evento
    {
        public double tiempo_cambio;
        public abstract void realizar_cambio();
    }
}
