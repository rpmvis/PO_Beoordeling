using System;

namespace FUNC
{
	/// <summary>
	/// Summary description for cMath.
	/// </summary>
	public class cMath
	{
		public cMath()
		{
		}

		public static int Integer(double dblNumber)
		{
			int iTemp = 0;
			iTemp = System.Convert.ToInt32(dblNumber);
			// correctie voor afronding naar boven
			if (iTemp > dblNumber) iTemp--;
			return iTemp;
		}

		public static int Modulus(int Num1, int Num2)
		{
			int iTemp = 0, iRest = 0;
			double dblNum = System.Convert.ToDouble(Num1)/Num2;
			iTemp = Integer(dblNum);
			iRest = Num1 - iTemp*Num2 ;
			return iRest;
		}

		public static int Min(int Num1, int Num2)
		{
			if (Num1 < Num2) return Num1;
			else return Num2;
		}

		public static int Max(int Num1, int Num2)
		{
			if (Num1 > Num2) return Num1;
			else return Num2;
		}


	}
}
