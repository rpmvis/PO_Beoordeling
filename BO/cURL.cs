using System;
using System.Collections.Specialized;
using System.Text; 
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.JScript; 


namespace BO
{
	/// <summary>
	/// Summary description for cURL.
	/// </summary>
	public class cURL
	{
		private string msRawURL = "", msHRef = "", msQueryString = "";
		private NameValueCollection mcolNV = null;

		public cURL(string sRawURL)
		{
			msRawURL = sRawURL; 
			
			int iPos = msRawURL.IndexOf("?");
			if (iPos > 0)
			{
				msHRef = msRawURL.Substring(0, iPos-1); 
				msQueryString = msRawURL.Substring(iPos +1);

				// converteren van querystring naar naam-waarde collectie
				// hierbij UnEscapr functie uitvoeren
				char[] sepNV ="&".ToCharArray();
				string[] sSplitNV1 = msQueryString.Split(sepNV);   
	    
				mcolNV = new NameValueCollection(); 
				for(int i = 0; i <= sSplitNV1.GetUpperBound(0); i++)
				{ 
					char[] sepNV2 ="=".ToCharArray();
					string[] sSplitNV2 = sSplitNV1[i].Split(sepNV2);
					if (sSplitNV2.Length != 2)
					{
						throw new Exception("Lengte van naam-waarde array ongelijk aan 2 bij URL expresssie " + this.RawURL + "!");
					}
					mcolNV.Add(sSplitNV2[0], sSplitNV2[1]);  	
				}
			}
		}

		public string RawURL
		{
			get
			{
				return msRawURL;
			}
		}

		public string HRef
		{
			get
			{
				return msHRef;
			}
		}

		public string QueryString
		{
			get
			{
				return msQueryString;
			}
		}

		public string GetValue(string sKey)
		{
			string sRet = "";
			if (mcolNV!= null)
			{
				sRet = HttpUtility.UrlDecode(mcolNV[sKey]); // regelt diacrytische tekens (ö)
				sRet = Microsoft.JScript.GlobalObject.unescape(sRet); // regelt "=" en "&" en "?"
			}
			return sRet;
		}
	}
}
