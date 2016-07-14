using System;
using System.Text;

namespace BO
{
	

	/// <summary>
	/// Summary description for cTag.
	/// </summary>
	public class cTag
	{
		private bool mbHtml;

		public cTag(bool IsHtmlFormat)
		{
			mbHtml = IsHtmlFormat;
		}

		public string BR()
		{
			if (mbHtml) return "<br>";
			else return "\n";
		}

		public string BR(byte iTimes)
		{
			if (iTimes==0) return "";
			else
			{
				StringBuilder sb = new StringBuilder(); 				

				for (byte i = 0; i < iTimes; i++)
				{
					if (mbHtml) sb.Append("<br>");
					else sb.Append("\n");
				}
				return sb.ToString(); 
			}
		}

		public string MailTo(string sAddress)
		{
			// <A href="mailto:wvd@dro.amsterdam.nl">wvd@dro.amsterdam.nl</A>
			if (mbHtml) 
			{
				StringBuilder sb = new StringBuilder(); 
				sb.Append("<A href=\"mailto:");
				sb.Append(sAddress);
				sb.Append("\">");
				sb.Append(sAddress); 
				sb.Append("</A>"); 
				
				return sb.ToString(); 
			}
			else return sAddress;
		}

	}
}
