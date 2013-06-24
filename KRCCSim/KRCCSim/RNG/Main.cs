using System;

namespace RNG
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			for(int i=1;i<100;i++)
			{
				Console.WriteLine (RNGen.Beta (1,1));
			}
		}
	}
}
