using System;
using System.IO; 

namespace FUNC
{
	/// <summary>
	/// Summary description for cTextFile.
	/// </summary>
	public class cTextFile
	{
		string msPath = "";

		public cTextFile(string  sPath)
		{
			msPath = sPath;
		}

		public string ReadLine()
		{
			// "C:\\Temp\\Test.txt"
			 StreamReader srReadLine = new StreamReader(
				(System.IO.Stream)File.OpenRead(msPath),
				System.Text.Encoding.ASCII);

			string sRet = srReadLine.ReadLine();  

			srReadLine.Close(); 

			return sRet;
		}
	}
}
