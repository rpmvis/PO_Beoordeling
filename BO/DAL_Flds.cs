using System;

namespace BO
{
	/// <summary>
	/// Summary description for cFlds.
	/// </summary>
	public class cFlds: System.Collections.CollectionBase
	{
		public cFlds()
		{
		}

		public int Add(cFld oFld)
		{
			// retourneert invoegpositie
			return List.Add(oFld);
		}

		public void Insert(int i, cFld oFld)
		{
			List.Insert( i, oFld);
		}
		
		public void Remove(cFld oFld)
		{
			List.Remove(oFld);
		} 
		
		public bool Contains(cFld oFld)
		{
			return List.Contains(oFld);
		}
		
		public int IndexOf(cFld oFld)
		{
			return List.IndexOf(oFld);
		}
		
		// indexer property
		public cFld this[int i]
		{
			get { return (cFld)List[i]; }
			set { List[i] = value; }
		}
	}
}
