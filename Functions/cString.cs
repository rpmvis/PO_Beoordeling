using System;
using System.Text;

namespace FUNC
{
	/// <summary>
	/// Summary description for cString.
	/// </summary>
	public class cString
	{
		public cString()
		{
		}

		public static int Compare(String s1, String s2)
		{
			IComparable ic1 = (IComparable)s1 ; // cast Comparable object to string

			IComparable ic2 = (IComparable)s2;   // cast Comparable object to strin

			return ic1.CompareTo(ic2); // > 0 = groter ; 0 = gelijk ; < 0 = kleiner dan
		}


		public static string Left(string s, int length)
		{
			string sLeft = s.Substring(0, length);
			return sLeft;
		}

		public static string Right(string s, int length)
		{
			int iStart = s.Length - length; 
			if (iStart <0){iStart = 0;}
			string s2 =s.Substring(iStart);
			return s2;
		}

		public static string Strip(string s, string ToStrip)
		{
			string sRight = Right(s, ToStrip.Length);
			if (Compare(sRight, ToStrip)==0) // indien strings aan elkaar gelijk
			{
				return Left(s, s.Length - ToStrip.Length);
			}
			else {return s;}
		}

		public static string Strip(System.Text.StringBuilder  sb, string ToStrip)
		{
			string s = sb.ToString();
			return Strip(s, ToStrip);
		}

		/// <summary>
		/// This function checks whether input i
		//     s numeric or not
		/// </summary>
    	
		public static bool isNumeric(string str)
		{
			if (str.Trim() == "")
			{
				return false;
			}
			char [] ca = str.ToCharArray();
			for (int i = 0; i < ca.Length;i++)
			{
				if (ca[i] > 57 || ca[i] < 48)
					return false;
			}
			return true;
		}

		public static int Instr(string String1, string String2)
		{
			return Instr(0, String1, String2);
		}

		public static int Instr(int Start, string String1, string String2)
			// returns position of String2 found within String1 from position Start
			// returns -1 if nothing is found
		{
			string sSub;
			int iRet = -1;

			// compare only over chars needed
			int iUB = String1.Length - String2.Length -Start;
			for(int i = Start;i <= iUB;i++)
			{
				sSub = String1.Substring(i, String2.Length);
				if (sSub.Equals(String2))
				{
					iRet = i;
					return iRet;
				}
			}
			return iRet;
		}

		public static string PostFix(string String, string PostFix)
		{
			if (Right(String, PostFix.Length).Equals(PostFix)) return String;
			else return (String + PostFix);
		}

		public static string AddToResponse(string sAddTo, string sField, string sValue)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(sAddTo);
			sb.Append("&");
			sb.Append(sField);
			sb.Append("=");

			if (sValue == null) sValue = "";
			else
			{
				sValue = sValue.Replace("&","%26"); 
				sValue = sValue.Replace("=","%3D"); 
				sValue = sValue.Replace(";","%3B"); 
			}
			sb.Append(sValue);

			return sb.ToString(); 
		}

		public static string AddToRequest(string sAddTo, string sField, string sValue)
		{
			StringBuilder sb = new StringBuilder("");
			try
			{
				sb.Append(sAddTo);
				sb.Append("&");
				sb.Append(sField);
				sb.Append("=");

				
				sValue = sValue.Replace("&","%26"); 
				sValue = sValue.Replace("=","%3D"); 
				sValue = sValue.Replace(";","%3B"); 
				sb.Append(sValue);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);  
				throw new FuncException("Fout in string-behandeling in procedure 'AddToRequest'", e);
			}
			return sb.ToString(); 
		}
	}
}
