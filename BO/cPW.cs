using System;
using System.Text; 
using FUNC;


namespace BO
{
	/// <summary>
	/// Summary description for cPW.
	/// </summary>
	public class cPW
	{
		public cPW()
		{
		}

		public string GeneratePW(int Len)
		{
			StringBuilder sbPW = new StringBuilder();   
			String s19 = "123456789";
			String sLow = "abcdefghijklmnopqrstuvwxyz";
			String sUpp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Char[] aLowChar = sLow.ToCharArray(); 
			Char[] aUppChar = sUpp.ToCharArray(); 

			Random rnd = new Random();
			for (int i = 0; i < Len; i++)
			{
				// 26 letters + 9 cijfers (geen nul)
				byte num = (byte)rnd.Next(0, 34);

				if (num <= 8)
				{
					sbPW.Append(s19[num]); 
				}
				else
				{
					double LowerUpper = rnd.Next(0, 2);
					if(LowerUpper < 1) // lower case
					{
						sbPW.Append(aLowChar[num-9]); 
					}
					else // upper case
					{
						sbPW.Append(aUppChar[num-9]); 
					}
				}
			}
			return sbPW.ToString(); 
		}
  		
		public bool PassWordsAreEqual(string PW1, string PW2)
		{
			return (cString.Compare(PW1, PW2) == 0 );
		}
	}
}
