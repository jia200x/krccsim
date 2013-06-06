using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komatsu
{
    public abstract class Componente : Elemento
    {
        public double media;
        public Faena faena;
        public Camion camion;

        public Componente(Faena faena, Camion camion)
        {
            this.faena = faena;
            this.camion = camion;
        }

        public override void realizar_cambio(double tiempo)
        {
            faena.Cambiar_Componente(tiempo, this);
        }

        public void generar_falla(double tiempo)
        {
            tiempo_cambio = tiempo + 20;
        }

        public abstract Componente replicate();
    }

    /*
     * Separamos cada tipo de componente como uno solo con el fin de asociar una distribución distinta a cada componente
     */
    public class Temperature_Card : Componente
    {
        public Temperature_Card(Faena faena, Camion camion)
            : base(faena, camion)
        {
        }

        public override void realizar_cambio(double tiempo)
        {
            Console.WriteLine("Se ha cambiado una temperature card");
            base.realizar_cambio(tiempo);
        }

        public override Componente replicate()
        {
            Temperature_Card card = new Temperature_Card(this.faena, this.camion);
            card.media = this.media;
            return card;
        }
    }

    public class Power_Converter : Componente
    {
        public Power_Converter(Faena faena, Camion camion)
            : base(faena, camion)
        {
        }


        public override void realizar_cambio(double tiempo)
        {
            Console.WriteLine("Se ha cambiado un power converter");
            base.realizar_cambio(tiempo);
        }

        public override Componente replicate()
        {
            Power_Converter card = new Power_Converter(this.faena, this.camion);
            card.media = this.media;
            return card;
        }


    }

    public class FDP_Panel : Componente
    {
        public FDP_Panel(Faena faena, Camion camion)
            : base(faena, camion)
        {
        }

        public override void realizar_cambio(double tiempo)
        {
            Console.WriteLine("Se ha cambiado un fdp panel");
            base.realizar_cambio(tiempo);
        }

        public override Componente replicate()
        {
            FDP_Panel card = new FDP_Panel(this.faena, this.camion);
            card.media = this.media;
            return card;
        }


    }

}
