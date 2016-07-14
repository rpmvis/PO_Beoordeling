using System;
using System.Data;
using System.Data.OleDb;  


namespace BO
{
	public class DAL_OleDb : DALEngine
	{
		OleDbConnection  moOleDbConn = null;

		public DAL_OleDb() : this("BEO_SQLOLEDB")
		{
		}

		public DAL_OleDb(string sDbsType) : base(sDbsType)
		{
		}

		~DAL_OleDb()
		{
		}

		public override IDbConnection GetConnection()
		{
			moOleDbConn = new System.Data.OleDb.OleDbConnection(this.ConnectionString);
			try
			{
				moOleDbConn.Open();
			}
			catch(Exception e)
			{
				throw new DALException("Fout bij openen van een verbinding met de database.\n\n", e);
			}
			return moOleDbConn;
		}

		public OleDbDataReader ExecSP_SqlDataReader(string spName)
		//RV
		{
			return ExecSP_SqlDataReader(spName, CommandBehavior.Default);
		}

		public OleDbDataReader ExecSP_SqlDataReader(string spName, CommandBehavior behavior)
		// RV
		{
			OleDbDataReader dr = null;

			try
			{
				OleDbCommand cmd = (OleDbCommand)CreateCommand(spName);
				
				dr = cmd.ExecuteReader(behavior);
			}
			catch (Exception e)
			{
				if (dr != null)
				{
					dr.Close();
					dr = null;
				}
					
				throw new DALException("Fout in DALEngine::ExecSP_SqlDataReader, in stored procedure //" + spName + "//", e);
			}		
			return dr;		
		}

		protected override IDbCommand CreateCommand(string spName)
		{
			OleDbCommand cmd = new OleDbCommand(spName, (OleDbConnection)this.ConnObject);
			cmd.CommandType = CommandType.StoredProcedure;
		
			foreach (DALprm oPrm in DAL_Parameters)
			{
				OleDbParameter oPrm_ole  = new OleDbParameter(oPrm.Name, oPrm.Value);
				oPrm_ole.Direction       = oPrm.Direction;				
				oPrm_ole.Value           = oPrm.Value;
								
				cmd.Parameters.Add(oPrm_ole);
				
				if (oPrm.Size != 0)
				{
					oPrm_ole.Size   = oPrm.Size;
					oPrm_ole.DbType = oPrm.Type;
				}	
			}
			return cmd;
		}

		public override void ExecSP_DataSet(string spName, DataSet ds, string sTblName)
		{
			try
			{
				OleDbCommand cmd = (OleDbCommand)CreateCommand(spName);				
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(ds, sTblName);
			}
			catch (Exception e)
			{				
				throw new DALException("Fout in DAL_OleDb::ExecSP_DataSet, in stored procedure //" + spName + "//", e);
			}
		}
		
		public OleDbDataReader ExecQuery_SqlDataReader(string sSQL)
			//RV
		{
			return (OleDbDataReader)this.ExecQuery_DataReader(sSQL, CommandBehavior.Default);
		}

		public override void ExecQuery_DataSet(string sSQL, DataSet ds, string sTblName)
		{		
			try
			{

				OleDbDataAdapter adapter = new OleDbDataAdapter(sSQL, (OleDbConnection)this.ConnObject);	
				adapter.Fill(ds, sTblName);
			}
			catch (Exception e)
			{				
				throw new DALException("Error in DAL_OleDb::ExecQuery_DataSet, in query //" + sSQL + "//", e);
			}		
		}

		public void ExecQuery_DataTable(string sSQL, ref DataTable dt)
		{
			try
			{
				dt = new DataTable(); 
				OleDbDataAdapter adapter = new OleDbDataAdapter(sSQL, (OleDbConnection)this.ConnObject);	
				adapter.Fill(dt);
			}
			catch (Exception e)
			{				
				throw new DALException("Error in DAL_OleDb::ExecQuery_DataTable, in query //" + sSQL + "//", e);
			}		
		}

		public void ExecQuery_DataSet(string sSQL, DataSet ds)
		{		
			try
			{
				OleDbDataAdapter adapter = new OleDbDataAdapter(sSQL, (OleDbConnection)this.ConnObject);	
				adapter.Fill(ds);
			}
			catch (Exception e)
			{				
				throw new DALException("Error in DAL_OleDb::ExecQuery_DataSet, in query //" + sSQL + "//", e);
			}		
		}

		public string[] AllTables()
		{
			string[] s = new string[]{""};
			return s;
		}
	}
}
