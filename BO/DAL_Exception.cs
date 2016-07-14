using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;


namespace BO
{
	public class DALException : System.Exception
	{
		public DALException(string message):base(message)
		{
			WriteLog(message);
			Console.Write(message);
		}
		
		public DALException(string sCustomMsg, Exception innerException) : base(sCustomMsg, innerException)
		{
			string s= innerException.Message + "\n" + 
                sCustomMsg + "\nBron: " + innerException.Source;
			WriteLog(s);
			Console.Write(s);
			throw new Exception(s); 
		}

		public void WriteLog(string sLogtext)
		{
			StringBuilder sb  = new StringBuilder();
			sb.Append(DateTime.Now.ToLongTimeString());
			sb.Append(" ");
			sb.Append(DateTime.Now.ToShortDateString());
			sb.Append("\t");
			sb.Append(sLogtext);
			string sLine = sb.ToString();

			string sAppPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
			string sLogFile = sAppPath + "/App_Log.txt";

			FileStream  fs = new FileStream(sLogFile, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter SW = new StreamWriter(fs);
			SW.BaseStream.Seek(0, SeekOrigin.End);
		
			// add text
			SW.WriteLine(sLine);
			// toevoegen regel aan onderliggende FileStream fs
			SW.Flush();
			SW.Close();
		}
	}


//	public class MyException : System.Exception
//	{
//		public MyException(Exception e) : base(e)
//		{
//			string sMsg = e.Message & "\n Bron:" & e.Source;
//		}
//	}
//

}