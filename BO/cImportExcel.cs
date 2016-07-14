using System;
using System.Data;
using System.Data.OleDb;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cImportExcel.
	/// </summary>
	public class cImportExcel
	{
		public cImportExcel()
		{
		}

		public void Import(string sFilePath 
						   , ref string sMessage)
		{
			// get Data Source table
			string sSQL = "SELECT * FROM [Blad1$]";
			DataTable dtSource = this.getDataFromXLS(sFilePath, sSQL);

			cImport2 oImport2 = new cImport2();
			oImport2.Import2(sFilePath, ref dtSource, ref sMessage);
		}

		private DataTable getDataFromXLS(string sFilePath, string sSQL)
		{
			// REM: this function is WRONG because server path differs from local path !!!
			string sMsg = "";

			OleDbCommand  oCmd = null;
			try
			{
				// REM: properties with extra double qoute!
				// see web article "http://blogs.wdevs.com/Gaurang/archive/2005/06/15/5112.aspx"
				String sConnString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
					"Data Source=" + sFilePath + ";" +
					"Extended Properties=\"Excel 8.0\";";

				// String sConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+sFilePath; // Extended Properties=""Excel 10.0;HDR=Yes;IMEX=1""" ;
				OleDbConnection cn =new OleDbConnection (sConnString);
				cn.Open();
				oCmd = new OleDbCommand (sSQL, cn);
				OleDbDataAdapter da = new OleDbDataAdapter();
				da.SelectCommand = oCmd;
				DataTable dt = new DataTable();
				try
				{
					da.Fill(dt);
				}
				catch(Exception ex)
				{
					sMsg = "Heeft het eerste Excel werkblad als naam 'Blad1$'?\n\n" +
						ex.Message; 
					throw new Exception(sMsg); 
				}
				cn.Close();

				da = null;
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
