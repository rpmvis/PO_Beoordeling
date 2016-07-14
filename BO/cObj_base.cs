using System;
using System.Data;
using System.Reflection;

namespace BO


{
	/// <summary>
	/// Base object for all Business objects
	/// </summary>
	public class cObj_base
	{
		public bool Update(DAL_OleDb oDal)
		{
			// update ALL props of this object
			bool bRet = oDal.UpdateObject(this);
			return bRet;
		}

		public bool Update_Property(DAL_OleDb oDal, string sPropertyName)
		{
			// update ONE prop of this object
			bool bRet = oDal.UpdateObject_Property(this, sPropertyName);
			return bRet;
		}
	}
}
