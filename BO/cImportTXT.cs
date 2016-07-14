using System;
using System.Data;
using System.Data.OleDb;
using System.Text; 
using System.IO; 

namespace BO
{
	/// <summary>
	/// Summary description for cImportTXT.
	/// </summary>
	public class cImportTXT
	{

		public cImportTXT()
		{
		}

		public void Import(string sFilePath 
			               , ref string sText 
						   , ref string sMessage)
		{
			// get Data Source table
			DataTable dtSource = this.GetDataTable_from_TXT(ref sText);

			cImport2 oImport2 = new cImport2();
			oImport2.Import2(sFilePath, ref dtSource, ref sMessage);
		}

		private DataTable GetDataTable_from_TXT(ref string sFileText)
		{
			string sMsg = "";
			DataTable dt = new DataTable("tImportCSV"); 
			string sLine = "";
			string[] aLines = null;
			string[] aStr = null;
			string sField = ""; 
			string sText = "";
			Type typStr = System.Type.GetType("System.String"); 
			DataColumn dc = null;
			DataRow dr = null;

			/* string sPattern2 = "\t(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";

			\t		tab
			(?=		begin positive lookahead
			(?:		begin non-capturing group
			[^\"]*\"[^\"]*\"	[^\"] = any character except the quote
								*       repeat
								"       an opening quote!
								[^\"] = any character except the quote
								*       repeat			
								"       an ending quote!					  
			)		end non-capturing group
			*		repeat non-capturing group 0/more times
			(?!		begin negative lookahead
			[^\"]*\"	the lookahead is negative if a quote can be found in the substring following a tab
			)		end negative lookahead
			)		end positive lookahead
			*/
			
			// ?m = for multi line mode
			string sPattern1 = "(?m)\\r\\n|\\n|\\r";           // escape \ with \  

			// string sPattern1 = "^(?!s+)$"; 
			cRegEx oRegEx = new cRegEx(sPattern1);             // idem

			// remove CR/LF's at end of text
			while (true)
			{
				if (sFileText.EndsWith("\n"))
					sFileText = sFileText.Substring(0, sFileText.Length -1); 
				else
					break;
				if (sFileText.EndsWith("\r"))
					sFileText = sFileText.Substring(0, sFileText.Length -1); 
				else
					break;
			}

			aLines = oRegEx.Split(sFileText); 
			
			string sPattern2 = "\\t(?=(?:[^\\\"]*\\\"[^\\\"]*\\\")*(?![^\\\"]*\\\"))";

			oRegEx = new cRegEx(sPattern2); 

			try
			{
				for (int i = 0; i < aLines.Length; i++)
				{
					sLine = aLines[i];
					Console.WriteLine(sLine);
					aStr = oRegEx.Split(sLine); 
					if (i ==0)
					// add table fields
					{
						for(int j = 0; j <= aStr.Length -1; j++)
						{
							sField = aStr[j].ToString();
							dc = new DataColumn();
							dc.ColumnName = sField;
							dc.DataType = typStr;
 
							dt.Columns.Add(dc);
						}
					}
					else
					// add data to a new row and add row to table
					{
						dr = dt.NewRow();
						for(int j = 0; j <= aStr.Length -1; j++)
						{
							sText = aStr[j].ToString();
							dr[j] = sText;
						}
						dt.Rows.Add(dr);  
					}

				}
				return dt;
			}
			catch(Exception ex)
			{	
				sMsg =  ex.Message; 
				throw new Exception(sMsg); 
			}
			finally
			{
				
			}
		}
	}
}
