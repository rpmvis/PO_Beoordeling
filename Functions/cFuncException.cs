using System;

namespace FUNC
{
	/// <summary>
	/// Summary description for cFuncException.
	/// </summary>
		public class FuncException : System.Exception
		{

			public FuncException(string message):base(message)
			{
				Console.Write(message);
			}
		
			public FuncException(string message, Exception innerException) : base(message, innerException)
			{
				string s="";
				s = innerException.Message;
				Console.Write(innerException.Message + "-" + innerException.Source);
			}
		}
	}

