using System;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient; 



namespace _360Feedback_web
{
	/// <summary>
	/// Summary description for CDAC.
	/// </summary>
	public class CDAC
	{
		public CDAC()
		{
			//
			// TODO: Add constructor logic here
			//
		}

	
		//************************************************************************
		//* Purpose: Accessing SQL database                                      *
		//* Methods:                                                             *
		//*               GetDataSet                                                           *
		//*               RunProc                                                *
		//*               GetDataReader                                          *
		//*               GetDataView                                            *
		//* Created By:   Satya Akkinepally                                                          *
		//* **********************************************************************    

		public DataSet GetDataSet
			(string sConnect,string[] sProcName , string[] DataTable)
		{
			//*********************************************************************
			//* Purpose: Returns Dataset for one or multi datatables              *
			//* Input parameters:                                                 *
			//*                                              sConnect----Connection string     *
			//*         ProcName() ---StoredProcedures name in array                     *
			//*      DataTable()---DataTable name in array                        *
			//* Returns :                                                                                    *
			//*                    DataSet Object contains data                            *
			//* *******************************************************************                                          
			DataSet dstEorder;
			SqlConnection conn;
			SqlDataAdapter dadEorder; 
			try
			{
				int iCnt = sProcName.GetUpperBound(0);
				dstEorder = new DataSet();
				conn = new SqlConnection(sConnect);
				// if one datatable and SP
				if(iCnt == 0)
				{
					dadEorder = new SqlDataAdapter(sProcName[0], conn);
					dadEorder.Fill(dstEorder, DataTable[0]);
				}
					// more than one datatable and one SP
				else
				{
					conn.Open();
					//add first data table and first SP
					dadEorder = new SqlDataAdapter(sProcName[0], conn);                                         
					dadEorder.Fill(dstEorder, DataTable[0]);
					// add second datatable and second SP onwards
					for(int i=1 ;i< (iCnt +1) ;i++)
					{
						dadEorder.SelectCommand = new SqlCommand(sProcName[i], conn);
						dadEorder.Fill(dstEorder, DataTable[i]);
					}                                  
					conn.Close();
				}
				return dstEorder;
			}
			catch ( Exception oError)
			{
				//write error to the windows event log                  
				WriteToEventLog(oError);
				throw;                                       
			}
		}
		public void RunProc(string sConnect,string sProcName)
		{
			//*********************************************************************
			//* Purpose: Executing  Stored Procedures where UPDATE, INSERT        *
			//*          and DELETE statements are expected but does not          *
			//*          work for select statement is expected.                   *
			//* Input parameters:                                                 *
			//*                      sConnect----Connection string                     *
			//*               ProcName ---StoredProcedures name                   *
			//* Returns :                                                                                    *
			//*                      nothing                                                          *
			//* *******************************************************************   

			string sCommText= sProcName;
			//create a new Connection object using the connection string
			SqlConnection oConnect =new SqlConnection(sConnect);
			//create a new Command using the CommandText and Connection object
			SqlCommand oCommand =new SqlCommand(sCommText, oConnect);
			try
			{
				oConnect.Open();
				oCommand.ExecuteNonQuery();
			}
			catch(Exception oError)
			{
				//write error to the windows event log
				WriteToEventLog(oError);
				throw;
			}
			finally
			{
				oConnect.Close();
			}
		}  

		public  System.Data.SqlClient.SqlDataReader 
			GetDataReader(string sConnect, string sProcName) 
		{
			//*********************************************************************
			//* Purpose: Getting DataReader for the given Procedure
			//* Input parameters:
			//*               sConnect----Connection string
			//*               ProcName ---StoredProcedures name
			//* Returns :
			//*               DataReader contains data
			//* *******************************************************************  
			string sCommText= sProcName;


			//create a new Connection object using the connection string
			SqlConnection oConnect =new SqlConnection(sConnect);
			//create a new Command using the CommandText and Connection object
			SqlCommand oCommand = new SqlCommand(sCommText, oConnect);
			
			SqlDataReader oDR;
			try
			{
				//open the connection and execute the command
				oConnect.Open();
				//oDA.SelectCommand = oCommand
				oDR  = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
			}
					
			catch
			{
				//write error to the windows event log
				//WriteToEventLog(oError); @@@ trust level
				throw;
			}
			return oDR;
		} 

		public DataView GetDataView   ( string sConnect,string sProcName,string DataSetTable)
		{          
			//*********************************************************************
			//* Purpose: Getting DataReader for the given Procedure               *
			//* Input parameters:                                                 *
			//*               sConnect----Connection string                     *
			//*               ProcName ---StoredProcedures name                             *
			//*               DataSetTable--DataSetTable name sting               *
			//* Returns :                                                                                    *
			//*                      DataView contains data                       *
			//* ******************************************************************* 
			string sCommText= sProcName;
			//create a new Connection object using the connection string
			SqlConnection oConn = new SqlConnection(sConnect);
			//create a new Command using the CommandText and Connection object
			SqlCommand oCommand= new  SqlCommand(sCommText, oConn);
			//declare a variable to hold a DataAdaptor object
			SqlDataAdapter oDA = new SqlDataAdapter();

			try
			{
				//open the connection and execute the command
				oConn.Open();
				oDA.SelectCommand = oCommand;
				SqlDataReader oDR = oCommand.ExecuteReader();
			}
			catch(Exception oError)
			{
				//write error to the windows event log
				WriteToEventLog(oError);
				throw;
			}
			DataSet oDS;
			oDS = new DataSet(DataSetTable);
			oDA.Fill(oDS);

			DataView oDataView ;
			oDataView = new DataView(oDS.Tables[0]);
			oConn.Close();
			return oDataView;
		} 

		private void WriteToEventLog( Exception oError)
		{
			//*********************************************************************
			//* Purpose:Writing error to the windows event log                    *
			//* Input parameters:                                                 *
			//*                         oError----Exception object                     *
			//* Returns :                                                                                    *
			//*                        nothing                                            *
			//* *******************************************************************             

			System.Diagnostics.EventLog objEventLog = new System.Diagnostics.EventLog();
			objEventLog.Source = "360FBFeedback";
			objEventLog.WriteEntry(oError.Message.ToString());
		}
	} 
}
