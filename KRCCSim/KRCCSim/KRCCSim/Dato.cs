using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRCCSim
{
    public class Dato
    {
        string[] arr_string;
        double[] arr_double;

        public Dato(string[] arr_string, double[] arr_double)
        {
            this.arr_string = arr_string;
            this.arr_double = arr_double;
        }

        public Dato()
        {
        }
    }
}
