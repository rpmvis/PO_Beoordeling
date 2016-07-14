using System;
using System.Data; 
using System.Text; 
using FUNC;

namespace BO
{
	/// <summary>
	/// Summary description for cLkp.
	/// </summary>
	public class cLkp
	{
		public cLkp()
		{
		}

		public static int Count(DAL_OleDb oDal, string sField_count, string sTable, string sField_GroupBy, string sHaving)

		//  example:
		//  SELECT COUNT(Comp) AS Count_Comp
		//	FROM    tProf_Comp
		//  GROUP BY Prof
		//  HAVING (Prof = //POhfd//)
		{
			DAL_OleDb mDal = new DAL_OleDb("BEO");
			
			StringBuilder sSQL = new StringBuilder(); 

			sSQL.Append("SELECT COUNT(("); 
			sSQL.Append(sField_count); 
			sSQL.Append(")) AS Counted");
			sSQL.Append(" FROM "); 
			sSQL.Append(sTable); 
			sSQL.Append(" GROUP BY "); 
			sSQL.Append(sField_GroupBy); 
			sSQL.Append(" HAVING "); 
			sSQL.Append(sHaving); 
			sSQL.Append(";"); 

			System.Data.IDataReader dr = mDal.ExecQuery_DataReader(sSQL.ToString());  	
			
			int iRet = 0;
			try
			{
				dr.Read();
				iRet = (int)dr.GetValue(0);  
			}

			catch (Exception e)
			{
				string sMsg = "Methode //Count// mislukt van telling op " + sField_count + "// binnen tabel //" + sTable + "//!";
				throw new DALException(sMsg.ToString() , e);
			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); // close to avoid error //a DataReader already open on this connnection//
					dr.Dispose();
				}
			}
			return iRet;
		}

		public static string Lkp(DAL_OleDb oDal, string sField, string sTable, string sWhere)
		// lookup string lookup value form field in database table
		{
			StringBuilder sSQL = new StringBuilder(); 

			sSQL.Append("SELECT "); 
			sSQL.Append(sField); 
			sSQL.Append(" FROM "); 
			sSQL.Append(sTable);
 
			if (sWhere !="")
			{
				sSQL.Append(" WHERE "); 
				sSQL.Append(sWhere); 
			}
			sSQL.Append(";"); 

			System.Data.IDataReader dr = oDal.ExecQuery_DataReader(sSQL.ToString());  	

			string sRet;

			try
			{
				dr.Read();
				sRet = dr.GetValue(0).ToString();  
			}

			catch (Exception e)
			{
				string sMsg = "Methode //Lkp// mislukt bij opzoeken van //" + sField.ToString()  + "// binnen tabel //" + sTable + "//!";
				throw new DALException(sMsg.ToString() , e);
			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); // close to avoid error //a DataReader already open on this connnection//
					dr.Dispose();
				}
			}
			return sRet;
		}


	public static string[] Lkp(DAL_OleDb oDal, string[] sFields, string sTable, string sWhere)
	// lookup string lookup value form field in database table
{
		StringBuilder sSel= new StringBuilder(); 
		int iUB = sFields.GetUpperBound(0);

		for(int i=0; i <= iUB;i++) 
		{
			sSel.Append(sFields[i]);
			sSel.Append(", ");
		}
		sSel.Remove(sSel.Length -2, 2); 

		StringBuilder sSQL= new StringBuilder(); 

		sSQL.Append("SELECT "); 
		sSQL.Append(sSel.ToString() ); 
		sSQL.Append(" FROM "); 
		sSQL.Append(sTable); 
		sSQL.Append(" WHERE "); 
		sSQL.Append(sWhere); 
		sSQL.Append(";"); 
	
		System.Data.IDataReader dr = oDal.ExecQuery_DataReader(sSQL.ToString());  	

		string[] sRet = new string[iUB+1]; // = Array.CreateInstance(typeof(System.String), iUB); 

		try
		{
			dr.Read();
			for(int i=0; i <= iUB;i++) 
			{
				sRet[i] = dr.GetValue(i).ToString();   
			}
		}

		catch (Exception e)
		{
			string sMsg = "Methode //Lkp// mislukt bij opzoeken van " + sFields.ToString()  + "// binnen tabel //" + sTable + "//!";
			throw new DALException(sMsg.ToString() , e);
		}
		finally
		{
			if (dr != null)
			{
				dr.Close(); // close to avoid error //a DataReader already open on this connnection//
				dr.Dispose();
			}
		}
  	return sRet;
}


	}
}
