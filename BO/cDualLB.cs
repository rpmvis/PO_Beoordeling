using System;
using System.Web.UI.WebControls;

namespace BO
{
	/// <summary>
	/// Summary description for cDualLB.
	/// </summary>
	public class cDualLB
	{
		cList moList1;
		cList moList2;


		public cDualLB()
		{
			moList1= new cList();
			moList2= new cList(); 
		}
		
		~cDualLB()
		{
			moList1= null;
			moList2= null;
		}

		public bool FillList1(string sSQL1, 
			ListControl ctlList1,
			bool SelFirstItem)
		{
			return moList1.FillList(sSQL1, ctlList1, SelFirstItem);
		}

		public bool FillList2(string sSQL2, 
			ListControl ctlList2,
			bool SelFirstItem)
		{
			return moList2.FillList(sSQL2, ctlList2, SelFirstItem);
		}

		public cList List1
		{
			get
			{
				return moList1;
			}
		}

		public cList List2
		{
			get
			{
				return moList2;
			}
		}

		public bool RequeryListBoxes()
		{
			return (moList1.Requery() & moList2.Requery());
		}

		public bool UpdateSource_RequeryListBoxes(string sSQL)
		{
			bool bOk = false;
			DAL_OleDb mDal = new DAL_OleDb("BEO");
			int iRows = mDal.Exec_ActionQuery(sSQL);
			
			if (iRows > -1) bOk = RequeryListBoxes();

			mDal.Dispose(); 

			return bOk;
		}
	}
}
