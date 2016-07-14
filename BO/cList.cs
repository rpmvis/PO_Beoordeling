using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb; 

namespace BO
{
	/// <summary>
	/// Summary description for cList.
	/// </summary>
	public class cList
	{
		DAL_OleDb mDal;
		string msRowSource=""; // sp or SQL statement
		ListControl mctlList=null;
		bool mbSelFirstItem=false;

		public cList()
		{
			// constructor
		}

		private DAL_OleDb Dal
		{
			get
			{
				if (mDal == null)
				{
					mDal = new DAL_OleDb("BEO"); 	
				}
				return mDal;
			}
		}

		public int UpdateSource(string sSQL)
		{
			int iRows = this.Dal.Exec_ActionQuery(sSQL);
			return iRows;
		}


		
		public string SQL
		{
			get
			{
				bool IsQry = (msRowSource.ToUpper().Substring(0, "SELECT".Length) == "SELECT");
				if (IsQry) return msRowSource;
				else return "";
			}
		}

		public bool Requery()
		{
			return this.FillList(msRowSource, mctlList, mbSelFirstItem);
		}

		public bool FillList(string sRowSource, 
			                   ListControl ctlList,
			                   bool SelFirstItem)
		{
			msRowSource = sRowSource;
			mctlList= ctlList;
			mbSelFirstItem= SelFirstItem;

			bool ok = false;

			ctlList.Items.Clear(); 

			OleDbDataReader dr;
	
			if (this.SQL == "") dr = this.Dal.ExecSP_SqlDataReader(msRowSource);
			else dr = this.Dal.ExecQuery_SqlDataReader(msRowSource);

			ListItem oLI;
			if (dr != null)
			{
				string sValue = "";
				string sDisplay = "";

				while (dr.Read())
				{
					sValue = dr.GetValue(0).ToString();

					// 1e veld altijd data veld
					// 2e veld - indien aanwezig - display veld
					// indien afwezig: (text veld = data veld)
					if (dr.FieldCount == 1)
					{
						sDisplay = sValue;
					}
					else
					{
						sDisplay = dr.GetValue(1).ToString();
					}
					oLI = new ListItem(sDisplay, sValue); 
					ctlList.Items.Add(oLI);  
				}
			}

			dr.Close(); 

			if (SelFirstItem)
			{
				if (ctlList.Items.Count > 0)
				{
					ctlList.Items[0].Selected = true; 		
				}
			}

			ok = true;
			return ok;
		}

//		public string GetData(int SelectedIndex)
//		// get data from index
//		{
//			string s = "";
//			Hashtable htData = this.htData;
//			if (htData != null)
//			{
//				if (htData.ContainsKey( SelectedIndex))
//				{
//					s = (string)htData[SelectedIndex];
//				}
//			}
//			return s;
//		}
//
//		public string GetData(string sText)
//		// get data from text
//		{
//			string s = "";
//			Hashtable ht = this.htTextToData; 
//			if (ht != null)
//			{
//				if (ht.ContainsKey(sText))
//				{
//					s = (string)ht[sText];
//				}
//			}
//			return s;
//		}
//
//		
//		public int ListIndex(string sText)
//		{
//			int i = -1;
//			Hashtable htListIndex = this.htListIndex; 
//			if (htListIndex != null) // sText != null
//			{
//				if (sText != null)
//				{
//					if (htListIndex.ContainsKey(sText))
//					{
//						i = (int)htListIndex[sText];
//					}
//				}
//			}
//			return i;
//		}
//
//		private Hashtable htData
//		{
//			get
//			{
//				if (mhtData == null)
//				{
//					Fill_hts();
//				}
//				return mhtData;
//			}
//		}
//
//		private Hashtable htText
//		{
//			get
//			{
//				if (mhtText == null)
//				{
//					Fill_hts();
//				}
//				return mhtText;
//			}
//		}
//
//		private Hashtable htTextToData
//		{
//			get
//			{
//				if (mhtTextToData == null)
//				{
//					Fill_hts();
//				}
//				return mhtTextToData;
//			}
//		}
//
//
//
//		private Hashtable htListIndex
//		{
//			get
//			{
//				if (mhtListIndex == null)
//				{
//					Fill_hts();
//				}
//				return mhtListIndex;
//			}
//		}
//
//		private void Fill_hts()
//		{
//			DAL_OleDb mdal = new DAL_OleDb("BEO");
//
//			SqlDataReader dr;
// 
//			if (this.SQL == "") dr = this.Dal.ExecSP_SqlDataReader(msRowSource);
//			else dr = this.Dal.ExecQuery_SqlDataReader(msRowSource);
//
//			if (dr != null)
//			{
//				string sData = "";
//				string sText = "";
//				int i = -1;
//				mhtData = new Hashtable(); 
//				mhtText = new Hashtable(); 
//				mhtListIndex = new Hashtable();
//				mhtTextToData = new Hashtable();
//
//				while (dr.Read())
//				{
//					sData = dr.GetValue(0).ToString();
//
//					// 1e veld altijd data veld
//					// 2e veld - indien aanwezig - text veld
//					// indien afwezig: (text veld = data veld)
//					if (dr.FieldCount == 1)
//					{
//						sText = sData;
//					}
//					else
//					{
//						sText = dr.GetValue(1).ToString();
//					}
//
//					i++;
//					// LET OP: Key eerst
//					mhtData.Add(i,  sData);
//					mhtText.Add(i,  sText);
//					mhtListIndex.Add(sData, i);
//					mhtTextToData.Add(sText,sData);
//				}
//			}
//			this.Dal.Dispose(); 
//		}

	}
}
