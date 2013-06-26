using System;

namespace RNG
{
	public class RNGen
	{
		private static Random ran = new Random(DateTime.Now.Millisecond);
		public RNGen ()
		{
		}
		public static double Gamma(double alpha, double beta)
		{
			//La funcion gamma no puede recibir valores negativos de parámetros
			if (alpha <= 0 || beta <= 0)
			{
				return -1;
			}
			while(true)
			{
				double z = ran.NextDouble();
				double r;
				double t;
				double c;
				//Se genera la instancia de r
				if(alpha > 0 && alpha < 1)
				{
					double e = Math.Exp (1);
					double b = (e+alpha)/e;
					c = b/(alpha*gamma_function(alpha));
					if(z <= 1/b)
					{
						r = Math.Pow(b*z,1/alpha);
						t=c*alpha*Math.Pow(r,alpha-1)/b;
						
					}
					else
					{
						r = -Math.Log (b*(1-z)/alpha);
						t=c*alpha*Math.Exp (-r)/b;
					}
					
				}
				else
				{
					double lambda = Math.Sqrt(2*alpha-1);
					double u = Math.Pow(alpha, lambda);
					c=4*Math.Pow (alpha,alpha)*Math.Exp(-alpha)/(lambda*gamma_function(alpha));
					r = Math.Pow(u*z/(1-z),1/lambda);
					if (r==0)
					{
						t=0;
					}
					else
					{
						t=c*lambda*u*Math.Pow (r,lambda-1)/Math.Pow (u+Math.Pow(r,lambda),2);
					}
				}
				
				double div = dgamma (r,alpha, 1)/t;
				double unif = ran.NextDouble();
				if (unif <= div)
				{
					return r*beta;
				}
			}	
		}
		private static double gamma_function(double x)
		{
			double[] p = {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
	     	  771.32342877765313, -176.61502916214059, 12.507343278686905,
	     	  -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};
			int g = 7;
			if(x < 0.5) return Math.PI / (Math.Sin(Math.PI * x)*gamma_function(1-x));
	 
			x -= 1;
			double a = p[0];
			double t = x+g+0.5;
			for(int i = 1; i < p.Length; i++)
			{
				a += p[i]/(x+i);
			}
	 
			return Math.Sqrt(2*Math.PI)*Math.Pow(t, x+0.5)*Math.Exp(-t)*a;
		}
		private static double dgamma(double x,double a, double b)
		{
			return Math.Pow(b,a)/gamma_function(a)*Math.Pow (x,a-1)*Math.Exp (-b*x);
		}
		public static double Beta(double a, double b)
		{
			double Y1 = Gamma (a,1);
			double Y2 = Gamma (b,1);
			return Y1/(Y1+Y2);
		}
		public static double Unif(double a, double b)
		{
			return a+ran.NextDouble()*(b-a);
		}
		public static double Arcsin()
		{
			return Math.Pow(Math.Sin(Math.PI*ran.NextDouble()/2),2);
		}
		public static double CustomFit(double[] input)
		{
			if (input.Length != 21)
			{
				throw new Exception();
			}
			double U = Unif (0,1);
			//Checkear en que área queda
			double suma=0;
			for(int i=1;i<21;i++)
			{
				suma+=input[i];
			}
			double punto = U*suma;
			double sa=0;
			//Se busca donde está ese punto
			for(int i=1;i<21;i++)
			{
				sa+=input[i];
				if (punto <= sa)
				{
					//Se encontró el punto... Luego, se devuelve la instancia
					return punto;
				}
			}
			return punto;
		}
		public static double Expo(double rate)
		{
			return -Math.Log (1-ran.NextDouble())/rate;
		}
	}
}

