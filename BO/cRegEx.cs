using System;
using System.Text.RegularExpressions;

namespace BO
{
	/// <summary>
	/// Summary description for cRegEx.
	/// </summary>
	public class cRegEx
	{
		private Regex _re;

		public cRegEx(string sPattern)
		{
			// example sPattern:
			//   "\t(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";

			/* RegexOptions reOptions = RegexOptions.Multiline | 
				                     RegexOptions.IgnoreCase | 
				                     RegexOptions.IgnorePatternWhitespace;
			*/
			RegexOptions reOptions = RegexOptions.Multiline | 
				RegexOptions.IgnoreCase;
			_re = new Regex(sPattern, reOptions);
		}

		public string[] Split(string sLine)
		{
			string[] astr = null; // astr = array of strings
			astr = _re.Split(sLine);

			return astr;
		}

	}
}
