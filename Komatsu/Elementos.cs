using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komatsu
{
    public abstract class Elemento
    {
        public double tiempo_cambio;

        public abstract void realizar_cambio(double tiempo);

    }



}
