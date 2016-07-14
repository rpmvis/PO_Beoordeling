using System;
using System.Collections; 
using System.Reflection;

namespace FUNC
{
	/// <summary>
	/// Summary description for cObject.
	/// </summary>
	public class cObject
	{
		public cObject()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static void Sort(ArrayList arrayList, string sSortField)
		{
			if (arrayList.Count ==0) return;

			Type objType = arrayList[0].GetType(); 
			IComparer oComparer = new cComparer(sSortField, objType);
			arrayList.Sort(oComparer);
		}

		private class cComparer : System.Collections.IComparer
		{
			private Type mobjType = null;
			private System.Reflection.PropertyInfo moPropInfo = null;

			public cComparer(string sSortField, Type objType)
			{
				mobjType = objType;
				PropertyInfo[] aPropInfo = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

				foreach(PropertyInfo oPI in aPropInfo)
				{
					if (oPI.Name == sSortField)
					{
						moPropInfo= oPI;
					}
				}
				// Console.Write(""); 
			}			

			public int Compare(Object x, Object y)
			{
				if (moPropInfo == null) return 0;

				object obj1 = x; // casting Object to custom object
				object oVal1 = moPropInfo.GetValue(obj1, null); 
				IComparable ic1 = (IComparable)(oVal1)   ; // cast Comparable object to LastName property of custom object

				object obj2 = y;  // casting Object to custom object
				object oVal2 = moPropInfo.GetValue(obj2, null); 
				IComparable ic2 = (IComparable)(oVal2) ;  // cast Comparable object to LastName property of custom object

				return ic1.CompareTo(ic2); // > 0 = groter ; 0 = gelijk ; < 0 = kleiner dan
			}
		}

	}
}
